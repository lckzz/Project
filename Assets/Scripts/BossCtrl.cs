using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossType
{
    Boss1,
    Boss2,
    Boss2Shadow     //����2�� �нŵ�
}

public enum BossState
{
    Idle,
    Trace,
    Attack,
    Blink,
    Spawn
}
public enum Boss2State
{
    Idle,
    Attack,     //�÷��̾ �ִ���ġ�� ������ ������� ���� 
    Blink,
    Skill1, //���̾
    Skill2, //�н� ���̾
}

public class BossCtrl : MonoBehaviour
{
    public BossType BsType = BossType.Boss1;
    public BossState BSState = BossState.Trace;
    public Boss2State BSState2 = Boss2State.Idle;
    GameObject Playerobj;
    Animator ani;
    SpriteRenderer sr;
    CapsuleCollider2D capColl;
    AudioSource audio;
    Rigidbody2D rbody;
    public AudioClip[] Boss1clip;
    RaycastHit2D[] hits = new RaycastHit2D[4];
    AnimalCtrl animalCtrl;
    Vector3 TargetPos = Vector3.zero;
    Vector3 DisVec = Vector3.zero;
    Vector3 VecDir = Vector3.zero;
    float Distance = 0.0f;
    float Speed = 2.5f;
    float Delay = 0.0f;     //ù��°���� ������
    float BlinkTimer = 0.0f;
    float BlinkCoolTime = 5.0f;     //��ũ ��Ÿ��
    float BlinkOffTime = 0.0f;      //��ũ ������ ����
    float SpawnCoolTime = 0.0f;     //��� ��ȯ ��Ÿ��
    int AttackCount = 0;
    int Randomidx = 0;
    int Summoneridx = 0;        //��ȯ�� ���� ����
    bool Patten2 = false;       //�� 50�۹̸��϶� ���ο� ���� ����ġ
    GameObject SummonerPrefab;
    GameObject SummonerPos;
    Transform[] SumPos;
    int SumRandidx = 0;
    LayerMask DownLayer;


    //����2
    float JumpForce = 75.0f;
    int JumpCount = 0;
    bool AttackDelay = false;
    bool IdleDelay = false;
    bool GroundAttCheck = false;
    RaycastHit2D groundhit;
    public PlatformEffector2D[] MonPlatform = new PlatformEffector2D[3];


    AnimatorClipInfo[] clip;
    public AnimationClip aniClip;
    bool TakeOff = true;
    bool AttCheck = false;
    bool DownAtt = false;

    Vector3 AttPosition = Vector3.zero;


    float CurHp = 3000.0f;
    float MaxHp = 3000.0f;
    // Start is called before the first frame update
    void Start()
    {
        Playerobj = GameObject.Find("Player");
        ani = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
        capColl = this.GetComponent<CapsuleCollider2D>();
        audio = this.GetComponent<AudioSource>();
        rbody = this.GetComponent<Rigidbody2D>();
        SummonerPrefab = Resources.Load<GameObject>("MonsterPrefab/Boss1Summoner");
        SummonerPos = GameObject.Find("SummonerSpawnPos");
        SumPos = SummonerPos.GetComponentsInChildren<Transform>();      //��� ��ȯ ��ġ
        animalCtrl = GameObject.Find("AnimalRoot").GetComponent<AnimalCtrl>();
        clip = ani.GetCurrentAnimatorClipInfo(0);
        DownLayer = LayerMask.NameToLayer("Down");

        


    }

    // Update is called once per frame
    void Update()
    {
        if (BossInGameMgr.Inst.BossAppear == true)
            return;


        hits[0] = Physics2D.Raycast(transform.position, Vector2.up, 10, LayerMask.GetMask("Player"));
        hits[1] = Physics2D.Raycast(transform.position, Vector2.down, 5, LayerMask.GetMask("Player"));
        hits[2] = Physics2D.Raycast(transform.position, Vector2.left, 5, LayerMask.GetMask("Player"));
        hits[3] = Physics2D.Raycast(transform.position, Vector2.right, 5, LayerMask.GetMask("Player"));

        groundhit = Physics2D.Raycast(transform.position, Vector2.down, 1, (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("MonPlatform")));

        //if (hits[0].collider != null)
            //Debug.Log(hits[0].distance);

        MonsterTimer();

        if (BsType == BossType.Boss1)        //ù��° �����϶�
        {
            if(BSState == BossState.Trace)
            {
                //����1�� ������ ����
                PlayerDistance();
                this.transform.position += VecDir * Time.deltaTime * Speed;
                ani.SetBool("run", true);
                //Debug.Log(VecDir);
                //�Ÿ��� ������ �̿��ؼ� �����̵�����
                if (sr.flipX == true && VecDir.x > 0.0f)
                    sr.flipX = false;


                if (sr.flipX == false && VecDir.x < 0.0f)
                    sr.flipX = true;

                


                if (Distance <= 2.0f)
                {
                    //����
                    BSState = BossState.Attack;
                    ani.SetBool("run", false);

                }

                else
                {
                    if(Patten2 == false)
                    {
                        if (BlinkCoolTime <= 0.0f)       //�ٵ� ��ũ ��Ÿ���� ���� ���� ��Ÿ���� �ȵ������� ��ũ��
                        {
                            Debug.Log("?");
                            BSState = BossState.Blink;
                            ani.SetTrigger("blink");
                            BossSound(Boss1clip, 0);
                            Delay = 1.3f;
                            BlinkTimer = 5.0f;


                        }
                    }
                    
                    else
                    {
                        if (BlinkCoolTime <= 0.0f && SpawnCoolTime >= 10.0f)       //�ٵ� ��ũ ��Ÿ���� ���� ���� ��Ÿ���� �ȵ������� ��ũ��
                        {
                            Debug.Log("����2");
                            BSState = BossState.Blink;
                            ani.SetTrigger("blink");
                            BossSound(Boss1clip, 0);
                            Delay = 1.3f;
                            BlinkTimer = 5.0f;


                        }
                    }

                   
                }
                //StartCoroutine(Trace());

            }

 
            if (BSState == BossState.Attack)
            {
                //PlayerDistance();
                this.transform.position = this.transform.position;      //���� ��ġ���� ����

                if (hits[0].collider != null)
                {
                    if (hits[0].distance <= 1.5f && Delay <= 0.0f)
                    {
                        ani.SetTrigger("AttackUp");
                        BossSound(Boss1clip, 5);
                        AttackCount = 0;
                        Delay = 1.5f;
                        BSState = BossState.Trace;
                    }


                }
                else if(hits[1].collider != null)
                {
                    if (hits[1].distance <= 1.5f && Delay <= 0.0f)
                    {
                        ani.SetTrigger("AttackDown");
                        BossSound(Boss1clip, 3);
                        AttackCount = 0;
                        Delay = 1.0f;
                        BSState = BossState.Trace;
                    }



                }
                else
                {
                    if (Delay <= 0.0f && AttackCount == 0)        //ùŸ
                    {
                        ani.SetTrigger("Attack1");
                        BossSound(Boss1clip, 1);
                        AttackCount++;
                        Delay = 0.3f;
                    }
                    else if (Delay <= 0.0f && AttackCount == 1)
                    {
                        ani.SetTrigger("Attack2");
                        BossSound(Boss1clip, 2);
                        AttackCount++;
                        Delay = 0.3f;
                    }
                    else if (Delay <= 0.0f && AttackCount == 2)
                    {
                        ani.SetTrigger("Attack3");
                        BossSound(Boss1clip, 3);
                        AttackCount++;
                        Delay = 0.3f;
                    }
                    else if (Delay <= 0.0f && AttackCount == 3)
                    {
                        ani.SetTrigger("Attack4");
                        BossSound(Boss1clip, 4);
                        AttackCount++;
                        Delay = 0.5f;
                    }
                    else if (Delay <= 0.0 && AttackCount > 3)
                    {
                        ani.SetTrigger("idle");
                        AttackCount = 0;
                        //Delay = 0.01f;
                        BSState = BossState.Trace;

                    }
                }

                //if (rbody.bodyType == RigidbodyType2D.Kinematic)
                //    rbody.bodyType = RigidbodyType2D.Dynamic;


            }


            if (BSState == BossState.Blink)
            {
                PlayerDistance();



                if (Delay <= 0.0f)
                {
                    sr.enabled = false;
                    this.gameObject.layer = DownLayer;


                    if (Randomidx == 1 && BlinkTimer <= 0.0f)
                        BlinkOffX(1.5f);


                    else if (Randomidx == 2 && BlinkTimer <= 0.0f)
                        BlinkOffX(-1.5f);


                    else if (Randomidx == 3 && BlinkTimer <= 0.0f)
                        BlinkOffY(3.5f);

                }



            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                CurHp = CurHp / 2 - 10.0f;
            }

            if (CurHp < MaxHp / 2.0f)   //ü���� 50�۹̸��� �Ǹ� ����2 ����ġ ��
            {
                Patten2 = true;
            }

            //Debug.Log(BlinkOffTime);
            if (SpawnCoolTime <= 0.0f && Patten2 == true)
            {
                //��Ÿ���� ���� 
                BSState = BossState.Spawn;
            }

            if (BSState == BossState.Spawn)
            {
                if (Summoneridx >= 5)    //5������ ������ �ٽ� �߰ݻ��� �� ��Ÿ�� 30��
                {
                    BSState = BossState.Trace;
                    SpawnCoolTime = 30.0f;
                    return;
                }

                if (Delay <= 0.0f)
                {
                    ani.SetTrigger("Spawn");
                    this.transform.position = this.transform.position;
                    Delay = 1.5f;
                    Summoneridx++;      //1.5�ʸ��� ����ϳ��� ��ȯ
                    GameObject Sum = Instantiate(SummonerPrefab) as GameObject;
                    SumRandidx = Random.Range(1, 8);
                    Sum.transform.position = SumPos[SumRandidx].position;



                }

            }

        }

        else if(BsType == BossType.Boss2)
        {

            if (BSState2 == Boss2State.Idle)
            {
                if (!IdleDelay)
                {
                    IdleDelay = true;
                    ani.SetTrigger("idle");
                    StartCoroutine(DelayCo(3.0f));      //���̵���¶�� �����̽ð��� �ְ� �ٽ� ���û��·�..

                }

            }

            else if (BSState2 == Boss2State.Attack)     //�����϶� 
            {

                PlayerDistance();       //�÷��̾��� ��ġ�� üũ �� ����
                //�Ÿ��� ������ �̿��ؼ� �����̵�����
                if (sr.flipX == true && VecDir.x > 0.0f)            //ĳ���� �ٶ󺸵���
                    sr.flipX = false;

                if (sr.flipX == false && VecDir.x < 0.0f)     //ĳ���� �ٶ󺸵���
                    sr.flipX = true;

                if (!AttackDelay)       //���ݵ����̰� ������
                {
                    Debug.Log(AttackDelay);

                    AttCheck = true;
                    Jumpani(1400.0f);                //���� ����(���� �ִϸ��̼�)
                    if (groundhit.collider != null)
                    {
                        if (groundhit.collider.name.Contains("Boss2Platform1"))
                            MonPlatform[0].rotationalOffset = 0;
                        else if (groundhit.collider.name.Contains("Boss2Platform2"))
                            MonPlatform[1].rotationalOffset = 0;
                        else if (groundhit.collider.name.Contains("Boss2Platform3"))
                            MonPlatform[2].rotationalOffset = 0;

                    }
                    StartCoroutine(DelayCo(5.0f));  //���� ������
                    


                    

                }


                

                if (rbody.velocity.y <= -3.0f)                  //������ٵ���y�� �ӵ��� ������ 2.0�������� �÷��� ���󺹱�
                {
                    for(int ii = 0; ii < MonPlatform.Length; ii++)
                    {
                        MonPlatform[ii].rotationalOffset = 0.0f;
                    }
                }
                //MonPlatform.rotationalOffset = 0.0f;
                if (rbody.velocity.y <= 18.0f && rbody.velocity.y >= 17.0f)     //������������ϸ� �ٿ����
                {
                    if(DownAtt == false)
                    {
                        DownAtt = true;

                    }
                }

                if (DownAtt == true)
                {
                    if (transform.position.y <= AttPosition.y + 1.0f)
                    {
                        DownAtt = false;
                        AttCheck = false;
                        Vector3 pos = transform.position;
                        pos.y = -3.0f;
                        transform.position = pos;
                        rbody.AddForce(Vector2.up * 0.01f);
                        if (Delay <= 0.0f)
                        {
                            ani.SetTrigger("groundAtt");
                            GroundAttCheck = true;
                            Delay = 0.9f;

                            Debug.Log("������");

                        }
                    }
                    transform.Translate(Vector2.down * Time.deltaTime * 25.0f);

                    //if (rbody.velocity.y <= 10.0f)
                    //    AttackCheck = false
                }


                if(AttCheck == false)
                {
     
                    Vector3 pos = transform.position;
                    pos.y = -3.0f;
                    transform.position = pos;

                    
                }
                //if(AttackCheck == false)
                //{

                //}
                //rbody.AddForce(Vector3.down * 50.0f);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    DownAtt = false;
                }

                Debug.Log(DownAtt);
                if (rbody.velocity.y != 0.0f)
                    transform.position = Vector3.Lerp(transform.position, AttPosition, Time.deltaTime * 1.7f); //������ݽ� �÷��̾��� ��ġ�� ������ ��ġ�� �̵�

                //if (groundhit.collider != null && animalCtrl.Playerhit.collider != null)
                //{
                //    if ((
                //        groundhit.collider.name == animalCtrl.Playerhit.collider.name) ||
                //        (
                //        groundhit.collider.name.Contains("Platform") && animalCtrl.Playerhit.collider.name.Contains("Platform"))) //�����ϸ鼭 ���� ��� �����϶�
                //    {
                //        if (Delay <= 0.0f)
                //        {
                //            ani.SetTrigger("groundAtt");
                //            GroundAttCheck = true;
                //            Delay = 0.9f;

                //            Debug.Log("������");

                //        }
                //    }

                //    if (Delay <= 0.1f && GroundAttCheck == true)
                //    {
                //        Debug.Log("�����ϰ� �����̰� 0�����̸�");

                //        //ani.Play("Idle");
                //        BSState2 = Boss2State.Idle;
                //        GroundAttCheck = false;

                //    }
                    //if ((groundhit.collider.name != animalCtrl.Playerhit.collider.name && Delay <= 0.0f))
                    //{
                    //    Debug.Log("������ ���ϰų� �÷��̾�� ��¶��� �ٸ�");

                    //    // ani.Play("Idle");
                    //    BSState2 = Boss2State.Idle;

                    //}

         

                    



                //}
  
  
            }
        }




        //Debug.Log(SpawnCoolTime);
        //Debug.Log(BlinkCoolTime);
        //Debug.Log(Delay);

    }


    void PlayerDistance()
    {
        TargetPos = Playerobj.transform.position;
        DisVec = TargetPos - this.transform.position;
        VecDir = DisVec.normalized;
        Distance = DisVec.magnitude;
    }

    void MonsterTimer()
    {
        if (Delay > 0.0f)
        {

            Delay -= Time.deltaTime;
            if (Delay <= 0.0f)
            {
                Delay = 0.0f;
                if (BSState == BossState.Blink)      //��ũ���¶��
                {
                    Randomidx = Random.Range(1, 4);

                }
            }
        }

        if (BlinkTimer > 0.0f)
        {
            BlinkTimer -= Time.deltaTime;
            if (BlinkTimer <= 0.0f)
            {
                BlinkTimer = 0.0f;
            }
        }

        if (BlinkCoolTime > 0.0f)
        {
            BlinkCoolTime -= Time.deltaTime;
            if (BlinkCoolTime <= 0.0f)
            {
                BlinkCoolTime = 0.0f;
                if (BSState == BossState.Blink)
                {
                    BlinkOffTime = 2.0f;
                }
            }
        }

        if(SpawnCoolTime > 0.0f)
        {
            SpawnCoolTime -= Time.deltaTime;
            if(SpawnCoolTime <= 0.0f)
            {
                SpawnCoolTime = 0.0f;
            }
        }

        if (BlinkOffTime > 0.0f)
        {
            BlinkOffTime -= Time.deltaTime;
            if (BlinkOffTime <= 0.0f)
            {
                BlinkOffTime = 0.0f;
            }
        }
    }

   
    void BossSound(AudioClip[] ac, int num)
    {
        audio.PlayOneShot(ac[num]);
        
    }

    void BlinkOffX(float Dis)
    {
        BlinkCoolTime = 10.0f;  //��ũ ��Ÿ��
        sr.enabled = true;      //������ ���ְ�
        //capColl.enabled = true; //�ݶ��̴� ���ְ�
        this.gameObject.layer = LayerMask.NameToLayer("Monster");

        TargetPos = Playerobj.transform.position;
        //��ũ������ �÷��̾��� �Ÿ��� ���� �˾Ƴ���
        TargetPos.x = TargetPos.x + Dis;        //�÷��̾��� x��Ÿ����� DIs��ŭ �����ְ�
        Debug.Log("�÷��̾�pos" + Playerobj.transform.position);
        Debug.Log(TargetPos);

        this.transform.position = TargetPos;
        if (sr.flipX == true && VecDir.x > 0.0f)
            sr.flipX = false;

        if (sr.flipX == false && VecDir.x < 0.0f)
            sr.flipX = true;


        ani.SetTrigger("Appear");
        Delay = 0.7f;
        BossSound(Boss1clip, 0);

        if (BlinkOffTime <= 0.0f)
        {
            ani.SetTrigger("idle");
            if (Distance <= 2.0f)
                BSState = BossState.Attack;
            else
                BSState = BossState.Trace;
        }
    }

    void BlinkOffY(float Dis)
    {
        BlinkCoolTime = 10.0f;
        sr.enabled = true;
        this.gameObject.layer = LayerMask.NameToLayer("Monster");
        //capColl.enabled = true;

        TargetPos = Playerobj.transform.position;
        TargetPos.y = TargetPos.y + Dis;
        Debug.Log("�÷��̾�pos" + Playerobj.transform.position);
        Debug.Log("��ũ�� pos" + TargetPos);
        this.transform.position = TargetPos;
        if (sr.flipX == true && VecDir.x > 0.0f)
            sr.flipX = false;


        if (sr.flipX == false && VecDir.x < 0.0f)
            sr.flipX = true;
        Debug.Log(Distance);

        ani.SetTrigger("Appear");
        BossSound(Boss1clip, 0);

        if (BlinkOffTime <= 0.0f)
        {
            ani.SetTrigger("idle");
            if (Distance <= 2.0f)
                BSState = BossState.Attack;
            else
                BSState = BossState.Trace;
        }
    }

    void Jumpani(float JumpForce)
    {

        AttackDelay = true;
        AttPosition = Playerobj.transform.position;
        rbody.AddForce(Vector2.up * JumpForce);
        Debug.Log("�ӳ�");
        ani.SetTrigger("TakeOff");
        ani.SetTrigger("raise");




    }

    IEnumerator DelayCo(float Delay)
    {
        //Debug.Log(AttackDelay);

        yield return new WaitForSeconds(Delay);
        if (BSState2 == Boss2State.Attack)
            AttackDelay = false;

        else if (BSState2 == Boss2State.Idle)// ������ �ð��� ������ ����2�� ���°� ���̵���¶�� ���ݻ��·� ��ȯ
        {
            IdleDelay = false;
            BSState2 = Boss2State.Attack;


        }
    }
}
