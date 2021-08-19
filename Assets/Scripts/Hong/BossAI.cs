using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float bulletSpeed;
    public Transform firePos;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Patrol();
    }
    float ElapsedTime;
    void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (hp > 0)
        {
            if ( ElapsedTime >= GlobalDelay )
            {
                ElapsedTime = 0f;
                animator.SetTrigger("Attack");
                Attack();
            }
            if (isMove)
            {
                //도착했으면
                if (Vector3.Distance(transform.position, destination) <= 1f)
                {
                    isMove = false;
                    Patrol();   
                    return;
                }
                transform.forward = destination;
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            }
        }
        else
        {
            Die();
        }
    }
    int Track1 = 0;
    int Track2 = 0;
    int Track3 = 0;
    bool IsArrival;
    void Attack()
    {

        CanAttack = false;
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
    bool CanAttack = true;
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
        animator.SetTrigger("Death");
        Destroy(this);
    }
    IEnumerator BBullet_02()
    {
        for (int i = 0; i < 9; i++)
        {
            firePos.localEulerAngles = new Vector3(0, 60 - (i * 15), 0);
            MonBullet_01(BossBullet2);
            yield return new WaitForSeconds(.5f);
        }
        firePos.localEulerAngles = Vector3.zero;
        yield return null;
    }
    public void GetDamaged(float damaged)
    {
        throw new System.NotImplementedException();
    }
    public void GetDamaged(float damaged, int Type)
    {
        if( (int)monsterType != Type)
        {
            hp -= (damaged / 2);
        }
        else
        {
            hp -= damaged;
        }
    }
}
