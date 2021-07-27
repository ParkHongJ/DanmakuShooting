using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
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

        StartCoroutine(State_Attack());
    }
    //idle상태일 때 이 코루틴이 실행된다
    public IEnumerator State_Idle()
    {
        CurrentState = AI_ENEMY_STATE.IDLE;

        animator.SetTrigger((int)AI_ENEMY_STATE.IDLE);
        yield return null;
    }

    public float AttackDelay;
    public GameObject BulletObj;
    public float bulletSpeed;
    public Transform firePos;
    public int AttackDamage;
    public IEnumerator State_Attack()
    {
        CurrentState = AI_ENEMY_STATE.ATTACK;
        //공격 주기
        float ElapsedTime = 0f;

        while ( CurrentState == AI_ENEMY_STATE.ATTACK )
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
    public void Attack()
    {
        GameObject bullet = Instantiate(BulletObj);
        bullet.SetActive(true);
        bullet.transform.position = firePos.position;
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}
