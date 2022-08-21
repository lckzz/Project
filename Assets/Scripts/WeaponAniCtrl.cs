using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAniCtrl : MonoBehaviour
{
    Animator ani;
    public GameObject[] Weapons;
    // Start is called before the first frame update
    void Start()
    {
        //����ִ� ������ �ִϸ��̼��� ������ ����
        ani = GetComponentInChildren<Animator>();
        //������ �ִ� ������ ��ϰ�������


        Debug.Log(Weapons.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShotCtrl.weapon = Weapon.Pistol;
            JudgeWeapon(ShotCtrl.weapon);
            //����ִ� ������ �ִϸ��̼��� ������ ����
            ani = GetComponentInChildren<Animator>();

        }

        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShotCtrl.weapon = Weapon.Revolver;
            JudgeWeapon(ShotCtrl.weapon);
            //����ִ� ������ �ִϸ��̼��� ������ ����
            ani = GetComponentInChildren<Animator>();
        }

        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShotCtrl.weapon = Weapon.Rifile;
            JudgeWeapon(ShotCtrl.weapon);
            //����ִ� ������ �ִϸ��̼��� ������ ����
            ani = GetComponentInChildren<Animator>();

        }
    }

    public void WeaponAnimation(bool WeaponCheck)
    {
        ani.SetBool("shot", WeaponCheck);
    }


    void JudgeWeapon(Weapon wp) //������������ ���⸦ �Ǵ��ϴ��Լ�
    {
        for (int ii = 0; ii < Weapons.Length; ii++)
        {
            if (ii == (int)wp)  //���� ������߿��� ���� ������������ �´ٸ�
            {
                Weapons[ii].SetActive(true);
            }
            else
            {
                Weapons[ii].SetActive(false);
            }
        }
    }
}
