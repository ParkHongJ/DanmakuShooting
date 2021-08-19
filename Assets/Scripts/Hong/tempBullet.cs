using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempBullet : MonoBehaviour
{

    [SerializeField]
    IHit ihit;

    public Transform firePos;
    public Vector3 Target;
    // Start is called before the first frame update
    void Start()
    {
        IsArrival = false;
    }

    float RotateSpeed = 3f;
    public bool IsArrival = false;

    public float Damage = 10f;
    float timer = 0.0f;
    float waitingTime = 1.5f;
    // Update is called once per frame
    void Update()
    {
        if(transform.position == Target)
        {
            IsArrival = true;
        }

        if (IsArrival) //도착했으면
        {
            timer += Time.deltaTime; // 1.5초동안 대기
            if (timer > waitingTime ) // 시작지점으로 돌아가기
            {
                if (Vector3.Distance(transform.position,firePos.position) <= 2f) //   transform.position == firePos.position)
                    Destroy(gameObject);
                transform.position = Vector3.MoveTowards(transform.position, firePos.position, 15f * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, 10f * Time.deltaTime);
        }

        transform.Rotate(Vector3.up * RotateSpeed);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Bullet"))
    //    {
    //        return;
    //    }
    //    IHit ihit;

    //    ihit = other.GetComponent<IHit>();
    //    if( ihit != null)
    //    {
    //        ihit.GetDamaged(Damage);
    //    }

    //}
    float delay = 0.5f;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            return;
        }
        IHit ihit = other.GetComponent<IHit>();
        if ( ihit != null )//충돌판정이 가능한 오브젝트면
        {
            float ElapsedTime = 0f;
            ElapsedTime += Time.deltaTime;
            if(ElapsedTime >= delay)
            {
                ihit.GetDamaged(Damage);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        ihit = null;
    }
    public void SetTargetAndFirepos(Vector3 _Target, Transform _Firepos)
    {
        Target = _Target;
        firePos = _Firepos;
    }
}
