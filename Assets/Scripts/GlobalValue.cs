using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    Pistol,
    Revolver,
    Rifile
}



//public class MonsterInfo       //������ ���� ����
//{
//    public MonsterType MonType = MonsterType.Crawler;       //������ ����
//    public int MonUniqueid = 0;          //������ ������ȣ
//    public float MonHp = 0.0f;              //������ ü��
//    public float MonAtt = 0.0f;             //������ ���ݷ�
//    public float MonSpeed = 0.0f;           //������ ���ǵ�



//    public void SetType(MonsterType a_MonType)
//    {
//        MonType = a_MonType;
//        if (a_MonType == MonsterType.Crawler)
//        {
//            MonUniqueid = 1000;
//            MonHp = 200.0f;
//            MonAtt = 10.0f;
//            MonSpeed = 2.0f;
//        }

//        else if (a_MonType == MonsterType.Flying)
//        {
//            MonUniqueid = 1001;
//            MonHp = 200.0f;
//            MonAtt = 10.0f;
//            MonSpeed = 2.0f;
//        }

//        else if (a_MonType == MonsterType.FlyingRider)
//        {
//            MonUniqueid = 1002;
//            MonHp = 400.0f;
//            MonAtt = 15.0f;
//            MonSpeed = 3.5f;
//        }

//        else if (a_MonType == MonsterType.Melee1)
//        {
//            MonUniqueid = 1003;
//            MonHp = 500.0f;
//            MonAtt = 15.0f;
//            MonSpeed = 2.5f;
//        }

//        else if (a_MonType == MonsterType.Melee2)
//        {
//            MonUniqueid = 1004;
//            MonHp = 500.0f;
//            MonAtt = 15.0f;
//            MonSpeed = 2.5f;
//        }

//        else if (a_MonType == MonsterType.Ranged)
//        {
//            MonUniqueid = 1005;
//            MonHp = 300.0f;
//            MonAtt = 25.0f;
//            MonSpeed = 1.5f;
//        }

//        else if (a_MonType == MonsterType.Trap1)
//        {
//            MonUniqueid = 1006;
//            MonHp = 500.0f;
//            MonAtt = 30.0f;
//            MonSpeed = 0.0f;
//        }

//        else if (a_MonType == MonsterType.Trap2)
//        {
//            MonUniqueid = 1007;
//            MonHp = 700.0f;
//            MonAtt = 50.0f;
//            MonSpeed = 0.0f;
//        }
//    }
//}

public class GlobalValue
{
    public static int g_UserGold = 0;
    public static int g_Level = 0;

    ////���� ������ ����Ʈ
    //public static List<MonsterInfo> m_MonDataList = new List<MonsterInfo>();

    //public static void InitMonsterData()
    //{
    //    if (0 < m_MonDataList.Count)        //���͵����;ȿ� �Ѱ��� �ִٸ� ����
    //        return;

    //    MonsterInfo a_MonNode;
    //    for (int i = 0; i < (int)MonsterType.Count; i++)
    //    {
    //        a_MonNode = new MonsterInfo();
    //        a_MonNode.SetType((MonsterType)i);
    //        m_MonDataList.Add(a_MonNode);
    //    }
    //}
}
