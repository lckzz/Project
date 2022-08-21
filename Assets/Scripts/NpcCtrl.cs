using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NpcCtrl : MonoBehaviour
{

    Vector3 m_Dir = Vector3.zero;
    Vector3 DirNor = Vector3.zero;
    float Distance = 0.0f;
    GameObject AnimalPlayer;
    SpriteRenderer NpcFilp;
    Image TextImage;
    // Start is called before the first frame update
    void Start()
    {
        AnimalPlayer = GameObject.Find("Player");
        NpcFilp = GetComponent<SpriteRenderer>();
        TextImage = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Dir = AnimalPlayer.transform.position - this.transform.position;
        Distance = m_Dir.magnitude;
        DirNor = m_Dir.normalized;

        //Debug.Log(DirNor);

        if(Distance < 1.5f)
        {
            TextImage.gameObject.SetActive(true);       //ĳ���� ��ȭȰ��ȭ ��
            if(DirNor.x < 0.0f) //npc�� ĳ���ͻ����� ������ �����̶��
            {
                NpcFilp.flipX = true;
            }
            else
            {
                NpcFilp.flipX = false;
            }
        }
        else
        {
            TextImage.gameObject.SetActive(false);      //ĳ���� ��ȭȰ��ȭ ����
        }
    }


}
