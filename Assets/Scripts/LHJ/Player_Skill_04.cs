using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Player_Skill_04 : MonoBehaviour, IPlayer_Skill
{ // 레이저 빔
    private LineRenderer LineRender;

    public float Damage = 1.0f;
    public float Distance = 15.0f;
    public float Delay = 0.25f;
    public float LiveTime = 2.0f;
    public GameObject Owner = null;

    // Start is called before the first frame update
    void Start()
    {
        LineRender = this.GetComponent<LineRenderer>();
        InvokeRepeating("Attack", 0.0f, Delay);
        Destroy(this.gameObject, LiveTime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, Distance))
        {
            LineRender.SetPosition(0, this.transform.position);
            LineRender.SetPosition(1, hit.point);
        }
        else
        {
            LineRender.SetPosition(0, this.transform.position);
            LineRender.SetPosition(1, transform.position + (transform.TransformDirection(Vector3.forward) * Distance));
        }
    }

    void Attack()
    {
        RaycastHit hit;
        IHit hit2;
        Player pl;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, Distance))
        {
            LineRender.SetPosition(0, this.transform.position);
            LineRender.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Player"))
                return;

            if (hit.collider.gameObject.TryGetComponent<IHit>(out hit2))
            {
                //Debug.LogFormat("{0} Damaged {1}", hit.collider.gameObject.name, Damage);
                hit2.GetDamaged(Damage, 0);
                if (Owner != null && Owner.TryGetComponent<Player>(out pl))
                    pl.lastEnemy = hit.collider.gameObject;
            }
            else if (hit.collider.gameObject.transform.parent != null)
            {
                if (hit.collider.gameObject.transform.parent.TryGetComponent<IHit>(out hit2))
                {
                    //Debug.LogFormat("{0} Damaged {1}", hit.collider.gameObject.name, Damage);
                    hit2.GetDamaged(Damage, 0);
                    if (Owner != null && Owner.TryGetComponent<Player>(out pl))
                        pl.lastEnemy = hit.collider.gameObject;
                }
            }
        }
        else
        {
            LineRender.SetPosition(0, this.transform.position);
            LineRender.SetPosition(1, transform.position + (transform.TransformDirection(Vector3.forward) * Distance));
            //Debug.DrawRay(transform.position, (transform.TransformDirection(Vector3.forward) * Distance), Color.white, 1000.0f);
        }
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
