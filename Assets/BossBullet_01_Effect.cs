using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet_01_Effect : MonoBehaviour
{
    public GameObject ParentObject;
    public float Damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explosion());
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3f);
        Destroy(ParentObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        IHit ihit = other.GetComponent<IHit>();

        if( ihit != null)
        {
            StartCoroutine(GetDamage(ihit));
        }
    }
    IEnumerator GetDamage(IHit _ihit)
    {
        _ihit.GetDamaged(Damage);
        yield return new WaitForSeconds(1f);
        _ihit.GetDamaged(Damage);
        yield return new WaitForSeconds(1f);
        _ihit.GetDamaged(Damage);
        yield return new WaitForSeconds(1f);
    }
}
