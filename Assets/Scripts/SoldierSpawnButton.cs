using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawnButton : MonoBehaviour
{
    bool dragging = false;
    Soldier dragged;
    public bool team;
    public Soldier SoldierPrefab;
    public int price;
    public PrepManager prep;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (dragging)
        {
            Ray MausRay = Camera.main.ScreenPointToRay(Input.mousePosition, Camera.main.stereoActiveEye);
            RaycastHit hit;
            Physics.Raycast(MausRay, out hit, 64);
            if (hit.collider)
            {
                
                dragged.transform.position = hit.point;
            }
            else
            {
                Vector3 mauspos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 90);
                dragged.transform.position = new Vector3(mauspos.x, 0, mauspos.z);
            }
            if (Input.GetMouseButtonDown(0))
            {

                dragged.GetComponent<Collider>().enabled = true;
                dragging = false;
            }
        }

    }





    public void DragSoldier()
    {
        if (prep.canAfford(price))
        {
            Vector3 mauspos = Camera.main.ScreenToWorldPoint(Input.mousePosition+Vector3.forward*90);
            dragging = true;
            int x = team ? 100 : -100;

            dragged = Instantiate(SoldierPrefab, new Vector3(x, 0, 0), Quaternion.identity);
            prep.soldiers.Add(dragged);
        }


    }
}
