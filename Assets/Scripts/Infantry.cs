using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Infantry : MonoBehaviour
{
    private NavMeshAgent agent;
    public int playerV;
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
    public float x, z;
    float NextAttack = 0;
    float FireRate = 1;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 100;
        Health = MaxHealth;
        cv.worldCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        tr = GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        Chasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        x = transform.position.x;
        z = transform.position.z;
        if (!Chasing)
        {
            float radius = 20;
            nearby= Physics.OverlapSphere(tr.position, radius, 8);
            nearby = nearby.Where(h => h != this.GetComponent<Collider>()).ToArray();
            Debug.Log("nearby l: "+nearby.Length);

            float closest = radius + 1;
            foreach (Collider hit in nearby)
            {
                float dis = Vector3.Distance(hit.transform.position, tr.position);
                if (dis <= closest)
                {
                    closest = dis;
                    target = hit.transform;
                    Debug.Log(this.name + " chasing: " + hit.name);
                    agent.SetDestination(target.position);
                }
                Chasing = true;
                
            }
            if (nearby.Length==0)
            {
                Chasing = false;
                agent.ResetPath();//cambiar por nexo
                target = null;
            }

        }
        if (Chasing && target!=null)
        {
            agent.SetDestination(target.position);
        }
        if (agent.remainingDistance < agent.stoppingDistance && Time.time>=NextAttack && target!=null)
        {
            //attack
            Debug.Log("attack");
            Vector3 dir = (tr.position - target.position).normalized * 5;
            rig.AddForce(dir,ForceMode.Impulse);
            NextAttack = Time.time+FireRate;
        }




    }
}
