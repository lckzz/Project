using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    
    Vector3 BulletDir = Vector3.zero;
    Rigidbody2D rb;
    GameObject[] BullethitFx;
    SpriteRenderer sprend;

    private void Awake()
    {
        BullethitFx = new GameObject[2];
    }

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        sprend = GetComponent<SpriteRenderer>();
        for(int ii = 0; ii < BullethitFx.Length; ii++)
        {
            BullethitFx[ii] = Resources.Load<GameObject>("bullet_hit_" + (ii + 1).ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rbody.AddForce(Vector2.right * BulletSpeed);
        if(ShotCtrl.weapon == Weapon.Pistol)
        {

        }

        BulletDir.Normalize();

        rb.AddForce(BulletDir * 30.0f);

    }

    public void BulletGetDir(Vector3 Dir)
    {
        BulletDir = Dir;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Monster")
        {
            if(ShotCtrl.weapon == Weapon.Pistol)    //¹«±â°¡ ±ÇÃÑÀÌ¶ó¸é
            {
                sprend.enabled = false;     //ÃÑ¾ËÀº ²ô°í 
                GameObject BulletFx = (GameObject)Instantiate(BullethitFx[0]);
                BulletFx.transform.position = this.transform.position;
                Destroy(BulletFx, 1.0f);
            }

            else if(ShotCtrl.weapon == Weapon.Revolver)
            {
                sprend.enabled = false;
                GameObject BulletFx = (GameObject)Instantiate(BullethitFx[0]);
                BulletFx.transform.position = this.transform.position;
                Destroy(BulletFx, 1.0f);
            }

            else if (ShotCtrl.weapon == Weapon.Rifile)
            {
                sprend.enabled = false;
                GameObject BulletFx = (GameObject)Instantiate(BullethitFx[1]);
                BulletFx.transform.position = this.transform.position;
                Destroy(BulletFx, 1.0f);
            }
        }
    }


}
