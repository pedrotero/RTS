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
    float FireRate = 4;
    float attackRadius = 2;


    void Start()
    {
        GetComponent<Collider>().enabled = false;
        MaxHealth = 50;
        Health = MaxHealth;
        cv.worldCamera = Camera.main;
        tr = GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        Chasing = false;
    }



    void Update()
    {
        if (!Chasing)
        {
            float radius = 20, radius2 = 40;
            nearby = Physics.OverlapSphere(tr.position, radius2, 8);
            nearby = nearby.Where(h => h != this.GetComponent<Collider>()).ToArray();

            float closest = radius2 + 1;
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
        if (Chasing && target != null)
        {
            agent.SetDestination(target.position);
        }
        if (agent && agent.remainingDistance < attackRadius && Time.time >= NextAttack && target != null)
        {
            //attack
            Vector3 dir = (tr.position - target.position).normalized * 5;

            target.SendMessage("takeDamage", new Vector4(dir.x, dir.y, dir.z, 10));

            NextAttack = Time.time + FireRate;
            agent.ResetPath();
            rig.velocity = new Vector3(0, 0, 0);
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
        }
        //si es un edif borrar esta parte
        agent.isStopped = true;
        Vector3 vkb = kbdmg;
        rig.velocity = vkb;
        tr.position += vkb;
    }

}
