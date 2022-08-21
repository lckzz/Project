using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInGameMgr : MonoBehaviour
{
    GameObject Boss1SpawnPos;
    GameObject Boss2SpawnPos;
    BossTrigger BsTrigger;
    [HideInInspector] public AudioSource audio;
    [HideInInspector] public AudioClip[] Adclip = new AudioClip[2];
    public static BossInGameMgr Inst;
    public bool BossAppear = false;      //시네마틱떄 보스가 등장하는 순간
    // Start is called before the first frame update
    private void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        Boss1SpawnPos = GameObject.Find("Boss1SpawnPos");
        Boss2SpawnPos = GameObject.Find("Boss2SpawnPos");
        BsTrigger = FindObjectOfType<BossTrigger>();
        audio = GetComponent<AudioSource>();

        Adclip[0] = Resources.Load<AudioClip>("Story Theme (Extended Version) - Song");
        Adclip[1] = Resources.Load<AudioClip>("Boss Theme 1 - Loop");

        audio.clip = Adclip[0];
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
