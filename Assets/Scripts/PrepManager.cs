using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepManager : MonoBehaviour
{
    public List<Soldier> soldiers;
    public List<Turret> turrets;
    public int Budget;
    public Text budt;
    public bool team;
    public Nexo nexo;
    public PrepManager otherPrep;
    public bool HayNexo=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool canAfford(int price)
    {
        if (price <= Budget)
        {
            Budget -= price;
            budt.text = "Cash: $" + Budget;
            return true;
        }
        else
        {
            return false;
        }
    }


    public void ChangeScreen()
    {
        if (HayNexo)
        {
            int x = 113;
            int y = 90;
            if (team)
            {
                x = 0;
                y = 90;
            }

            Vector3 newPos = new Vector3(x, y, 0);
            Camera.main.transform.position = newPos;
        }
        
    }

    public void BeginGame()
    {
        if (HayNexo && otherPrep.HayNexo)
        {
            foreach (Soldier sol in soldiers)
            {
                sol.activateAgent(team, otherPrep.nexo);
                sol.agent.speed = sol.agtSpeed;

            }
            foreach (Turret tur in turrets)
            {
                tur.canShoot = true;
                tur.team = team;
            }
        }
        
    }

}
