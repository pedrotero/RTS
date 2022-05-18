using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepManager : MonoBehaviour
{
    public List<Soldier> soldiers;
    public int Budget;
    public Text budt;
    public bool team;
    public Nexo nexo;
    public PrepManager otherPrep;
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
        int x = 70;
        int y = 80;
        if (team)
        {
            x = 0;
            y = 90;
        }

        Vector3 newPos = new Vector3(x, y, 0);
        Camera.main.transform.position = newPos;
    }

    public void BeginGame()
    {
        foreach (Soldier sol in soldiers)
        {
            sol.activateAgent(team,otherPrep.nexo);
        }
    }

}
