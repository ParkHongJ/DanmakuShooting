using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour, IHit
{
    public enum AI_ENEMY_MONSTER_TYPE
    {
        Ground = 1, //땅인지
        Sky //하늘인지
    }
    public void GetDamaged(float damaged, int Type)
    {
        throw new System.NotImplementedException();
    }
    public float hp;

    public Transform[] BaseTrack;
    public Transform[] Track_01;
    public Transform[] Track_02;
    public Transform[] Track_03;
    bool CanSeePlayer;
    public AI_ENEMY_MONSTER_TYPE monsterType;
    Animator animator;
    public float speed;
    bool isMove = false;
    Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Patrol();
    }
    void Update()
    {
        if(hp > 0)
        { 
            if (isMove)
            {
                if (Vector3.Distance(transform.position, destination.position) >= 5f)
                {
                    isMove = false;
                    return;
                }

                transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
            }
        }
        else
        {
            Die();
        }
    }
    public void SetDestination()
    {
        //destination = 
    }
    public void Patrol()
    {
        animator.SetTrigger("Patrol");
        SetDestination();
        isMove = true;
    }
    
    public void BossBullet_01()
    {

    }
    public void BossBullet_02()
    {

    }
    public void BossBullet_03()
    {

    }
    public void Die()
    {

    }
}
