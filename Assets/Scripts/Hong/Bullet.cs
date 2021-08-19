using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //총알 속도
    public float speed;
    //총알 데미지
    public int damage = 10; 
    enum AttackType
    {
        Ground = 1, //땅인지
        Sky //하늘인지
    }
    AttackType attackType = AttackType.Ground;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            return;
        }
        IHit ihit = other.GetComponent<IHit>();
        if ( ihit != null )
        {
            ihit.GetDamaged(damage, (int)attackType);
            Destroy(gameObject);
        }
        if ( other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}

    }
}
