using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : Unit
{
    public NavMeshAgent agent;
    public Rigidbody rig;
    public Unit target;
    public Collider[] nearby;
    public bool Chasing;
    public float NextAttack;
    public float FireRate;
    public float attackRadius;
    public Nexo nexoTarget;
    public bool canReach;
    // Start is called before the first frame update

    public new void takeDamage(float dmg)
    {

        Health -= dmg;
        hb.UpdateDMG(Health / MaxHealth);
        if (Health <= 0)
        {
            Destroy(gameObject);
            return;
        }
        //si es un edif borrar esta parte
        if (agent)
        {
            Invoke(nameof(restartAgent), 0.2f);
        }



    }

    public void restartAgent()
    {
        if (agent)
        {
            agent.isStopped = false;
        }
        
        rig.velocity = new Vector3(0, 0, 0);
    }

    public bool getTeam()
    {
        return team;
    }


    public void activateAgent(bool t,Nexo n)
    {
        nexoTarget = n;
        team = t;
        agent = gameObject.AddComponent<NavMeshAgent>();
        
    }
}
