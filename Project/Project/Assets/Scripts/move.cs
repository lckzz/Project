using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Rigidbody2D rig;
    float jump = 780.0f;
    float walk = 30.0f;
    float maxwalk = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(transform.up * jump);
        }
    }
}
