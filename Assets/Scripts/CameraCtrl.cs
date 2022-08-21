using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    GameObject animal;
    Vector3 newPostion = Vector3.zero;
    float smoothTime = 0.2f;
    float xVelocity = 0.0f;
    float yVelocity = 0.0f;


    //ī�޶� ����
    [HideInInspector] public Vector3 m_GroundMin = Vector3.zero;    //������ �ϴ�
    [HideInInspector] public Vector3 m_GroundMax = Vector3.zero;    //������ �������

    [HideInInspector] public Vector3 m_CamWMin = Vector3.zero;
    [HideInInspector] public Vector3 m_CamWMax = Vector3.zero;
    Vector3 m_ScWdHalf = Vector3.zero;

    float a_LmtBdLeft = 0;
    float a_LmtBdTop = 0;
    float a_LmtBdRight = 0;
    float a_LmtBdBottom = 0;
    //ī�޶� ����


    // Start is called before the first frame update
    void Start()
    {
        animal = GameObject.Find("Player");
        GameObject Groundobj = GameObject.Find("GroundObj");

        Vector3 a_GrdHalfSize = Vector3.zero;
        a_GrdHalfSize.x = Groundobj.transform.localScale.x / 2.0f;
        a_GrdHalfSize.y = Groundobj.transform.localScale.y / 2.0f;

        //�����ϴ�
        m_GroundMin.x = Groundobj.transform.position.x - a_GrdHalfSize.x;
        m_GroundMin.y = Groundobj.transform.position.y - a_GrdHalfSize.y;
        //�������
        m_GroundMax.x = Groundobj.transform.position.x + a_GrdHalfSize.x;
        m_GroundMax.y = Groundobj.transform.position.y + a_GrdHalfSize.y;
    }

    public void PlayerFind(GameObject player)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ī�޶� ȭ�� �����ϴ� �ڳ��� ���� ��ǥ
        m_CamWMin = Camera.main.ViewportToWorldPoint(Vector3.zero);
        //ī�޶� ȭ�� ������� �ڳ��� ���� ��ǥ
        m_CamWMax = Camera.main.ViewportToWorldPoint(Vector3.zero);


        //Vector3 playerPos = animal.transform.position;
        //transform.position = new Vector3(playerPos.x, playerPos.y + 2.0f, transform.position.z);
    }


    private void LateUpdate()
    {
        if (animal == null)
            return;

        newPostion = transform.position;
        newPostion.x = Mathf.SmoothDamp(transform.position.x, animal.transform.position.x,
            ref xVelocity, smoothTime);
        newPostion.y = Mathf.SmoothDamp(transform.position.y , animal.transform.position.y ,
            ref yVelocity, smoothTime);

        //ī�޶� ȭ�� �����ϴ� �ڳ��� ���� ��ǥ
        m_CamWMin = Camera.main.ViewportToWorldPoint(Vector3.zero);
        //ī�޶� ȭ�� ������� �ڳ��� ���� ��ǥ
        m_CamWMax = Camera.main.ViewportToWorldPoint(Vector3.zero);

        m_ScWdHalf.x = (m_CamWMax.x - m_CamWMin.x) / 2.0f;
        m_ScWdHalf.y = (m_CamWMax.y - m_CamWMin.y) / 2.0f;
        a_LmtBdLeft = m_GroundMin.x + 9.0f+ m_ScWdHalf.x;
        a_LmtBdTop = m_GroundMax.y - 5.0f -  m_ScWdHalf.y;
        a_LmtBdRight = m_GroundMax.x - 9.0f- m_ScWdHalf.x;
        a_LmtBdBottom = m_GroundMax.y - 7.7f - m_ScWdHalf.y;

        Debug.Log(a_LmtBdLeft);
        Debug.Log(a_LmtBdTop);
        if (newPostion.x < a_LmtBdLeft)
            newPostion.x = a_LmtBdLeft;

        if (a_LmtBdRight < newPostion.x)
            newPostion.x = a_LmtBdRight;

        if (a_LmtBdBottom > newPostion.y)
            newPostion.y = a_LmtBdBottom;

        if (a_LmtBdTop < newPostion.y)
            newPostion.y = a_LmtBdTop;


        transform.position = newPostion;

    }
}
