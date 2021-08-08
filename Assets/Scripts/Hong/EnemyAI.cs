using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : AttackPattern, IHit
{
    public enum AI_ENEMY_STATE
    {
        IDLE = 1,
        PATROL,
        CHASE,
        ATTACK
    };
    public enum AI_ENEMY_ATTACK
    {
        MonBullet_01 = 1,
        MonBullet_02,
        MonBullet_03,
        MonBullet_04
    }
    //현재 상태
    public AI_ENEMY_STATE CurrentState = AI_ENEMY_STATE.IDLE;
    public AI_ENEMY_ATTACK CurrentAttack;
    Animator animator;
    Transform myTransform;
    NavMeshAgent agent;
    Collider myCollider;

    GameObject[] Waypoints;



    //나중에 방에 들어가면 찾는로직 있어야함
    Transform playerTransform;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        myTransform = transform;
        myCollider = GetComponent<BoxCollider>();

        //플레이어 트랜스폼 얻기 (임시)
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        Waypoints = GameObject.FindGameObjectsWithTag("WayPoints");
    }
    private void Start()
    {

        StartCoroutine(State_Patrol());
    }
    bool CanSeePlayer = false;
    private void Update()
    {
        CanSeePlayer = false;
        CanSeePlayer = IsAttackRange();


    }
    //idle상태일 때 이 코루틴이 실행된다
    public IEnumerator State_Idle()
    {
        CurrentState = AI_ENEMY_STATE.IDLE;

        agent.Stop();

        while (CurrentState == AI_ENEMY_STATE.IDLE)
        {
            if (CanSeePlayer)
            {
                StartCoroutine(State_Chase());
                yield break;
            }
        }
        StartCoroutine(State_Patrol());
        yield return null;
    }
    public IEnumerator State_Patrol()
    {
        CurrentState = AI_ENEMY_STATE.PATROL;

        Transform RandomDest = Waypoints[Random.Range(0, Waypoints.Length)].transform;

        agent.SetDestination(RandomDest.position);

        while (CurrentState == AI_ENEMY_STATE.PATROL)
        {
            if (CanSeePlayer)
            {
                StartCoroutine(State_Chase());
                yield break;
            }

            if (Vector3.Distance(transform.position, RandomDest.position) <= DistEps)
            {
                StartCoroutine(State_Patrol());
                //StartCoroutine(State_Idle());
                yield break;
            }
            yield return null;
        }
    }
    float ChaseTimeOut = 5f;
    public IEnumerator State_Chase()
    {
        CurrentState = AI_ENEMY_STATE.CHASE;

        while (CurrentState == AI_ENEMY_STATE.CHASE)
        {
            yield return null;
            agent.SetDestination(playerTransform.position);

            //if (!CanSeePlayer)
            //{
            //    float ElapsedTime = 0f;
            //    while(true)
            //    {
            //        ElapsedTime += Time.deltaTime;

            //        agent.SetDestination(playerTransform.position);
            //        yield return null;
            //    }
            //}
            //while (true)
            //{
            //    agent.SetDestination(playerTransform.position);

            //}
            if (Vector3.Distance(transform.position, transform.position) <= DistEps)
            {
                StartCoroutine(State_Attack());
                yield break;
            }
        }
    }
    public IEnumerator State_Attack()
    {
        CurrentState = AI_ENEMY_STATE.ATTACK;
        //공격 주기
        float ElapsedTime = 0f;

        while (CurrentState == AI_ENEMY_STATE.ATTACK)
        {
            transform.LookAt(playerTransform);
            ElapsedTime += Time.deltaTime * 0.05f;

            if(!CanSeePlayer) 
            {
                StartCoroutine(State_Chase());
                yield break;
            }
            if (ElapsedTime >= AttackDelay)
            {
                ElapsedTime = 0f;

                //공격시작
                if (playerTransform != null)
                {
                    Attack();
                }
            }
            yield return null;
        }
    }
    public float AttackDelay;
    public GameObject BulletObj;
    public float bulletSpeed;
    public Transform firePos;
    public int AttackDamage;

    public override void Attack()
    {
        switch(CurrentAttack)
        {
            case AI_ENEMY_ATTACK.MonBullet_01:
                MonBullet_01();
                break;
            case AI_ENEMY_ATTACK.MonBullet_02:
                MonBullet_02();
                break;
            case AI_ENEMY_ATTACK.MonBullet_03:
                MonBullet_03();
                break;
            case AI_ENEMY_ATTACK.MonBullet_04:
                MonBullet_04();
                break;
        }
    }


    public void MonBullet_01()
    {
        GameObject bullet = Instantiate(BulletObj);
        bullet.SetActive(true);
        bullet.transform.position = firePos.position;
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }
    public Transform firePosL;
    public Transform firePosLL;
    public Transform firePosR;
    public Transform firePosRR;
    public void MonBullet_02()
    {
        GameObject[] bullet = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            bullet[i] = Instantiate(BulletObj);
            bullet[i].SetActive(true);
        }
        bullet[0].transform.position = firePosL.position;
        bullet[1].transform.position = firePosLL.position;
        bullet[2].transform.position = firePos.position;
        bullet[3].transform.position = firePosR.position;
        bullet[4].transform.position = firePosRR.position;

        bullet[0].GetComponent<Rigidbody>().AddForce(firePosL.forward * bulletSpeed, ForceMode.Impulse);
        bullet[1].GetComponent<Rigidbody>().AddForce(firePosLL.forward * bulletSpeed, ForceMode.Impulse);
        bullet[2].GetComponent<Rigidbody>().AddForce(firePos.forward * bulletSpeed, ForceMode.Impulse);
        bullet[3].GetComponent<Rigidbody>().AddForce(firePosR.forward * bulletSpeed, ForceMode.Impulse);
        bullet[4].GetComponent<Rigidbody>().AddForce(firePosRR.forward * bulletSpeed, ForceMode.Impulse);
    }
    public void MonBullet_03()
    {

        GameObject bullet = Instantiate(BulletObj);
        bullet.transform.position = firePos.position;
        bullet.GetComponent<tempBullet>().SetTargetAndFirepos(playerTransform.position, firePos);
    }
    public void MonBullet_04()
    {
        GameObject[] bullet = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            bullet[i] = Instantiate(BulletObj);
        }
        bullet[0].transform.position = firePos.position;
        bullet[1].transform.position = firePosL.position;
        bullet[2].transform.position = firePosR.position;

        float tempDistance = Vector3.Distance(bullet[0].transform.position, playerTransform.position);

        //Transform tempTransform = firePosL;
        //tempTransform.position = firePosL.forward * tempDistance;
        //Transform tempTransform2 = firePosR;
        //tempTransform2.position = firePosR.forward * tempDistance;
        Debug.Log(tempDistance);
        Debug.Log(firePosR.forward);
        Debug.Log(firePosR.forward * tempDistance);
        bullet[0].GetComponent<tempBullet>().SetTargetAndFirepos(playerTransform.position, firePos);
        bullet[1].GetComponent<tempBullet>().SetTargetAndFirepos(firePosL.position + firePosL.forward * tempDistance, firePosL);
        bullet[2].GetComponent<tempBullet>().SetTargetAndFirepos(firePosR.position + firePosR.forward * tempDistance, firePosR);

        //bullet.transform.position = firePos.position;
        //bullet.GetComponent<tempBullet>().SetTargetAndFirepos(playerTransform, firePos);
    }
    public void MonAttack_01()
    {

    }
    float DistEps = 15f;

    bool IsAttackRange()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) <= DistEps)
        {
            return true;
        }
        return false;
    }


    public void MonBullet_01_Init()
    {

    }
    public void MonBullet_02_Init()
    {
        for (int i = 0; i < 5; i++)
        {

        }
    }
    public void MonBullet_03_Init()
    {

    }
    public void MonBullet_04_Init()
    {

    }
    public void GetDamaged(float damaged, int Type)
    {
        //if(attacktype)
        if(true)
        {
            Die();
        }
    }
    public void Die()
    {
        MonAttack_01();
        Destroy(gameObject);
    }
}
