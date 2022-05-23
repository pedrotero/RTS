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
        if (agent)
        {
            agent.isStopped = true;
            Vector3 vkb = -kbdmg; //en direccion contraria 
            rig.velocity = vkb;
            Invoke(nameof(restartAgent), vkb.magnitude * 0.2f);
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
