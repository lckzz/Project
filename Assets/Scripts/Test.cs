using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Rigidbody2D rb;
    bool attcheck = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(attcheck == false)
            rb.AddForce(Vector2.down * 70.0f);

        if(transform.position.y <= -3.0f)
        {
            attcheck = true;
            
        }

        if(attcheck == true)
        {

        }
        
    }
}
