using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Hero : Soldier
{
    // Start is called before the first frame update
    void Start()
    {
        NextAttack = 0;
        FireRate = 2;
        attackRadius = 5;
        GetComponent<Collider>().enabled = false;
        MaxHealth = 500;
        Health = MaxHealth;
        cv.worldCamera = Camera.main;
        tr = GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        Chasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Chasing && agent)
        {
            float radius = 20;
            nearby = Physics.OverlapSphere(tr.position, radius, 136);
            nearby = nearby.Where(h => h.GetComponent<Unit>().team != team).ToArray();
            float closest = radius + 1;
            foreach (Collider hit in nearby)
            {

                float dis = Vector3.Distance(hit.ClosestPoint(tr.position), tr.position);
                if (dis <= closest)
                {
                    closest = dis;
                    target = hit.GetComponent<Soldier>();
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
            agent.SetDestination(target.GetComponent<Collider>().ClosestPoint(tr.position));
        }

        if (target && agent)
        {
            Vector3 attackPoint = target.GetComponent<Collider>().ClosestPointOnBounds(tr.position);
            float dist2Att = (attackPoint - tr.position).magnitude;
            if (agent && target && dist2Att <= attackRadius && Time.time >= NextAttack)
            {
                //attack
                Vector3 dir = (tr.position - target.tr.position).normalized * 5;
                target.SendMessage("takeDamage", new Vector4(dir.x, dir.y, dir.z, 10));

                NextAttack = Time.time + FireRate;
                agent.ResetPath();
                target = null;
                agent.isStopped = true;
                Chasing = false;
                Vector3 vkb = dir; //en direccion contraria 
                rig.velocity = vkb;
                Invoke(nameof(restartAgent), vkb.magnitude * 0.2f);
            }
        }
        if (!target && agent)
        {
            agent.ResetPath();
            target = nexoTarget;
            Chasing = false;
        }
    }
}