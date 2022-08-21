using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Vector3 GroundMin = Vector3.zero;
    Vector3 GroundMax = Vector3.zero;
    Vector3 GroundMin2 = Vector3.zero;
    Vector3 GroundMax2 = Vector3.zero;
    float posx = 0.0f;
    float posy = 0.0f;
    Vector3 a_GroundHalfSize = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
        a_GroundHalfSize.x = this.transform.localScale.x / 2.0f;
        a_GroundHalfSize.y = this.transform.localScale.y / 2.0f;

        //좌측하단
        GroundMin.x = this.transform.position.x - a_GroundHalfSize.x;
        GroundMin.y = this.transform.position.y - a_GroundHalfSize.y;
        //좌측상단
        GroundMin2.x = this.transform.position.x - a_GroundHalfSize.x;
        GroundMin2.y = this.transform.position.y + a_GroundHalfSize.y;
        //우측상단
        GroundMax.x = this.transform.position.x + a_GroundHalfSize.x;
        GroundMax.y = this.transform.position.y + a_GroundHalfSize.y;
        //우측하단
        GroundMax2.x = this.transform.position.x + a_GroundHalfSize.x;
        GroundMax2.y = this.transform.position.y - a_GroundHalfSize.y;
    }

    // Update is called once per frame
    void Update()
    {
        a_GroundHalfSize.x = this.transform.localScale.x / 2.0f;
        a_GroundHalfSize.y = this.transform.localScale.y / 2.0f;

        //좌측하단
        GroundMin.x = this.transform.position.x - a_GroundHalfSize.x;
        GroundMin.y = this.transform.position.y - a_GroundHalfSize.y;
        //좌측상단
        GroundMin2.x = this.transform.position.x - a_GroundHalfSize.x;
        GroundMin2.y = this.transform.position.y + a_GroundHalfSize.y;
        //우측상단
        GroundMax.x = this.transform.position.x + a_GroundHalfSize.x;
        GroundMax.y = this.transform.position.y + a_GroundHalfSize.y;
        //우측하단
        GroundMax2.x = this.transform.position.x + a_GroundHalfSize.x;
        GroundMax2.y = this.transform.position.y - a_GroundHalfSize.y;
        //좌측상단에서 우측상단까지의 선
        Debug.DrawLine(GroundMin2, GroundMax,Color.black);
        //우측상단에서 우측하단까지의 선
        Debug.DrawLine(GroundMax, GroundMax2, Color.black);
        //우측하단에서 좌측하단까지의 선
        Debug.DrawLine(GroundMax2, GroundMin, Color.black);
        //좌측하단에서 좌측상단까지의 선
        Debug.DrawLine(GroundMin, GroundMin2, Color.black);


    }
}
