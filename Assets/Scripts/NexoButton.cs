using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NexoButton : MonoBehaviour
{
    bool dragging = false;
    Nexo dragged;
    public bool team;
    public Nexo NexoPrefab;
    public int price = 0;
    public PrepManager prep;
    public bool HayNexo;
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
                dragged.transform.position = hit.point + Vector3.up*4;
            }
            else
            {
                Vector3 mauspos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 90);
                dragged.transform.position = new Vector3(mauspos.x, 4, mauspos.z);
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
        if (!HayNexo)
        {
            Vector3 mauspos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 90);
            dragging = true;
            int x = team ? 100 : -100;

            dragged = Instantiate(NexoPrefab, new Vector3(x, 4, 0), Quaternion.identity);
            HayNexo = true;
            GetComponent<Button>().enabled = false;
            prep.nexo = dragged;
            prep.HayNexo = true;
            //prep.soldiers.Add(dragged);
        }


    }
}
