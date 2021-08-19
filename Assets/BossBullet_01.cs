using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet_01 : MonoBehaviour
{
    public GameObject BulletEffect;
    public GameObject ExplodedEffect;
    private void OnTriggerEnter(Collider other)
    {
        //총알이펙트를 끄고
        BulletEffect.SetActive(false);
        //폭발이펙트를 킴
        ExplodedEffect.SetActive(true);
    }
}
