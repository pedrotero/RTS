using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Infantry : MonoBehaviour
{
    private Camera cam;
    private NavMeshAgent agent;
    public int playerV;
    Rigidbody rig;
    float Health;
    float MaxHealth;
    public Canvas cv;
    public HealthBar hb;
    public bool team;
    public Vector3 target;
    public Transform tr;
    private Collider[] nearby;
    private bool Chasing;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 100;
        Health = MaxHealth;
        cv.worldCamera = Camera.main;
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        tr = GetComponent<Transform>();
        Chasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Chasing)
        {
            nearby = Physics.OverlapSphere(tr.position, 10, 8, QueryTriggerInteraction.Ignore);
            target = tr.position; //cambiar por nexo

            float closest = 11;
            foreach (Collider hit in nearby)
            {
                Debug.Log("Hello World");


                float dis = Vector3.Distance(hit.transform.position, tr.position);
                if (dis < closest)
                {
                    closest = dis;
                    target = hit.transform.position;
                    Debug.Log("chasing: " + hit.name + " with Vector3 "+ target);
                }
            }
                Chasing = true;
                agent.SetDestination(target);
            
        }
        Debug.Log("remdist" + agent.remainingDistance);

        //if (Input.GetMouseButtonDown(0))
        //{ 
        //    Health *= 0.66f;
        //    hb.UpdateDMG(Health/MaxHealth);
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        agent.SetDestination(hit.point);
        //    }
        //    if(agent.remainingDistance < agent.stoppingDistance)
        //    {
        //        //attack
        //    }

        //}




    }
}
