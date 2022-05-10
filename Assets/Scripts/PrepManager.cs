using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepManager : MonoBehaviour
{
    public static PrepManager prep;
    bool dragging = false;
    GameObject dragged;
    public Infantry SoldierPrefab;

    // Start is called before the first frame update
    private void Awake()
    {
        prep = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        { Vector3 mauspos = Camera.main.WorldToScreenPoint(Input.mousePosition);
            dragged.transform.position = mauspos;
        }
    }


    private void OnMouseDown()
    {
        if (dragging)
        {
            dragged.GetComponent<Collider>().enabled = true;
            dragging = false;
        }
    }

    void DragSoldier()
    {
        Vector3 mauspos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        dragging = true;
        Instantiate(SoldierPrefab, mauspos, Quaternion.identity);
    }
}
