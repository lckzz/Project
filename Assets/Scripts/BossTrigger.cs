using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
public class BossTrigger : MonoBehaviour
{

    BoxCollider2D boxColl;
    public bool TriggerCheck = false;
    public PlayableDirector Pd;
    CinemachineBrain CineBrain;
   
    // Start is called before the first frame update
    void Start()
    {
        CineBrain = Camera.main.GetComponent<CinemachineBrain>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Pd.time >= Pd.duration - 0.1f)       //타임라인이 끝이나면 메인카메라에 붙은 시네머신브레인을 꺼준다.
        {                                       //시네머신브레인이 켜져있으면 플레이어한테 카메라가 붙지않는데 이걸 해결못함ㅠㅠ
            CineBrain.enabled = false;          //타임라인이 끝나면 끄고 다른 타임라인이 시작할때 켜주는식으로 구현
            BossInGameMgr.Inst.BossAppear = false;       //타임라인이 끝나면 움직임을 다시 풀어준다.
        }

    }


    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {

            BossInGameMgr.Inst.audio.clip = BossInGameMgr.Inst.Adclip[1];
            BossInGameMgr.Inst.audio.Play();
            boxColl.enabled = false;
            
            Pd.gameObject.SetActive(true);
            CineBrain.enabled = true;           //트리거를 나가면 시네머신브레인을 다시 켜주고
            BossInGameMgr.Inst.BossAppear = true;       //트리거를 나가면 보스등장으로인한 움직임 제어
            Pd.Play();                          //타임라인 시작
            
        }
    }
}
