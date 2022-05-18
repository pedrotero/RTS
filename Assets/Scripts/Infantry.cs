using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Infantry : Soldier
{
    
    // Start is called before the first frame update
    void Start()
    {
        NextAttack = 0;
        FireRate = 2;
        attackRadius = 1;
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
        if (Chasing && target)
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
                Vector3 dir = (tr.position - target.position).normalized * 5;

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




    }


    


}
