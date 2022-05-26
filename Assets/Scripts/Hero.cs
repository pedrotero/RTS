using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Hero : Soldier
{
    public int charges = 2;
    KeyCode skill;
    public KeyCode sk0;
    public KeyCode sk1;
    public ParticleSystem ps;
    public float skillDur = 0;
    // Start is called before the first frame update

    void Start()
    {
        agtSpeed = 5;
        AtkDmg = 15;
        NextAttack = 0;
        FireRate = 2f;
        attackRadius = 5;
        GetComponent<Collider>().enabled = false;
        MaxHealth = 650;
        Health = MaxHealth;
        cv.worldCamera = Camera.main;
        tr = GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        Chasing = false;
        ps.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Chasing && agent)
        {
            float radius = 40;
            nearby = Physics.OverlapSphere(tr.position, radius, 8);
            nearby = nearby.Where(h => h.GetComponent<Unit>().team != team).ToArray();
            float closest = radius + 1;
            foreach (Collider hit in nearby)
            {
                float dis = Vector3.Distance(hit.ClosestPoint(tr.position), tr.position);
                if (dis <= closest)
                {
                    closest = dis; //asigna al mas cercano 
                    target = hit.GetComponent<Unit>(); //posicion del objetivo como tal

                }
                Chasing = true;

            }
            if (nearby.Length == 0)
            {
                //cambiar por nexo
                target = nexoTarget;
                Chasing = false;
            }
            agent.SetDestination(target.col.ClosestPoint(tr.position));
        }

        if (target && agent && agent.pathStatus != NavMeshPathStatus.PathComplete)
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
            if (charges>0 && Input.GetKeyDown(skill) && Time.time >= skillDur)
            {
                Debug.Log("Hello World");

                ps.Play();
                agent.speed = 10;
                attackRadius = 10;
                AtkDmg = 30;
                FireRate = 1f;
                charges--;
                skillDur = Time.time + 7.5f;
            }
            if (Time.time >= skillDur)
            {
                ps.Pause();
                ps.Clear();
                attackRadius = 5;
                FireRate = 2.5f;
                AtkDmg = 15;
                agent.speed = 5;

            }

            Vector3 attackPoint = target.GetComponent<Collider>().ClosestPointOnBounds(tr.position);
            float dist2Att = (attackPoint - tr.position).magnitude;
            if (agent && target && dist2Att <= attackRadius && Time.time >= NextAttack)
            {
                Debug.Log("Hello World");

                //attack
                Vector3 dir = (tr.position - target.tr.position).normalized * 5;
                target.takeDamage(AtkDmg);

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


    public new void activateAgent(bool t, Nexo n)
    {
        nexoTarget = n;
        team = t;
        agent = gameObject.AddComponent<NavMeshAgent>();
        skill = team ? sk1 : sk0;
    }
}
