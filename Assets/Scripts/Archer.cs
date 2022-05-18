using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Archer : Soldier
{
    
    LineRenderer lr;



    void Start()
    {
        NextAttack = 0;
        FireRate = 0.5f;
        attackRadius = 20;
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
        if (Chasing && target)
        {
            agent.SetDestination(target.position);
        }
        if (target)
        {
            Vector3 attackPoint = target.GetComponent<Collider>().ClosestPointOnBounds(tr.position);
            float dist2Att = (attackPoint - tr.position).magnitude;
            if (agent && dist2Att <= attackRadius && Time.time >= NextAttack)
            {
                //attack
                target.SendMessage("takeDamage", new Vector4(0, 0, 0, 3));

                NextAttack = Time.time + FireRate;
                agent.ResetPath();
                DrawLaser(target.position);
                target = null;
                agent.isStopped = true;
                Chasing = false;
                Invoke(nameof(restartAgent), 0);
                Invoke(nameof(EraseLaser), 0.2f);
            }
            else if(agent && dist2Att <= attackRadius)
            {
                agent.ResetPath();
                target = null;
                agent.isStopped = true;
                Chasing = false;
                Invoke(nameof(restartAgent), 0);

            }
        }
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


}
