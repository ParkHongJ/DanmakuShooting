using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : AttackPattern
{
    public enum AI_ENEMY_STATE
    {
        IDLE = 1,
        PATROL,
        CHASE,
        ATTACK
    };

    //현재 상태
    public AI_ENEMY_STATE CurrentState = AI_ENEMY_STATE.IDLE;

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
        CanSeePlayer = Tempfunc();


        for (int i = 0; i < vertices.Count - 1; ++i)
        {
            Debug.DrawLine(vertices[i], vertices[i + 1]);
        }
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
                StartCoroutine(State_Idle());
                yield break;
            }
            yield return null;
        }
    }
    public IEnumerator State_Chase()
    {
        CurrentState = AI_ENEMY_STATE.CHASE;

        while (CurrentState == AI_ENEMY_STATE.CHASE)
        {
            agent.SetDestination(playerTransform.position);

            //while (true)
            //{
            //    agent.SetDestination(playerTransform.position);

            //}
            if (Vector3.Distance(transform.position, transform.position) <= DistEps)
            {
                StartCoroutine(State_Attack());
                yield break;
            }
            yield return null;
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
        GameObject bullet = Instantiate(BulletObj);
        bullet.SetActive(true);
        bullet.transform.position = firePos.position;
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    float DistEps = 15f;

    bool Tempfunc()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) <= DistEps)
        {
            return true;
        }
        return false;
    }
    private Vector3 pivot;

    public List<Vector3> vertices;
    public float radius = 5.0f;
    public float fScale = .5f;
    void drawCircle()
    {

        vertices = new List<Vector3>();
        float heading;
        for (int a = 0; a < 360; a += 360 / 30)
        {
            heading = a * Mathf.Deg2Rad;
            vertices.Add(new Vector3(Mathf.Cos(heading) * radius, Mathf.Sin(heading) * this.radius, transform.position.z));

        }
        for (int i = 0; i < vertices.Count - 1; ++i)
        {
            Debug.DrawLine(vertices[i], vertices[i + 1]);
        }

    }

}
