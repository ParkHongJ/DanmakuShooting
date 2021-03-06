using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_02 : MonoBehaviour
{ // 모래늪
    public float Height = 1.0f;
    public float Radius = 1.1f;
    public float Delay = 0.25f;
    public float LiveTime = 1.25f;
    public float Damage = 1.0f;
    public GameObject Owner = null;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Attack",0.0f,Delay); // 공격 판정 반복
        Death();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(this.transform.position.x, this.transform.position.y + Height, this.transform.position.z), Radius);
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

    void Death()
    {
        Destroy(this.gameObject, LiveTime);
    }
}
