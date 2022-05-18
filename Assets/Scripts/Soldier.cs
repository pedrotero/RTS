using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    public NavMeshAgent agent;
    public float Health;
    public float MaxHealth;
    public Canvas cv;
    public HealthBar hb;
    public bool team;
    public Rigidbody rig;
    public Transform target;
    public Transform tr;
    public Collider[] nearby;
    public bool Chasing;
    public float NextAttack;
    public float FireRate;
    public float attackRadius;
    // Start is called before the first frame update

    public void takeDamage(Vector4 kbdmg)
    {

        Health -= kbdmg.w;
        hb.UpdateDMG(Health / MaxHealth);
        if (Health <= 0)
        {
            Destroy(gameObject);
            return;
        }
        //si es un edif borrar esta parte
        agent.isStopped = true;
        Vector3 vkb = -kbdmg; //en direccion contraria 
        rig.velocity = vkb;
        Invoke(nameof(restartAgent), vkb.magnitude * 0.2f);


    }

    public void restartAgent()
    {
        agent.isStopped = false;
        rig.velocity = new Vector3(0, 0, 0);
    }

    public bool getTeam()
    {
        return team;
    }


    public void activateAgent(bool t)
    {
        team = t;
        agent = gameObject.AddComponent<NavMeshAgent>();
    }
}
