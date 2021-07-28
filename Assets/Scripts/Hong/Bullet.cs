using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //총알 속도
    public float speed;
    //총알 데미지
    public int damage = 10;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("부딪힘");
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<Enemy>().GetDamaged(damage);
            Debug.Log(damage);
            Destroy(gameObject);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}

    }
}
