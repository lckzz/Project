using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCtrl : MonoBehaviour
{
    float v = 0.0f;
    float h = 0.0f;
    float m_Speed = 5.0f;
    float JumpForce = 370.0f;
    int JumpCount = 0;
    Vector3 m_MoveDir = Vector3.zero;
    Vector3 NextMoveStep = Vector3.zero;
    GameObject Animal;
    Animator animator;
    SpriteRenderer spriteRend;
    Rigidbody2D rigid;
    GameObject Weapon;
    float maxspeed = 3.0f;
    bool WeaponAniCheck = false;
    WeaponAniCtrl WeaponAni;
    ShotCtrl Shot;
    Vector3 MousePos;
    Vector2 ShotDir = Vector2.zero;
    float Distance = 0.0f;
    Transform PlayerPos;
    public RaycastHit2D Playerhit;
    GameObject PlayerGroundCheclObj;
    PlayerGroundCheck PlayerGround;
    bool Delayb = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        spriteRend = this.GetComponentInChildren<SpriteRenderer>();
        //rigid = this.GetComponentInChildren<Rigidbody2D>();
        Weapon = GameObject.Find("Weapon");
        Shot = this.GetComponentInChildren<ShotCtrl>();
        WeaponAni = this.GetComponentInChildren<WeaponAniCtrl>();
        //platObject = GameObject.Find("PlayerPlatformEf2D").GetComponent<PlatformEffector2D>();
        PlayerPos = GameObject.Find("Player").transform;
        PlayerGroundCheclObj = GameObject.Find("PlayerGroundCheck");
        PlayerGround = PlayerGroundCheclObj.GetComponent<PlayerGroundCheck>();
        Debug.Log(PlayerPos);

        //Camera.main.gameObject.GetComponent<CameraCtrl>().PlayerFind(gameObject);

    }

    // Update is called once per frame
    void Update()
    {



       // Playerhit = Physics2D.Raycast(PlayerPos.transform.position, Vector2.down, 1, (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Platform")));  //플레이어가 어떤 땅을 밟고있는지 계속 체크
        //RaycastHit2D hits = Physics2D.Raycast(PlayerPos.transform.position, Vector3.down, 1);
        //if(Playerhit.collider != null)
            //Debug.Log(Playerhit.collider.name);
        //Debug.Log(Delayb);
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    if (!Delayb)
        //    {
        //        Debug.Log("공격");
        //        Delayb = true;
        //        //공격후 쿨타임 코루틴
        //        StartCoroutine(TestFunc(5.0f));//5.0초의 쿨타임을 넣어준다
        //    }
        //}
        //h = Input.GetAxis("Horizontal");
        //v = Input.GetAxis("Vertical");

        //if(h > 0.0f)
        //{
        //    m_MoveDir = new Vector3(h, 0, 0);
        //    m_MoveDir.z = 0.0f;
        //    m_MoveDir.Normalize();
        //    NextMoveStep = m_MoveDir * Time.deltaTime * m_Speed;
        //    spriteRend.flipX = false;
        //    transform.Translate(NextMoveStep);
        //    animator.SetBool("run", true);


        //}

        //else if(h < 0.0f)
        //{
        //    m_MoveDir = new Vector3(h, 0, 0);
        //    m_MoveDir.z = 0.0f;
        //    m_MoveDir.Normalize();
        //    NextMoveStep = m_MoveDir * Time.deltaTime * m_Speed;
        //    spriteRend.flipX = true;
        //    transform.Translate(NextMoveStep);
        //    animator.SetBool("run", true);
        //}
        //else
        //{
        //    animator.SetBool("run", false);
        //}



        ////캐릭터 
        //if (Input.GetButtonUp("Horizontal"))
        //{
        //    rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        //}
        //캐릭터 

        //캐릭터가 플랫폼이펙터2D위에 올라가있을때 밑점프를 누르면 밑으로 내려가게
        //하단점프

        //점프
        //if (JumpCount < 2)
        //{


        //    if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        //    { //하단 점프

        //        rigid.AddForce(Vector2.up * 100.0f);

        //        if(Playerhit.collider != null)
        //        {
        //            if (Playerhit.collider.name.Contains("Ground"))
        //                return;

        //        }

        //        //platObject.rotationalOffset = 180.0f;
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Space))
        //    {  //상단 점프
        //        rigid.AddForce(transform.up * JumpForce);
        //        JumpCount++;
        //        animator.SetBool("jumping", true);
        //    }

            
        //        //platObject.rotationalOffset = 0.0f;
        //}
        //점프
        //캐릭터 방향
        //if (Input.GetButton("Horizontal"))
        //{
        //    spriteRend.flipX = Input.GetAxisRaw("Horizontal") == -1;
        //    if (spriteRend.flipX == true)
        //    {
        //        WeaponAni.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        //        WeaponAni.transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        //    }
        //    else
        //    {
        //        WeaponAni.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //        WeaponAni.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        //    }

        //}


        //if (Mathf.Abs(rigid.velocity.x) < 0.3f)
        //{
        //    animator.SetBool("run", false);
        //}
        //else
        //{
        //    animator.SetBool("run", true);
        //}
        //캐릭터 방향



        //바닥 레이캐스트체크
        //if (rigid.velocity.y < 0.0f)
        //{
        //    int LayMask = (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Platform"));
        //    RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayMask);
        //    Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        //    if (hit.collider != null)
        //    {
        //        //Debug.Log(hit);
        //        if (hit.distance < 0.5f)
        //        {
        //            animator.SetBool("jumping", false);
        //            JumpCount = 0;
        //        }
        //    }
        //}
        //바닥 레이캐스트체크


        


        //Debug.Log(ShotDir);


        //무기를 회전시키기 위한 값들
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ShotDir = MousePos - Shot.firePos[0].transform.position;
        Distance = ShotDir.magnitude;
        ShotDir.Normalize();


        if(Distance > 1.0f)
        {
            float angle = Mathf.Atan2(ShotDir.y, ShotDir.x) * Mathf.Rad2Deg;
            if (angle < 0)
                angle += 360;
            //Debug.Log(angle);

            if (90 < angle && angle < 225)
            {
                spriteRend.flipX = true;
                WeaponAni.transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);
                WeaponAni.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);


            }

            else if (angle < 90 || 325 < angle)
            {

                spriteRend.flipX = false;
                WeaponAni.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                WeaponAni.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);


            }
        }

        //무기를 회전시키기 위한 값들




    }

    private void FixedUpdate()
    {

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if(h != 0.0f || v != 0.0f)
        {
            m_MoveDir = new Vector3(h, v, 0);
            if(1.0f < m_MoveDir.magnitude)
                m_MoveDir.Normalize();
            transform.position += m_MoveDir * 3.0f * Time.deltaTime;

            animator.SetBool("run", true);

            if(PlayerGround.GroundMin.x + 0.5f > this.transform.position.x)     //플레이어가 배경의 왼쪽으로 벗어나려고하면
            {
                Vector3 pos = this.transform.position;
                pos.x = PlayerGround.GroundMin.x + 0.5f;
                this.transform.position = pos;
            }

            if(PlayerGround.GroundMin.y + 0.7f > this.transform.position.y)        //플레이어의 y축좌표가 좌측하단의 y축보다 작아지려하면
            {
                Vector3 pos = this.transform.position;
                pos.y = PlayerGround.GroundMin.y + 0.7f;
                this.transform.position = pos;
            }

            if(PlayerGround.GroundMin2.y < this.transform.position.y)           //플레이어의 y축좌표가 좌측상단의 y축보다 커지려하면
            {
                Vector3 pos = this.transform.position;
                pos.y = PlayerGround.GroundMin2.y;
                this.transform.position = pos;
            }

            if(PlayerGround.GroundMax.x - 0.5f < this.transform.position.x)     //플레이어가 오른쪽으로 벗어나려고 하면
            {
                Vector3 pos = this.transform.position;
                pos.x = PlayerGround.GroundMax.x -0.5f;
                this.transform.position = pos;
            }


        }
        else
        {
            animator.SetBool("run", false);
        }

        //if (rigid.velocity.x > maxspeed)
        //{
        //    rigid.velocity = new Vector2(maxspeed, rigid.velocity.y);
        //}
        //else if (rigid.velocity.x < maxspeed * -1)
        //{
        //    rigid.velocity = new Vector2(maxspeed * -1, rigid.velocity.y);
        //}

        
    }


    //IEnumerator TestFunc(float Delaytime)
    //{
    //    Debug.Log(Delaytime);
    //    Debug.Log("??");
    //    yield return new WaitForSeconds(Delaytime);
 
    //    Delayb = false;

    //}




}
