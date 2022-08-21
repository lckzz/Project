using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCtrl : MonoBehaviour
{
    float v = 0.0f;
    float h = 0.0f;
    float m_Speed = 5.0f;
    float JumpForce = 300.0f;
    float JumpCount = 0;
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

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        spriteRend = this.GetComponentInChildren<SpriteRenderer>();
        rigid = this.GetComponentInChildren<Rigidbody2D>();
        Weapon = GameObject.Find("Weapon");
        Shot = this.GetComponentInChildren<ShotCtrl>();
        WeaponAni = this.GetComponentInChildren<WeaponAniCtrl>();
        //Camera.main.gameObject.GetComponent<CameraCtrl>().PlayerFind(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
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



        //캐릭터 
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        }
        //캐릭터 

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



        if (Mathf.Abs(rigid.velocity.x) < 0.3f)
        {
            animator.SetBool("run", false);
        }
        else
        {
            animator.SetBool("run", true);
        }
        //캐릭터 방향



        //바닥 레이캐스트체크
        if (rigid.velocity.y < 0.0f)
        {
            RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Ground"));
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            if (hit.collider != null)
            {
                if (hit.distance < 0.5f)
                {
                    animator.SetBool("jumping", false);
                    JumpCount = 0;
                }
            }
        }
        //바닥 레이캐스트체크





        //Debug.Log(ShotDir);

        //점프
        if (JumpCount < 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigid.AddForce(transform.up * JumpForce);
                JumpCount++;
                animator.SetBool("jumping", true);
            }
        }
        //점프

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
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxspeed)
        {
            rigid.velocity = new Vector2(maxspeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxspeed * -1)
        {
            rigid.velocity = new Vector2(maxspeed * -1, rigid.velocity.y);
        }

        
    }



}
