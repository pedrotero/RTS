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
        MaxHealth = 25;
        Health = MaxHealth;
        cv.worldCamera = Camera.main;
        tr = GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        Chasing = false;
        

    }



    void Update()
    {
        if (!Chasing && agent)
        {
            float radius = 40;
            nearby = Physics.OverlapSphere(tr.position, radius, 136);
            nearby = nearby.Where(h => h.GetComponent<Unit>().team != team).ToArray();
            float closest = radius + 1;
            foreach (Collider hit in nearby)
            {
                float dis = Vector3.Distance(hit.ClosestPoint(tr.position), tr.position);
                if (dis <= closest)
                {
                    closest = dis; //asigna al mas cercano 
                    target = hit.GetComponent<Unit>(); //posicion del objetivo como tal
                    agent.SetDestination(target.tr.position);

                }
                Chasing = true;

            }
            if (nearby.Length == 0)
            {
                //cambiar por nexo
                target = nexoTarget;
                Chasing = false;
            }

        }
        if (target && agent && agent.isOnNavMesh)
        {
            agent.SetDestination(target.tr.position);
        }
        if (target && agent)
        {
            Vector3 attackPoint = target.GetComponent<Collider>().ClosestPointOnBounds(tr.position);
            float dist2Att = (attackPoint - tr.position).magnitude;
            if (agent && target && dist2Att <= attackRadius && Time.time >= NextAttack)
            {
                //attack
                target.SendMessage("takeDamage", new Vector4(0, 0, 0, 1));

                NextAttack = Time.time + FireRate;
                agent.ResetPath();
                DrawLaser(target.tr.position);
                target = null;
                agent.isStopped = true;
                Chasing = false;
                Invoke(nameof(restartAgent), 0);
                Invoke(nameof(EraseLaser), 0.2f);
            }
            else if(agent && target && dist2Att <= attackRadius)
            {
                agent.ResetPath();
                target = null;
                agent.isStopped = true;
                Chasing = false;
                Invoke(nameof(restartAgent), 0);

            }
        }
        if (!target && agent)
        {
            agent.ResetPath();
            target = nexoTarget;
            Chasing = false;
        }
    }



    void DrawLaser(Vector3 obj)
    {
        lr.startColor = team ? Color.blue : Color.red;
        lr.endColor = team ? Color.blue : Color.red;
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
