using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryButton : MonoBehaviour
{
    bool dragging = false;
    Infantry dragged;
    public Infantry SoldierPrefab;

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
            Physics.Raycast(MausRay, out hit);
            if (hit.collider)
            {
                Debug.Log("mauspos " + hit.point);
                dragged.transform.position = hit.point;
            }
            if (Input.GetMouseButtonDown(0))
            {
                dragged.GetComponent<Collider>().enabled = true;
                dragged.activateAgent();
                dragging = false;
            }
        }
    }


    private void OnMouseDown()
    {
    }

    

    public void DragSoldier()
    {
        Vector3 mauspos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        dragging = true;
        dragged = Instantiate(SoldierPrefab, new Vector3(mauspos.x,0,mauspos.z), Quaternion.identity);

    }
}