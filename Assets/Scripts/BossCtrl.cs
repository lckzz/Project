using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossType
{
    Boss1,
    Boss2,
    Boss2Shadow     //보스2의 분신들
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
    Attack,     //플레이어가 있는위치로 도약후 내려찍기 공격 
    Blink,
    Skill1, //파이어볼
    Skill2, //분신 파이어볼
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
    float Delay = 0.0f;     //첫번째공격 딜레이
    float BlinkTimer = 0.0f;
    float BlinkCoolTime = 5.0f;     //블링크 쿨타임
    float BlinkOffTime = 0.0f;      //블링크 끝나고 난후
    float SpawnCoolTime = 0.0f;     //잡몹 소환 쿨타임
    int AttackCount = 0;
    int Randomidx = 0;
    int Summoneridx = 0;        //소환한 몹의 갯수
    bool Patten2 = false;       //피 50퍼미만일때 새로운 패턴 스위치
    GameObject SummonerPrefab;
    GameObject SummonerPos;
    Transform[] SumPos;
    int SumRandidx = 0;
    LayerMask DownLayer;


    //보스2
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
        SumPos = SummonerPos.GetComponentsInChildren<Transform>();      //잡몹 소환 위치
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

        if (BsType == BossType.Boss1)        //첫번째 보스일때
        {
            if(BSState == BossState.Trace)
            {
                //보스1의 움직임 구현
                PlayerDistance();
                this.transform.position += VecDir * Time.deltaTime * Speed;
                ani.SetBool("run", true);
                //Debug.Log(VecDir);
                //거리와 방향을 이용해서 보스이동구현
                if (sr.flipX == true && VecDir.x > 0.0f)
                    sr.flipX = false;


                if (sr.flipX == false && VecDir.x < 0.0f)
                    sr.flipX = true;

                


                if (Distance <= 2.0f)
                {
                    //공격
                    BSState = BossState.Attack;
                    ani.SetBool("run", false);

                }

                else
                {
                    if(Patten2 == false)
                    {
                        if (BlinkCoolTime <= 0.0f)       //근데 블링크 쿨타임이 돌고 스폰 쿨타임이 안돌았을때 블링크씀
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
                        if (BlinkCoolTime <= 0.0f && SpawnCoolTime >= 10.0f)       //근데 블링크 쿨타임이 돌고 스폰 쿨타임이 안돌았을때 블링크씀
                        {
                            Debug.Log("패턴2");
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
                this.transform.position = this.transform.position;      //현재 위치에서 고정

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
                    if (Delay <= 0.0f && AttackCount == 0)        //첫타
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

            if (CurHp < MaxHp / 2.0f)   //체력이 50퍼미만이 되면 패턴2 스위치 온
            {
                Patten2 = true;
            }

            //Debug.Log(BlinkOffTime);
            if (SpawnCoolTime <= 0.0f && Patten2 == true)
            {
                //쿨타임이 돌면 
                BSState = BossState.Spawn;
            }

            if (BSState == BossState.Spawn)
            {
                if (Summoneridx >= 5)    //5마리가 넘으면 다시 추격상태 및 쿨타임 30초
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
                    Summoneridx++;      //1.5초마다 잡몹하나씩 소환
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
                    StartCoroutine(DelayCo(3.0f));      //아이들상태라면 딜레이시간을 주고 다시 어택상태로..

                }

            }

            else if (BSState2 == Boss2State.Attack)     //공격일때 
            {

                PlayerDistance();       //플레이어의 위치를 체크 및 정보
                //거리와 방향을 이용해서 보스이동구현
                if (sr.flipX == true && VecDir.x > 0.0f)            //캐릭을 바라보도록
                    sr.flipX = false;

                if (sr.flipX == false && VecDir.x < 0.0f)     //캐릭을 바라보도록
                    sr.flipX = true;

                if (!AttackDelay)       //공격딜레이가 없으면
                {
                    Debug.Log(AttackDelay);

                    AttCheck = true;
                    Jumpani(1400.0f);                //점프 도약(점프 애니메이션)
                    if (groundhit.collider != null)
                    {
                        if (groundhit.collider.name.Contains("Boss2Platform1"))
                            MonPlatform[0].rotationalOffset = 0;
                        else if (groundhit.collider.name.Contains("Boss2Platform2"))
                            MonPlatform[1].rotationalOffset = 0;
                        else if (groundhit.collider.name.Contains("Boss2Platform3"))
                            MonPlatform[2].rotationalOffset = 0;

                    }
                    StartCoroutine(DelayCo(5.0f));  //공격 딜레이
                    


                    

                }


                

                if (rbody.velocity.y <= -3.0f)                  //리지드바디의y축 속도가 밑으로 2.0내려가면 플랫폼 원상복귀
                {
                    for(int ii = 0; ii < MonPlatform.Length; ii++)
                    {
                        MonPlatform[ii].rotationalOffset = 0.0f;
                    }
                }
                //MonPlatform.rotationalOffset = 0.0f;
                if (rbody.velocity.y <= 18.0f && rbody.velocity.y >= 17.0f)     //내려오기시작하면 다운어택
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

                            Debug.Log("딜레이");

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
                    transform.position = Vector3.Lerp(transform.position, AttPosition, Time.deltaTime * 1.7f); //도약공격시 플레이어의 위치를 저장한 위치로 이동

                //if (groundhit.collider != null && animalCtrl.Playerhit.collider != null)
                //{
                //    if ((
                //        groundhit.collider.name == animalCtrl.Playerhit.collider.name) ||
                //        (
                //        groundhit.collider.name.Contains("Platform") && animalCtrl.Playerhit.collider.name.Contains("Platform"))) //낙하하면서 땅을 밟기 직전일때
                //    {
                //        if (Delay <= 0.0f)
                //        {
                //            ani.SetTrigger("groundAtt");
                //            GroundAttCheck = true;
                //            Delay = 0.9f;

                //            Debug.Log("딜레이");

                //        }
                //    }

                //    if (Delay <= 0.1f && GroundAttCheck == true)
                //    {
                //        Debug.Log("공격하고 딜레이가 0수준이면");

                //        //ani.Play("Idle");
                //        BSState2 = Boss2State.Idle;
                //        GroundAttCheck = false;

                //    }
                    //if ((groundhit.collider.name != animalCtrl.Playerhit.collider.name && Delay <= 0.0f))
                    //{
                    //    Debug.Log("점프를 안하거나 플레이어와 밟는땅이 다름");

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
                if (BSState == BossState.Blink)      //블링크상태라면
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
        BlinkCoolTime = 10.0f;  //블링크 쿨타임
        sr.enabled = true;      //렌더러 켜주고
        //capColl.enabled = true; //콜라이더 켜주고
        this.gameObject.layer = LayerMask.NameToLayer("Monster");

        TargetPos = Playerobj.transform.position;
        //블링크오프때 플레이어의 거리를 먼저 알아내고
        TargetPos.x = TargetPos.x + Dis;        //플레이어의 x축거리에서 DIs만큼 더해주고
        Debug.Log("플레이어pos" + Playerobj.transform.position);
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
        Debug.Log("플레이어pos" + Playerobj.transform.position);
        Debug.Log("블링크될 pos" + TargetPos);
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
        Debug.Log("머냐");
        ani.SetTrigger("TakeOff");
        ani.SetTrigger("raise");




    }

    IEnumerator DelayCo(float Delay)
    {
        //Debug.Log(AttackDelay);

        yield return new WaitForSeconds(Delay);
        if (BSState2 == Boss2State.Attack)
            AttackDelay = false;

        else if (BSState2 == Boss2State.Idle)// 딜레이 시간이 끝나고 보스2의 상태가 아이들상태라면 공격상태로 전환
        {
            IdleDelay = false;
            BSState2 = Boss2State.Attack;


        }
    }
}
