using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nexo : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 500;
        Health = MaxHealth;
        cv.worldCamera = Camera.main;
        tr = GetComponent<Transform>();
        col = GetComponent<Collider>();
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
            if (team)
            {
                PlayerPrefs.SetInt("Winner", 0);
                SceneManager.LoadScene("EndScreen");
            }
            else
            {
                PlayerPrefs.SetInt("Winner", 1);
                SceneManager.LoadScene("EndScreen");
            }
            return;
        }



    }

    public bool getTeam()
    {
        return team;
    }
}
