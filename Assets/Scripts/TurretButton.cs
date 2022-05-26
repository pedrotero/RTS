using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour
{
    bool dragging = false;
    Turret dragged;
    public bool team;
    public Turret TurretPrefab;
    public int price = 100;
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
                Debug.Log("name" + hit.collider.name);
                dragged.transform.position = hit.point + Vector3.up * 4;
            }
            else
            {
                Vector3 mauspos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 90);
                dragged.transform.position = new Vector3(mauspos.x, 4, mauspos.z);
            }
            if (Input.GetMouseButtonDown(0))
            {

                if (Mathf.Abs(dragged.transform.position.x) <= 80 && Mathf.Abs(dragged.transform.position.z) <= 45)
                {

                    dragged.GetComponent<Collider>().enabled = true;
                    dragging = false;
                }
            }
        }

    }





    public void DragSoldier()
    {
        if (prep.canAfford(price))
        {
            Vector3 mauspos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 90);
            dragging = true;
            int x = team ? 100 : -100;

            dragged = Instantiate(TurretPrefab, new Vector3(x, 0, 0), Quaternion.identity);
            dragged.GetComponent<Renderer>().material.color = team ? Color.blue : Color.red;
            prep.turrets.Add(dragged);
        }


    }
}
