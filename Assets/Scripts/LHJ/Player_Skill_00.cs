using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_Skill_00 : MonoBehaviour, IPlayer_Skill
{ // 발사체
    public float Damage = 1.0f; // 데미지 값
    public float Speed = 10.0f; // 속도
    public int Type = 0; // 타입
    public bool isTurn = false;
    public GameObject Owner = null;
    public GameObject hitPrefab = null;
    private Rigidbody rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision co)
    {
        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot) as GameObject;

            var ps = hitVFX.GetComponent<ParticleSystem>();
            if (ps == null)
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
            else
                Destroy(hitVFX, ps.main.duration);
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
            return;
        IHit hit2;
        IPlayer_Skill ps;
        Player pl;
        if (hit.CompareTag("Bullet"))
            return;
        if (hit.TryGetComponent<IHit>(out hit2))
        {
            hit2.GetDamaged(Damage, 0); // 데미지 주기
            if (Owner != null && Owner.TryGetComponent<Player>(out pl))
                pl.lastEnemy = hit.gameObject;
        }

        Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.transform.forward);
        Vector3 pos = this.transform.position;

        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot) as GameObject;

            var pcs = hitVFX.GetComponent<ParticleSystem>();
            if (pcs == null)
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
            else
                Destroy(hitVFX, pcs.main.duration);
        }

        Destroy(this.gameObject,0.0f);
    }

    private void FixedUpdate() // 탄 이동
    {
        this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        //rb.velocity = (transform.forward * Speed);
        if (isTurn)
            this.transform.GetChild(0).rotation = this.transform.GetChild(0).rotation * Quaternion.Euler(Random.Range(0, 10), 0, 0);
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
