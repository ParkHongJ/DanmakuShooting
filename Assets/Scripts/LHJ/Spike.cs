using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float Damage = 10.0f;
    public GameObject Owner;
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

    public void Attack() // 애니메이션 트리거
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(this.transform.position.x, this.transform.position.y - 0.3f, this.transform.position.z), 0.8f);
        IHit hit2;
        Player pl;
        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent<IHit>(out hit2))
            {
                Debug.LogFormat("{0} Dmaged {1}", hit.name, Damage);
                hit2.GetDamaged(Damage, 0);
                if (Owner != null && Owner.TryGetComponent<Player>(out pl))
                    pl.lastEnemy = hit.gameObject;
            }
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
