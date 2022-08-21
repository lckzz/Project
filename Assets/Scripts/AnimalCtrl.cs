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



       // Playerhit = Physics2D.Raycast(PlayerPos.transform.position, Vector2.down, 1, (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Platform")));  //�÷��̾ � ���� ����ִ��� ��� üũ
        //RaycastHit2D hits = Physics2D.Raycast(PlayerPos.transform.position, Vector3.down, 1);
        //if(Playerhit.collider != null)
            //Debug.Log(Playerhit.collider.name);
        //Debug.Log(Delayb);
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    if (!Delayb)
        //    {
        //        Debug.Log("����");
        //        Delayb = true;
        //        //������ ��Ÿ�� �ڷ�ƾ
        //        StartCoroutine(TestFunc(5.0f));//5.0���� ��Ÿ���� �־��ش�
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



        ////ĳ���� 
        //if (Input.GetButtonUp("Horizontal"))
        //{
        //    rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        //}
        //ĳ���� 

        //ĳ���Ͱ� �÷���������2D���� �ö������� �������� ������ ������ ��������
        //�ϴ�����

        //����
        //if (JumpCount < 2)
        //{


        //    if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        //    { //�ϴ� ����

        //        rigid.AddForce(Vector2.up * 100.0f);

        //        if(Playerhit.collider != null)
        //        {
        //            if (Playerhit.collider.name.Contains("Ground"))
        //                return;

        //        }

        //        //platObject.rotationalOffset = 180.0f;
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Space))
        //    {  //��� ����
        //        rigid.AddForce(transform.up * JumpForce);
        //        JumpCount++;
        //        animator.SetBool("jumping", true);
        //    }

            
        //        //platObject.rotationalOffset = 0.0f;
        //}
        //����
        //ĳ���� ����
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
        //ĳ���� ����



        //�ٴ� ����ĳ��Ʈüũ
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
        //�ٴ� ����ĳ��Ʈüũ


        


        //Debug.Log(ShotDir);


        //���⸦ ȸ����Ű�� ���� ����
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

        //���⸦ ȸ����Ű�� ���� ����




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

            if(PlayerGround.GroundMin.x + 0.5f > this.transform.position.x)     //�÷��̾ ����� �������� ��������ϸ�
            {
                Vector3 pos = this.transform.position;
                pos.x = PlayerGround.GroundMin.x + 0.5f;
                this.transform.position = pos;
            }

            if(PlayerGround.GroundMin.y + 0.7f > this.transform.position.y)        //�÷��̾��� y����ǥ�� �����ϴ��� y�ຸ�� �۾������ϸ�
            {
                Vector3 pos = this.transform.position;
                pos.y = PlayerGround.GroundMin.y + 0.7f;
                this.transform.position = pos;
            }

            if(PlayerGround.GroundMin2.y < this.transform.position.y)           //�÷��̾��� y����ǥ�� ��������� y�ຸ�� Ŀ�����ϸ�
            {
                Vector3 pos = this.transform.position;
                pos.y = PlayerGround.GroundMin2.y;
                this.transform.position = pos;
            }

            if(PlayerGround.GroundMax.x - 0.5f < this.transform.position.x)     //�÷��̾ ���������� ������� �ϸ�
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
