using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFire : AttackPattern
{
    public GameObject BulletObj;
    public Transform firePos;
    public float bulletSpeed = 30;

    private void Start()
    {
     //   BulletObj.GetComponent<Bullet>().damage = damage;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    public override void Attack()
    {
        GameObject bullet = Instantiate(BulletObj);
        bullet.SetActive(true);
        bullet.transform.position = firePos.position;
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}
