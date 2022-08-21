using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShotCtrl : MonoBehaviour
{
    public static Weapon weapon = Weapon.Rifile;
    GameObject[] BulletPrefab;
    GameObject BulletShellPrefab;
    GameObject[] ShotPrefab;
    GameObject Bullets;
    public Transform[] firePos;                     //총발사위치
    public Transform BulletshellPos;                //탄피위치
    float ShootDelay = 0.0f;
    WeaponAniCtrl WeaponAni;
    bool WeaponAniCheck = false;
    Vector3 MousePos;
    Vector2 ShotDir = Vector2.zero;
    float Distance = 0.0f;
    public Vector3 Dir;
    Vector3 Shotnormal;
    // Start is called before the first frame update

    private void Awake()
    {
        ShotPrefab = new GameObject[2];     //2개의 총 발사이펙트(추후 조정)
        BulletPrefab = new GameObject[3];   //3개의 총알(추후 추가)
    }

    void Start()
    {


        for(int i = 0; i < BulletPrefab.Length; i++)
        {
            BulletPrefab[i] = Resources.Load<GameObject>("bullet_" + (i + 1).ToString());
        }

        BulletShellPrefab = Resources.Load<GameObject>("bulletshell");
        for(int i = 0; i < ShotPrefab.Length; i++)
        {
            ShotPrefab[i] = Resources.Load<GameObject>("shot_" + (i + 1).ToString());
        }
        Bullets = GameObject.Find("Bullets");
        WeaponAni = GetComponentInChildren<WeaponAniCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < ShotPrefab.Length; i++)
        {
            //Debug.Log(ShotPrefab[i]);
        }
        if(weapon == Weapon.Pistol)
        {
            ShotDirFunc(weapon);
        }
        else if(weapon == Weapon.Revolver)
        {
            ShotDirFunc(weapon);
        }
        else if(weapon == Weapon.Rifile)
        {
            ShotDirFunc(weapon);
        }




        if (Distance > 1.0f)
        {
            //총 발사
            if (weapon == Weapon.Pistol)
                Shoot(0.3f);
            else if (weapon == Weapon.Revolver)
                Shoot(0.4f);
            else if (weapon == Weapon.Rifile)
                Shoot(0.15f);

            //총 발사
        }
        else
        {
            WeaponAniCheck = false;
            WeaponAni.WeaponAnimation(WeaponAniCheck);
        }

    }


    public void Fire(Vector3 pos, Weapon wp)        //총기 종류에  따라서 총알,탄피,이펙트가 다르게 조정하는함수
    {
        if(wp == Weapon.Pistol)
        {
            GameObject Bullet = (GameObject)Instantiate(BulletPrefab[0], firePos[(int)weapon].position, firePos[(int)weapon].rotation);
            //Bullet.transform.localPosition =this.transform.localPosition;
            //Bullet.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            Bullet.GetComponent<BulletCtrl>().BulletGetDir(pos);
            Destroy(Bullet, 3.0f);

            GameObject BulletShell = (GameObject)Instantiate(BulletShellPrefab, BulletshellPos.position, BulletshellPos.rotation);
            BulletShell.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            Destroy(BulletShell, 0.5f);

            GameObject shotFx1 = (GameObject)Instantiate(ShotPrefab[0], firePos[(int)weapon].transform);
            shotFx1.transform.position = firePos[(int)weapon].position;
            Destroy(shotFx1, 0.3f);
        }

        else if(wp == Weapon.Revolver)
        {
            GameObject Bullet = (GameObject)Instantiate(BulletPrefab[1], firePos[(int)weapon].position, firePos[(int)weapon].rotation);
            Bullet.GetComponent<BulletCtrl>().BulletGetDir(pos);
            Destroy(Bullet, 3.0f);

            GameObject BulletShell = (GameObject)Instantiate(BulletShellPrefab, BulletshellPos.position, BulletshellPos.rotation);
            BulletShell.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            Destroy(BulletShell, 0.5f);

            GameObject shotFx1 = (GameObject)Instantiate(ShotPrefab[0], firePos[(int)weapon].transform);
            shotFx1.transform.position = firePos[(int)weapon].position;
            Destroy(shotFx1, 0.3f);
        }

        else if(wp == Weapon.Rifile)
        {
            GameObject Bullet = (GameObject)Instantiate(BulletPrefab[2], firePos[(int)weapon].position, firePos[(int)weapon].rotation);
            Bullet.GetComponent<BulletCtrl>().BulletGetDir(pos);
            Destroy(Bullet, 3.0f);

            GameObject BulletShell = (GameObject)Instantiate(BulletShellPrefab, BulletshellPos.position, BulletshellPos.rotation);
            BulletShell.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            Destroy(BulletShell, 0.5f);

            GameObject shotFx = (GameObject)Instantiate(ShotPrefab[1], firePos[(int)weapon].transform);
            shotFx.transform.position = firePos[(int)weapon].position;
            Destroy(shotFx, 0.3f);
        }

        


    }

    void Shoot(float Delay)
    {
        if (ShootDelay > 0.0f)
        {
            ShootDelay -= Time.deltaTime;

        }


        if (ShootDelay <= 0.0f)
        {
            WeaponAniCheck = false;
            WeaponAni.WeaponAnimation(WeaponAniCheck);
            if (Input.GetMouseButton(0))
            {
                WeaponAniCheck = true;
                WeaponAni.WeaponAnimation(WeaponAniCheck);
                ShootDelay = Delay;

                Fire(Shotnormal,weapon);
            }
        }

    }


    void ShotDirFunc(Weapon wp)     //총에 종류에 따라 총알발사위치가 다르기때문에 위치조정
    {
        //총을 쏠때 마우스의 위치와 FirePos의 거리를 구하기 위한 값들
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ShotDir = MousePos - firePos[(int)wp].transform.position;
        Distance = ShotDir.magnitude;
        ShotDir.Normalize();
        Debug.DrawLine(firePos[(int)wp].transform.position, MousePos);
        Shotnormal = WeaponAni.transform.right.normalized;   //총의 오른쪽방향벡터값
                                                             //총을 쏠때 마우스의 위치와 FirePos의 거리를 구하기 위한 값들

    }
}
