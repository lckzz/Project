using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShellCtrl : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-0.005f, 0.001f, 0));
    }
}
