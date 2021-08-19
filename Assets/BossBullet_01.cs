using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet_01 : MonoBehaviour
{
    public GameObject BulletEffect;
    public GameObject ExplodedEffect;
    public float Damage;
    private void OnTriggerEnter(Collider other)
    {
        IHit ihit = other.GetComponent<IHit>();
        if (ihit != null)
        {
            ihit.GetDamaged(Damage);
        }
        //총알이펙트를 끄고
        BulletEffect.SetActive(false);
        //폭발이펙트를 킴
        ExplodedEffect.SetActive(true);
    }
}
