using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : MonoBehaviour
{
    bool dragging = false;
    bool draggingwall = false;
    Wall dragged;
    public bool team;
    public Wall WallPrefab;
    public int price;
    public PrepManager prep;
    public Vector3 startPos;
    public Vector3 endPos;
    Vector3 mauspos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Ray MausRay = Camera.main.ScreenPointToRay(Input.mousePosition, Camera.main.stereoActiveEye);
        RaycastHit hit;
        Physics.Raycast(MausRay, out hit, 64);
        if (hit.collider)
        {
            mauspos = hit.point;
        }
        else
        {
            mauspos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 90);
        }


        if (dragging)
        {
            startPos = new Vector3(mauspos.x, 0, mauspos.z);
            dragged.transform.position = startPos;
        }
        if (draggingwall)
        {
            endPos = new Vector3(mauspos.x, 0, mauspos.z);
            dragged.transform.position = Vector3.Lerp(startPos, endPos, 0.5f);
            Vector3 dir = endPos - startPos;
            float angle = Vector3.SignedAngle(Vector3.right, dir,Vector3.up);
            float size = Mathf.Abs((endPos - startPos).magnitude);
            dragged.transform.localScale = new Vector3(size, 5, 2);
            dragged.transform.rotation = Quaternion.Euler(new Vector3(0,angle,0));
            dragged.hb.transform.rotation = Quaternion.SlerpUnclamped(dragged.transform.rotation, dragged.hb.startRotation, -1);
            Debug.Log("angle"+angle);

        }

        if (Input.GetMouseButtonDown(0))
        {
            if (dragging)
            {
                dragging = false;
                draggingwall = true;
            }
            else if (draggingwall)
            {
                draggingwall = false;
                if (prep.canAfford((int)(startPos-endPos).magnitude*3 + 20))
                {
                    dragged.GetComponent<Collider>().enabled = true;
                }
                else
                {
                    Destroy(dragged.gameObject);
                    dragged = null;
                }
                Debug.Log("Hello World "+dragged);

            }
        }

    }





    public void CreateWall()
    {
            mauspos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 90);
            dragging = true;
            int x = team ? 100 : -100;

            dragged = Instantiate(WallPrefab, new Vector3(x, 0, 0), Quaternion.identity);
            dragged.GetComponent<Renderer>().material.color = team ? Color.blue : Color.red;
        //prep.soldiers.Add(dragged);


    }
}
