using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepManager : MonoBehaviour
{
    public int Budget;
    public Text budt;
    public static PrepManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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


}
