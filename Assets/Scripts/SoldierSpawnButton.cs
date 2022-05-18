using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawnButton : MonoBehaviour
{
    bool dragging = false;
    Soldier dragged;
    public bool team = false;
    public Soldier SoldierPrefab;
    public int price;
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
                Debug.Log("mauspos " + hit.point);
                dragged.transform.position = hit.point;
            }
            else
            {
                Vector3 mauspos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dragged.transform.position = new Vector3(mauspos.x, 0, mauspos.z);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("nigga");

                dragged.GetComponent<Collider>().enabled = true;
                dragged.activateAgent(true);
                dragging = false;
            }
        }

    }





    public void DragSoldier()
    {
        if (PrepManager.instance.canAfford(price))
        {
            Vector3 mauspos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
            Debug.Log("nigga " + mauspos);
            Debug.Log("pooopooo " + dragging);
            int x = team ? 100 : -100;

            dragged = Instantiate(SoldierPrefab, new Vector3(x, 0, 0), Quaternion.identity);
        }


    }
}
