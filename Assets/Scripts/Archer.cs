using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Archer : MonoBehaviour
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
    float FireRate = 0.5f;
    float attackRadius = 20;
    LineRenderer lr;


    void Start()
    {
        GetComponent<Collider>().enabled = false;
        MaxHealth = 50;
        Health = MaxHealth;
        cv.worldCamera = Camera.main;
        tr = GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        Chasing = false;
    }



    void Update()
    {
        if (!Chasing)
        {
            float radius = 40;
            nearby = Physics.OverlapSphere(tr.position, radius, 8);
            nearby = nearby.Where(h => h != this.GetComponent<Collider>()).ToArray();

            float closest = radius + 1;
            foreach (Collider hit in nearby)
            {
                float dis = Vector3.Distance(hit.transform.position, tr.position);
                if (dis <= closest)
                {
                    closest = dis; //asigna al mas cercano 
                    target = hit.transform; //posicion del objetivo como tal
                    if (true)  //vamos a comparar si llego al radio menor
                    {
                        agent.SetDestination(target.position);
                    }

                }
                Chasing = true;

            }
            if (nearby.Length == 0)
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
        if (target)
        {
            Vector3 attackPoint = target.GetComponent<Collider>().ClosestPointOnBounds(tr.position);
            float dist2Att = (attackPoint - tr.position).magnitude;
            if (agent && target && dist2Att <= attackRadius && Time.time >= NextAttack)
            {
                //attack

                target.SendMessage("takeDamage", new Vector4(0, 0, 0, 3));

                NextAttack = Time.time + FireRate;
                agent.ResetPath();
                agent.isStopped = true;
                Vector3 vkb = new Vector3(0,0,0); //en direccion contraria 
                rig.velocity = vkb;
                Invoke(nameof(restartAgent), vkb.magnitude * 0.2f);
                DrawLaser(target.position);
                Invoke(nameof(EraseLaser), 0.2f);
            }
        }
    }

    void takeDamage(Vector4 kbdmg)
    {
        Debug.Log("took Damage" + name);

        Health -= kbdmg.w;
        hb.UpdateDMG(Health / MaxHealth);
        if (Health <= 0)
        {
            Destroy(gameObject);
            return;
        }
        //si es un edif borrar esta parte
        agent.isStopped = true;
        Vector3 vkb = -kbdmg;
        rig.velocity = vkb;
        Invoke(nameof(restartAgent), vkb.magnitude*0.2f);
    }


    void restartAgent()
    {
        agent.isStopped = false;
        Debug.Log("Hello World");

    }


    void DrawLaser(Vector3 obj)
    {
        lr.SetPosition(0, tr.position);
        lr.SetPosition(1, obj);
        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;
        
    }

    void EraseLaser()
    {
        lr.SetPosition(0, tr.position);
        lr.SetPosition(1, tr.position);
        lr.startWidth = 0f;
        lr.endWidth = 0f;
    }

    public void activateAgent()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
    }
}
