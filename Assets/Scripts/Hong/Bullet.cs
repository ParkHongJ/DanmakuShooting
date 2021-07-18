using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //총알 속도
    public float speed;
    //총알 데미지
    public int damage;
    //타겟
    public Transform target;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("부딪힘");
        if (other.CompareTag("player"))
        {

            Debug.Log("콜리전들어감");
            Destroy(gameObject);

        }
    }
}
