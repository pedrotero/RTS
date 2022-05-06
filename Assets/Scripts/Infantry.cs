using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Infantry : MonoBehaviour
{
    public int playerV;
    Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        rig.velocity = new Vector3(x * playerV, rig.velocity.y , z * playerV);
    }
}
