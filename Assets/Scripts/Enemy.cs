using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject BulletObj;
    public GameObject player;
    public Transform firePos;
    public float bulletSpeed = 10;
    public GameObject[] bullets;

    //private void Awake()
    //{
    //    for (int i = 0; i < 200; i++)
    //    {
    //        bullets[i] = Instantiate(BulletObj);
    //    }
    //}
    void Update()
    {
        Vector3 vec = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(vec);
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Pattern());
        }
    }
    void Fire()
    {
        GameObject bullet = Instantiate(BulletObj);
        bullet.SetActive(true);
        bullet.transform.position = firePos.position;
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }
    IEnumerator Pattern()
    {
        Fire();
        yield return new WaitForSeconds(.1f);
        Fire();
        yield return new WaitForSeconds(.1f);
        Fire();
        yield return new WaitForSeconds(.1f);
        Fire();
        yield return new WaitForSeconds(.1f);
    }
}
