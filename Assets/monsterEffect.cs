using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterEffect : MonoBehaviour
{
    public float Damage;
    private void Start()
    {
        
    }

    IEnumerator MonsterEffect()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        IHit ihit = other.GetComponent<IHit>();
        if( ihit != null)
        {
            ihit.GetDamaged(Damage);
        }
    }
}
