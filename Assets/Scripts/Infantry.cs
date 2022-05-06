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
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 100;
        Health = MaxHealth;
        cv.worldCamera = Camera.main;
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Health *= 0.66f;
            hb.UpdateDMG(Health/MaxHealth);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }

        }
    }
}
