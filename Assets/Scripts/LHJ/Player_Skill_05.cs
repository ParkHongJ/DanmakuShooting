using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_05 : MonoBehaviour, IPlayer_Skill
{ // 폭발 공격
    public float Height = 1.5f;
    public float Radius = 3f;
    public float Damage = 1.0f;
    public float LiveTime = 1.0f;
    public GameObject Owner = null;
    public GameObject EffectObj = null;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Attack",0.1f);

        if (EffectObj)
            Instantiate(EffectObj, this.transform.position, Quaternion.Euler(Vector3.zero));
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
            if (hit.CompareTag("Player"))
                continue;
            if (hit.TryGetComponent<IHit>(out hit2))
            {
                //Debug.LogFormat("{0} Dmaged {1}", hit.name, Damage);
                hit2.GetDamaged(Damage, 0);
                if (Owner != null && Owner.TryGetComponent<Player>(out pl))
                    pl.lastEnemy = hit.gameObject;

            }
            else if (hit.transform.parent != null)
            {
                if (hit.transform.parent.TryGetComponent<IHit>(out hit2))
                {
                    //Debug.LogFormat("{0} Dmaged {1}", hit.name, Damage);
                    hit2.GetDamaged(Damage, 0);
                    if (Owner != null && Owner.TryGetComponent<Player>(out pl))
                        pl.lastEnemy = hit.gameObject;
                }
            }
        }
    }
    void Death()
    {
        Destroy(this.gameObject, LiveTime);
    }
    GameObject IPlayer_Skill.getOwner()
    {
        return Owner;
    }

    void IPlayer_Skill.setOwner(GameObject _owner)
    {
        this.Owner = _owner;
    }

    float IPlayer_Skill.getDamage()
    {
        return Damage;
    }

    void IPlayer_Skill.setDamage(float _damage)
    {
        Damage = _damage;
    }

}
