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
    public Transform[] firePos;                     //�ѹ߻���ġ
    public Transform BulletshellPos;                //ź����ġ
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
        ShotPrefab = new GameObject[2];     //2���� �� �߻�����Ʈ(���� ����)
        BulletPrefab = new GameObject[3];   //3���� �Ѿ�(���� �߰�)
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
            //�� �߻�
            if (weapon == Weapon.Pistol)
                Shoot(0.3f);
            else if (weapon == Weapon.Revolver)
                Shoot(0.4f);
            else if (weapon == Weapon.Rifile)
                Shoot(0.15f);

            //�� �߻�
        }
        else
        {
            WeaponAniCheck = false;
            WeaponAni.WeaponAnimation(WeaponAniCheck);
        }

    }


    public void Fire(Vector3 pos, Weapon wp)        //�ѱ� ������  ���� �Ѿ�,ź��,����Ʈ�� �ٸ��� �����ϴ��Լ�
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


    void ShotDirFunc(Weapon wp)     //�ѿ� ������ ���� �Ѿ˹߻���ġ�� �ٸ��⶧���� ��ġ����
    {
        //���� �� ���콺�� ��ġ�� FirePos�� �Ÿ��� ���ϱ� ���� ����
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ShotDir = MousePos - firePos[(int)wp].transform.position;
        Distance = ShotDir.magnitude;
        ShotDir.Normalize();
        Debug.DrawLine(firePos[(int)wp].transform.position, MousePos);
        Shotnormal = WeaponAni.transform.right.normalized;   //���� �����ʹ��⺤�Ͱ�
                                                             //���� �� ���콺�� ��ġ�� FirePos�� �Ÿ��� ���ϱ� ���� ����

    }
}
