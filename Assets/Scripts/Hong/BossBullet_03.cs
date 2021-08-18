using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet_03 : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        StartCoroutine("Attack", 2);
    }
    IEnumerator Attack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        MonBullet_02();
        StartCoroutine("Attack", 2);
    }

    public Transform firePos;

    public GameObject BulletObj;
    public float bulletSpeed;
    public void MonBullet_01()
    {
        GameObject bullet = Instantiate(BulletObj);
        bullet.SetActive(true);
        bullet.transform.position = firePos.position;
        bullet.transform.rotation = firePos.rotation;
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(firePos.forward * bulletSpeed, ForceMode.Impulse);
    }
    public void MonBullet_02()
    {
        for (int i = 0; i < 5; i++)
        {
            firePos.localEulerAngles = new Vector3(0, 45 - (i * 22.5f), 0);
            MonBullet_01();
        }
        firePos.localEulerAngles = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        IHit ihit;
        ihit = collision.gameObject.GetComponent<IHit>();

        if (ihit != null)
        {
            //StopCoroutine(Attack());
        }
    }
}
