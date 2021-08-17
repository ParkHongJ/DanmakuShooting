using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_00 : MonoBehaviour
{
    public float Damage = 1.0f; // 데미지 값
    public float Speed = 10.0f; // 속도
    public int Type = 0; // 타입
    public GameObject Owner = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
            return;
        IHit hit2;
        Player pl;
        if (hit.TryGetComponent<IHit>(out hit2))
        {
            hit2.GetDamaged(Damage, 0); // 데미지 주기
            if (Owner != null && Owner.TryGetComponent<Player>(out pl))
                pl.lastEnemy = hit.gameObject;
        }
        Destroy(this.gameObject);
    }

    private void FixedUpdate() // 탄 이동
    {
        this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}
