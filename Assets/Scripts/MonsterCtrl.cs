using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Crawler,            //에벌레
    Flying,              //날파리
    FlyingRider,        //날파리라이더
    Melee1,             //딱정벌레
    Melee2,             //딱정벌레2
    Ranged,             //창던지는고블린
    Trap1,              //함정1
    Trap2,               //함정2
    Summoner,            //보스1의 소환수
    Count
}

public enum MonsterState
{
    Idle,
    Trace,
    Attack

}

public class MonsterCtrl : MonoBehaviour
{
    public MonsterType MonType = MonsterType.Crawler;
    public MonsterState MonState = MonsterState.Idle;
    AnimalCtrl animal;
    GameObject Playerobj;           //플레이어의 위치를 알기위한 변수
    Animator anim;
    SpriteRenderer SpriteRend;
    [HideInInspector] public int MonUniqueid = 0;
    float MonHp = 0.0f;
    float MonAtt = 0.0f;
    float MonSpeed = 0.0f;
    float moveTimer = 0.0f;     //몬스터 이동 좌표를 찍어줄 시간
    float MoveX = 0.0f;         //몬스터 이동타이머가 0이 되면 이동할 랜덤한 x좌표를 받는 변수
    float Delay = 0.0f;         //대충 스킬딜레이나 행동딜레이
    RaycastHit2D righthit;
    RaycastHit2D lefthit;
    Vector3 m_Pos = Vector3.zero;
    Vector3 m_TargetPos = Vector3.zero;
    Vector3 m_Dir = Vector3.zero;
    float m_Distance = 0.0f;
    int reverseDir = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        MonsterSetting();
        animal = FindObjectOfType<AnimalCtrl>();
        Playerobj = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        SpriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveTimer > 0.0f)        //몬스터가 처음 생성됫을때나 특정이동좌표로 이동 완료시 이동할 좌표를 찍어줄 타이머
        {
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0.0f)
            {
                moveTimer = 0.0f;
                MoveX = Random.Range(-5.0f, 5.0f);
                m_Pos = this.transform.position;
                m_Pos.x += MoveX;
            }
        }
        if (Delay > 0.0f)       
        {
            Delay -= Time.deltaTime;
            if (Delay <= 0.0f)
            {
                Delay = 0.0f;

            }
        }


        if (MonType == MonsterType.Crawler)      //몬스터가 애벌레라면
        {
            if(MonState == MonsterState.Idle)       //몬스터상태가 대기상태라면
            {
               if(moveTimer <= 0.0f)
                {


                    m_TargetPos = Playerobj.transform.position - this.transform.position;
                    m_Dir = m_TargetPos.normalized;
                    m_Distance = m_TargetPos.magnitude;
                    Debug.Log(m_Dir);
                    if (m_Dir.x > 0.0f)
                    {
                        SpriteRend.flipX = true;
                    }
                    else
                    {
                        SpriteRend.flipX = false;
                    }
                    this.transform.Translate(m_Dir * Time.deltaTime * MonSpeed);

  




                }

            }
        }




        if(MonType == MonsterType.Summoner)
        {
            if (MonState == MonsterState.Idle)
            {
          
                anim.SetTrigger("appear");
                Delay = 1.5f;
                MonState = MonsterState.Trace;
  
            }

            else if(MonState == MonsterState.Trace)
            {
                if(Delay <= 0.0f)
                {
                    anim.SetBool("run",true);
                    if (reverseDir == 1)
                    {
                        SpriteRend.flipX = true;
                    }
                    else
                        SpriteRend.flipX = false;
                    m_Dir.x = m_Dir.x - reverseDir;

                    this.transform.Translate(m_Dir * Time.deltaTime * MonSpeed);
                    m_Dir = Vector3.zero;
                    Vector3 RightRay = this.transform.position;
                    RightRay.x += 1.0f;
                    Vector3 LeftRay = this.transform.position;
                    LeftRay.x -= 1.0f;

                    righthit = Physics2D.Raycast(RightRay, Vector3.right, 1, LayerMask.GetMask("Ground"));
                    Debug.DrawRay(RightRay, Vector3.down, Color.blue);
                    lefthit = Physics2D.Raycast(LeftRay, Vector3.left, 1, LayerMask.GetMask("Ground"));
                    Debug.DrawRay(LeftRay, Vector3.down, Color.red);

                    if (lefthit.collider != null)
                    {
                        if (lefthit.collider.tag == "ground")
                        {
                            reverseDir = -1;
                            //Debug.Log(reverseDir);

                        }
                    }
                    if (righthit.collider != null)
                    {
                        //Debug.Log(righthit.collider);
                        if (righthit.collider.name.Contains("Ground"))
                        {

                            reverseDir = 1;
                            //Debug.Log(reverseDir);

                        }
                    }
                }
 
            
       

            }
        }
    }

    void MonsterSetting()   //몬스터 종류에따른 정보
    {
        if (MonType == MonsterType.Crawler)
        {
            MonUniqueid = 1000;
            MonHp = 200.0f;
            MonAtt = 10.0f;
            MonSpeed = 0.5f;
            moveTimer = 0.0f;
        }

        else if (MonType == MonsterType.Flying)
        {
            MonUniqueid = 1001;
            MonHp = 200.0f;
            MonAtt = 10.0f;
            MonSpeed = 2.0f;
        }

        else if (MonType == MonsterType.FlyingRider)
        {
            MonUniqueid = 1002;
            MonHp = 400.0f;
            MonAtt = 15.0f;
            MonSpeed = 3.5f;
        }

        else if (MonType == MonsterType.Melee1)
        {
            MonUniqueid = 1003;
            MonHp = 500.0f;
            MonAtt = 15.0f;
            MonSpeed = 2.5f;
        }

        else if (MonType == MonsterType.Melee2)
        {
            MonUniqueid = 1004;
            MonHp = 500.0f;
            MonAtt = 15.0f;
            MonSpeed = 2.5f;
        }

        else if (MonType == MonsterType.Ranged)
        {
            MonUniqueid = 1005;
            MonHp = 300.0f;
            MonAtt = 25.0f;
            MonSpeed = 1.5f;
        }

        else if (MonType == MonsterType.Trap1)
        {
            MonUniqueid = 1006;
            MonHp = 500.0f;
            MonAtt = 30.0f;
            MonSpeed = 0.0f;
        }

        else if (MonType == MonsterType.Trap2)
        {
            MonUniqueid = 1007;
            MonHp = 700.0f;
            MonAtt = 50.0f;
            MonSpeed = 0.0f;
        }

        else if (MonType == MonsterType.Summoner)
        {
            MonUniqueid = 1008;
            MonHp = 1000.0f;
            MonAtt = 25.0f;
            MonSpeed = 3.0f;
        }
    }
}
