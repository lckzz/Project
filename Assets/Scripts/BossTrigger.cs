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
        if(Pd.time >= Pd.duration - 0.1f)       //Ÿ�Ӷ����� ���̳��� ����ī�޶� ���� �ó׸ӽź극���� ���ش�.
        {                                       //�ó׸ӽź극���� ���������� �÷��̾����� ī�޶� �����ʴµ� �̰� �ذ���ԤФ�
            CineBrain.enabled = false;          //Ÿ�Ӷ����� ������ ���� �ٸ� Ÿ�Ӷ����� �����Ҷ� ���ִ½����� ����
            BossInGameMgr.Inst.BossAppear = false;       //Ÿ�Ӷ����� ������ �������� �ٽ� Ǯ���ش�.
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
            CineBrain.enabled = true;           //Ʈ���Ÿ� ������ �ó׸ӽź극���� �ٽ� ���ְ�
            BossInGameMgr.Inst.BossAppear = true;       //Ʈ���Ÿ� ������ ���������������� ������ ����
            Pd.Play();                          //Ÿ�Ӷ��� ����
            
        }
    }
}
