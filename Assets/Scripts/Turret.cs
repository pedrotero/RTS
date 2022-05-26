using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : Unit
{
    public bool canShoot = false;
    public Collider[] nearby;
    private Unit target;
    private float NextAttack;
    private float FireRate;
    public int AtkDmg;
    void Start()
    {
        AtkDmg = 3;
        NextAttack = 0;
        FireRate = 0.75f;
        MaxHealth = 300;
        Health = MaxHealth;
        cv.worldCamera = Camera.main;
        tr = GetComponent<Transform>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && Time.time >= NextAttack)
        {
            float radius = 35;
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

            }
            if (target)
            {
                target.takeDamage(AtkDmg);
                NextAttack = Time.time + FireRate;
            }
            

        }
    }

    public void takeDamage(Vector4 kbdmg)
    {


        Health -= kbdmg.w;
        hb.UpdateDMG(Health / MaxHealth);
        if (Health <= 0)
        {
            Destroy(gameObject);
            return;
        }



    }

    public bool getTeam()
    {
        return team;
    }
}
