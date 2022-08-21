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
        //들고있는 무기의 애니메이션을 가지고 오기
        ani = GetComponentInChildren<Animator>();
        //하위에 있는 무기의 목록가져오기


        Debug.Log(Weapons.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShotCtrl.weapon = Weapon.Pistol;
            JudgeWeapon(ShotCtrl.weapon);
            //들고있는 무기의 애니메이션을 가지고 오기
            ani = GetComponentInChildren<Animator>();

        }

        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShotCtrl.weapon = Weapon.Revolver;
            JudgeWeapon(ShotCtrl.weapon);
            //들고있는 무기의 애니메이션을 가지고 오기
            ani = GetComponentInChildren<Animator>();
        }

        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShotCtrl.weapon = Weapon.Rifile;
            JudgeWeapon(ShotCtrl.weapon);
            //들고있는 무기의 애니메이션을 가지고 오기
            ani = GetComponentInChildren<Animator>();

        }
    }

    public void WeaponAnimation(bool WeaponCheck)
    {
        ani.SetBool("shot", WeaponCheck);
    }


    void JudgeWeapon(Weapon wp) //무기종류에서 무기를 판단하는함수
    {
        for (int ii = 0; ii < Weapons.Length; ii++)
        {
            if (ii == (int)wp)  //만약 무기들중에서 지금 무기의종류가 맞다면
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
