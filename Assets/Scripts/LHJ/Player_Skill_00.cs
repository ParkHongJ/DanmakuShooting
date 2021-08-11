using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_00 : MonoBehaviour
{
    public float Damage = 1.0f; // 데미지 값
    public float Speed = 10.0f; // 속도
    public int Type = 0; // 타입

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
        {
            return;
        }
        else if (hit.tag == "Enemy")
        {
            hit.GetComponent<Enemy>().GetDamaged((int)Damage); // 데미지 주기
        }
        Destroy(this.gameObject);
    }

    private void FixedUpdate() // 탄 이동
    {
        this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}
