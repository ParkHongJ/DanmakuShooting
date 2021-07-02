using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject BulletObj;
    public GameObject player;
    public Transform firePos;
    public float bulletSpeed = 10;
    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(vec);
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(BulletObj);
            bullet.SetActive(true);
            bullet.transform.position = firePos.position;
            Rigidbody rigid = bullet.GetComponent<Rigidbody>();
            rigid.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}
