using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Unit
{

    void Start()
    {
        MaxHealth = 200;
        Health = MaxHealth;
        cv.worldCamera = Camera.main;
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

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
