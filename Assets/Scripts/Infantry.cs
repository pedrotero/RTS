using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Infantry : MonoBehaviour
{
    private NavMeshAgent agent;
    float Health;
    float MaxHealth;
    public Canvas cv;
    public HealthBar hb;
    public bool team;
    private Rigidbody rig;
    public Transform target;
    public Transform tr;
    private Collider[] nearby;
    private bool Chasing;
    float NextAttack = 0;
    float FireRate = 2;
    float attackRadius = 1;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().enabled = false;
        MaxHealth = 100;
        Health = MaxHealth;
        cv.worldCamera = Camera.main;
        tr = GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        Chasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Chasing)
        {
            float radius = 20;
            nearby= Physics.OverlapSphere(tr.position, radius, 8);
            nearby = nearby.Where(h => h != this.GetComponent<Collider>()).ToArray();

            float closest = radius + 1;
            foreach (Collider hit in nearby)
            {
                float dis = Vector3.Distance(hit.transform.position, tr.position);
                if (dis <= closest)
                {
                    closest = dis;
                    target = hit.transform;
                    agent.SetDestination(target.position);
                }
                Chasing = true;
                
            }
            if (nearby.Length==0)
            {
                Chasing = false;
                if (agent)
                {
                    agent.ResetPath();
                }
                //cambiar por nexo
                target = null;
            }

        }
        if (Chasing && target!=null)
        {
            agent.SetDestination(target.position);
        }

        if (target)
        {
            Vector3 attackPoint = target.GetComponent<Collider>().ClosestPointOnBounds(tr.position);
            float dist2Att = (attackPoint - tr.position).magnitude;
            if (agent && target && dist2Att <= attackRadius && Time.time >= NextAttack)
            {
                //attack
                Vector3 dir = (tr.position - target.position).normalized * 10;

                target.SendMessage("takeDamage", new Vector4(dir.x, dir.y, dir.z, 10));

                NextAttack = Time.time + FireRate;
                agent.ResetPath();
                agent.isStopped = true;
                Vector3 vkb = dir; //en direccion contraria 
                rig.velocity = vkb;
                Invoke(nameof(restartAgent), vkb.magnitude * 0.2f);
            }
        }




    }

    void takeDamage(Vector4 kbdmg)
    {
        Debug.Log("took Damage"+name);

        Health -= kbdmg.w;
        hb.UpdateDMG(Health / MaxHealth);
        if (Health <= 0)
        {
            Destroy(gameObject);
            return;
        }
        //si es un edif borrar esta parte
        agent.isStopped = true;
        Vector3 vkb = -kbdmg; //en direccion contraria 
        rig.velocity = vkb;
        Invoke(nameof(restartAgent), vkb.magnitude * 0.2f);

        
    }

    void restartAgent()
    {
        agent.isStopped = false;
        Debug.Log("Hello World");

    }

    public void activateAgent()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
    }

}
