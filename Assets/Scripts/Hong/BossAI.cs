using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour, IHit
{
    public enum AI_ENEMY_ATTACK
    {
        BossBullet_01 = 1,
        BossBullet_02,
        BossBullet_03,
        BossBullet_04
    }
    public enum AI_ENEMY_MONSTER_TYPE
    {
        Ground = 1, //땅인지
        Sky //하늘인지
    }
    
    public float hp;

    public Transform[] BaseTrack;
    public Transform[] Track_01;
    public Transform[] Track_02;
    public Transform[] Track_03;
    public AI_ENEMY_MONSTER_TYPE monsterType;
    public AI_ENEMY_ATTACK monsterAttack;
    public GameObject player;
    Animator animator;
    public float speed;
    bool isMove = false;
    Vector3 destination;

    public GameObject BossBullet1;
    public GameObject BossBullet2;
    public GameObject BossBullet3;

    public float bulletSpeed = 5f;
    public Transform firePos;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Patrol();
        //MonBullet_02();
    }
    [SerializeField]
    float ElapsedTime;

    public GameObject t_50, t_25;
    private bool first_50 = true, first_25 = true;
    void Update()
    {
        



        if (hp <= ((hp * 25 ) / 100 ) && UseBullet_5)
        {
            //animator.SetTrigger("Attack_04");
        }
        if (hp > 0)
        {
            if ( ElapsedTime >= GlobalDelay )
            {
                ElapsedTime = 0f;
                Attack();
                //Patrol();
            }
            else if (isMove)
            {
                //도착했으면
                if (Vector3.Distance(transform.position, destination) <= 1f)
                {
                    isMove = false;
                    Patrol();   
                    return;
                }
                transform.LookAt(destination);
                //transform.forward = destination;
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            }
        }
        else
        {
            Die();
        }
    }
    private void LateUpdate()
    {
        if(hp>0)
        {
            ElapsedTime += Time.deltaTime;

        }
    }
    [SerializeField]
    int Track1 = 0;

    [SerializeField]
    int Track2 = 0;

    [SerializeField]
    int Track3 = 0;
    bool IsArrival;

    public GameObject destroywall_1;
    public GameObject destroywall_2;
    public GameObject destroywall_3;
    public GameObject destroywall_4;
    void Attack()
    {
        Debug.Log("공격");
        if(player == null)
        {
            return;
        }
        isMove = false;

        StartCoroutine(Delay(3f));
        transform.LookAt(player.transform);
        if (hp > 50)
        {
            Debug.Log("Pause1");
            int random = Random.Range(1, 3);
            switch(random)
            {
                case 1:
                    animator.SetTrigger("Attack_01");
                    BossBullet_01();
                    break;
                case 2:
                    animator.SetTrigger("Attack_02");
                    MonBullet_02();
                    break;
            }
        }
        else if(hp <= 50 && hp > 25)
        {
            Debug.Log("Pause2");
            int random = Random.Range(1, 4);
            switch(random)
            {
                case 1:
                    animator.SetTrigger("Attack_01");
                    BossBullet_01();
                    break;
                case 2:
                    animator.SetTrigger("Attack_02");
                    MonBullet_02();
                    break;
                case 3:
                    animator.SetTrigger("Attack_02");
                    BossBullet_02();
                    break;
            }
                
        }
        else
        {
            Debug.Log("Pause3");
            int random = Random.Range(1, 5);
            switch (random)
            {
                case 1:
                    animator.SetTrigger("Attack_01");
                    BossBullet_01();
                    break;
                case 2:
                    animator.SetTrigger("Attack_02");
                    MonBullet_02();
                    break;
                case 3:
                    animator.SetTrigger("Attack_02");
                    BossBullet_02();
                    break;
                case 4:
                    animator.SetTrigger("Attack_03");
                    BossBullet_03();
                    break;
            }
        }

        //Invoke("SetDestination", 1f);
    }
    public void BossBullet_05()
    {
        UseBullet_5 = false;
        

    }
    public GameObject MonBullet_2;
    public void MonBullet_02()
    {
        for (int i = 0; i < 5; i++)
        {
            firePos.localEulerAngles = new Vector3(0, 45 - (i * 22.5f), 0);
            MonBullet_01(MonBullet_2);
        }
        firePos.localEulerAngles = Vector3.zero;
    }
    public void SetDestination()
    {
        if( hp > 50)
        {
            if(IsArrival == true)
            {
                //도착했으면
                destination = Track_01[Track1].position;
                Track1--;
                if(Track1 == 0)
                {
                    IsArrival = false;
                }
            }
            else
            {
                destination = Track_01[Track1].position;
                Track1++;
            }
            if(Track1 == 3)
            {
                IsArrival = true;
            }
        }
        else if(hp <= 50 && hp >= 26)
        {
            if (IsArrival == true)
            {
                //도착했으면
                destination = Track_02[Track2].position;
                Track2--;
                if (Track2 == 0)
                {
                    IsArrival = false;
                }
            }
            else
            {
                destination = Track_02[Track2].position;
                Track2++;
            }
            if (Track2 == 3)
            {
                IsArrival = true;
            }
        }
        else
        {
            if (IsArrival == true)
            {
                //도착했으면
                destination = Track_03[Track3].position;
                Track3--;
                if (Track3 == 0)
                {
                    IsArrival = false;
                }
            }
            else
            {
                destination = Track_03[Track3].position;
                Track3++;
            }
            if (Track3 == 3)
            {
                IsArrival = true;
            }
        }
    }
    bool UseBullet_5 = true;
    [Range(0,100)]
    public float GlobalDelay;
    public void Patrol()
    {
        animator.SetTrigger("Patrol");
        SetDestination();
        isMove = true;
    }
    public void MonBullet_01()
    {
        GameObject bullet = Instantiate(BossBullet1);
        bullet.SetActive(true);
        bullet.transform.position = firePos.position;
        bullet.transform.rotation = firePos.rotation;
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(firePos.forward * bulletSpeed, ForceMode.Impulse);
    }

    public void MonBullet_01(GameObject _BulletObj)
    {
        GameObject bullet = Instantiate(_BulletObj);
        bullet.SetActive(true);
        bullet.transform.position = firePos.position;
        bullet.transform.rotation = firePos.rotation;
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(firePos.forward * bulletSpeed, ForceMode.Impulse);
    }
    public void BossBullet_01()
    {
        MonBullet_01(BossBullet1);
    }
    public void BossBullet_02()
    {
        StartCoroutine(BBullet_02());
    }
    public void BossBullet_03()
    {
        for (int i = 0; i < 3; i++)
        {
            firePos.localEulerAngles = new Vector3(0, 45 - (i * 45f), 0);
            MonBullet_01(BossBullet3);
        }
        firePos.localEulerAngles = Vector3.zero;
    }
    public void Die()
    {
        Debug.Log("죽음");
        animator.SetTrigger("Death");
        Destroy(gameObject);
    }
    IEnumerator BBullet_02()
    {
        for (int i = 0; i < 9; i++)
        {
            firePos.localEulerAngles = new Vector3(0, 60 - (i * 15), 0);
            MonBullet_01(BossBullet2);
            yield return new WaitForSeconds(.2f);
        }
        firePos.localEulerAngles = Vector3.zero;
        yield return null;
    }
    IEnumerator Delay(float _Delay)
    {
        yield return new WaitForSeconds(_Delay);
        isMove = true;
    }
    public void GetDamaged(float damaged)
    {
        Debug.Log("getdamaged(float) damaged:");
        Debug.Log(damaged);
        hp -= damaged; 
        if (first_50)
        {
            if (hp < 50f && hp > 25f)
            {
                Debug.Log("11");
                t_50.SetActive(true);
            }

            first_50 = false;
        }
        if (first_25)
        {
            if (hp < 25f)
            {
                Debug.Log("22");
                t_25.SetActive(true);
            }

            first_25 = false;
        }
    }
    public void GetDamaged(float damaged, int Type)
    {
        Debug.Log("getdamaged(float, type) damaged:");
        Debug.Log(damaged);
        if ( (int)monsterType != Type)
        {
            hp -= (damaged / 2);
        }
        else
        {
            hp -= damaged;
        }
        if (first_50)
        {
            if (hp < 50f && hp > 25f)
            {
                Debug.Log("11");
                t_50.SetActive(true);
            }

            first_50 = false;
        }
        if (first_25)
        {
            if (hp < 25f)
            {
                Debug.Log("22");
                t_25.SetActive(true);
            }

            first_25 = false;
        }
    }
}
