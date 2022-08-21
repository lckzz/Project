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

        //�����ϴ�
        GroundMin.x = this.transform.position.x - a_GroundHalfSize.x;
        GroundMin.y = this.transform.position.y - a_GroundHalfSize.y;
        //�������
        GroundMin2.x = this.transform.position.x - a_GroundHalfSize.x;
        GroundMin2.y = this.transform.position.y + a_GroundHalfSize.y;
        //�������
        GroundMax.x = this.transform.position.x + a_GroundHalfSize.x;
        GroundMax.y = this.transform.position.y + a_GroundHalfSize.y;
        //�����ϴ�
        GroundMax2.x = this.transform.position.x + a_GroundHalfSize.x;
        GroundMax2.y = this.transform.position.y - a_GroundHalfSize.y;
    }

    // Update is called once per frame
    void Update()
    {
        a_GroundHalfSize.x = this.transform.localScale.x / 2.0f;
        a_GroundHalfSize.y = this.transform.localScale.y / 2.0f;

        //�����ϴ�
        GroundMin.x = this.transform.position.x - a_GroundHalfSize.x;
        GroundMin.y = this.transform.position.y - a_GroundHalfSize.y;
        //�������
        GroundMin2.x = this.transform.position.x - a_GroundHalfSize.x;
        GroundMin2.y = this.transform.position.y + a_GroundHalfSize.y;
        //�������
        GroundMax.x = this.transform.position.x + a_GroundHalfSize.x;
        GroundMax.y = this.transform.position.y + a_GroundHalfSize.y;
        //�����ϴ�
        GroundMax2.x = this.transform.position.x + a_GroundHalfSize.x;
        GroundMax2.y = this.transform.position.y - a_GroundHalfSize.y;
        //������ܿ��� ������ܱ����� ��
        Debug.DrawLine(GroundMin2, GroundMax,Color.black);
        //������ܿ��� �����ϴܱ����� ��
        Debug.DrawLine(GroundMax, GroundMax2, Color.black);
        //�����ϴܿ��� �����ϴܱ����� ��
        Debug.DrawLine(GroundMax2, GroundMin, Color.black);
        //�����ϴܿ��� ������ܱ����� ��
        Debug.DrawLine(GroundMin, GroundMin2, Color.black);


    }
}
