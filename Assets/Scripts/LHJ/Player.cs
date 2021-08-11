using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController controller; // 캐릭터 이동 관련
    private Animator animator; // 애니메이터

    private float moveSpeed = 6.0f; // 이동 속도
    private float rotateSpeed = 10.0f; // 회전 속도
    private float flashPower = 5.0f; // 점멸 거리 
    private float flashDelay = 3.5f; // 점멸 딜레이
    private float attack2Range = 8.0f;

    public Transform FirePos;
    public bool isAlive = true; // 살아있는가
    public bool isMovable = true; // 움직일 수 있는가
    public bool isAttackable = true; // 공격
    public bool canFlash = true; // 점멸 가능한가

    private float viewX, viewY, viewZ; // 보는 방향
    private float moveX, moveZ; // 이동 값

    public GameObject[] bullet; // 0 1 2 3 4 5

    [SerializeField]
    [Tooltip("체력")]
    private float hp = 100; // 체력
    private int potion = 0; // 포션 개수
    private int attack1_type = 0; // 공격 타입
    private int attack2_type = 0; // 공격 타입

    void Start() // 초기 설정
    {
        if (FirePos == null)
            FirePos = transform.Find("FirePos").transform;
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        isAlive = true; // 살아있는가?
        canFlash = true; // 점멸중인가?
        isMovable = true; // 움직일 수 있는가
        isAttackable = true; // 공격
    }

    void FixedUpdate() // 물리 관련
    {
        if (isAlive)
        {
            Move(); // 움직임

            /// 회피
            if (Input.GetButton("Jump") && canFlash)
            {
                this.Dodge(moveX, moveZ);
            }
        }
    }

    void Update() // 체크
    {
        if (isAlive)
        {
            if (hp <= 0) // 체력이 없으면
            {
                Death(); // 사망
            }
            if (Input.GetKeyDown(KeyCode.E)) // 테스트용
            {
                this.Death(); // 사망 시 호출
            }

            if (Input.GetButton("Fire1") && isAttackable) // 공격1
            {
                Attack(4,0);
            }

            if (Input.GetButton("Fire2") && isAttackable) // 공격2
            {
                Attack(5,1);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) // 충돌 시
    {
        //Debug.LogFormat("충돌 : {0},{1}\n", hit.gameObject.name, hit.GetType());
        if (hit.gameObject.tag == "bullet?")
            Damaged(hit.transform.position.x);
    }

    void Move() // 플레이어 이동
    {
        moveX = Input.GetAxis("Horizontal"); // X 이동 값 
        moveZ = Input.GetAxis("Vertical"); // Z 이동 값 
        Vector2 inputDir = new Vector2(moveX, moveZ); // 입력 방향
        Vector3 inputDir3 = new Vector3(moveX, 0, moveZ); // 입력 방향

        if (isMovable) // 움직일 수 있는지 체크
            controller.Move(new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed);

        if (isMovable)
            ViewMouse();
        // 회전
        //if ((Mathf.Abs(moveX) >= 0.13f || Mathf.Abs(moveZ) >= 0.13f) && isMovable) // 일정이상 입력받으면
        //{
        //    viewX = Mathf.Lerp(viewX, moveX, 0.8f);
        //    viewZ = Mathf.Lerp(viewZ, moveZ, 0.8f);
        //}
        //Vector3 dir = new Vector3(viewX, viewY, viewZ); // 동일 높이에서 어디로 바라보고 있는지 
        //float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg; // x,y 값으로 각도 구하기
        //if (isMovable)
        //    controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, angle, 0.0f), rotateSpeed * Time.deltaTime); // 부드러운 회전

        this.UpdateAnimation(moveX, moveZ);
    }
    void UpdateAnimation(float h, float v) // 애니메이터 업데이트
    {
        if (animator == null)
            return;

        // 2차원 블랜더트리
        if (isMovable)
        {
            animator.SetFloat("move_FB", v);
            animator.SetFloat("move_LR", h);
        }

        if (isMovable)
        {
            animator.SetFloat("moveSpeed", new Vector2(h, v).magnitude); // 입력을 얼마나 넣었는가
        }

        if (controller.velocity != Vector3.zero) // 이동중인가?
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }


        controller.SimpleMove(Vector3.forward * 0); // 중력
    }

    void Attack(int _index, int _click) // 공격 타입 판단
    {
        switch (_index)
        {
            // Attack1 - FirePos에서 발사
            case 0: // 바위 발사
                Attack1(_index, 0.25f);
                break;
            case 3: // 화염구
                Attack1(_index, 0.25f);
                break;


            // Attack2 - FirePos위치 바닥부터 발사
            case 1: // 가시 공격
                Attack2(_index, 0.5f);
                break;

            // Attack3 - 마우스 지점에서 생성
            case 2: // 모래 늪
                Attack3(_index, 0.75f);
                break;
            case 5: // 폭발
                Attack3(_index, 0.75f);
                break;

            // Attack4 - 토글식
            case 4: // 빔
                StartCoroutine(Attack4(_index, 0.5f, 2.0f));
                break;
        }
        if (_index != 4)
            if (_click == 0)
                animator.SetTrigger("attack1");
            else if (_click == 1)
                animator.SetTrigger("attack2");
    }

    void Attack1(int _index, float _cooltime) // 발사
    {
        isAttackable = false; StartCoroutine(AttackDelay(_cooltime));
        ViewMouse();


        Instantiate(bullet[_index], FirePos.transform.position, FirePos.transform.rotation);
    }
    void Attack2(int _index, float _cooltime) // 발사
    {
        isAttackable = false; StartCoroutine(AttackDelay(0.5f));
        ViewMouse();


        Instantiate(bullet[_index], new Vector3(FirePos.transform.position.x, this.transform.position.y, FirePos.transform.position.z) + (this.transform.forward * 2.0f), FirePos.transform.rotation);
    }

    void Attack3(int _index, float _cooltime) // 마우스 지점공격
    {
        isAttackable = false; StartCoroutine(AttackDelay(0.5f));
        ViewMouse();

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스위치로 레이 구하기
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero); // 하늘보는 평면
        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            if (pointTolook.x >= this.transform.position.x + attack2Range)
                pointTolook.x = this.transform.position.x + attack2Range;
            if (pointTolook.z >= this.transform.position.z + attack2Range)
                pointTolook.z = this.transform.position.z + attack2Range;
            if (pointTolook.x <= this.transform.position.x - attack2Range)
                pointTolook.x = this.transform.position.x - attack2Range;
            if (pointTolook.z <= this.transform.position.z - attack2Range)
                pointTolook.z = this.transform.position.z - attack2Range;

            //Debug.LogFormat("{0},{1},{2}", pointTolook.x, pointTolook.y, pointTolook.z);
            Instantiate(bullet[_index], pointTolook, FirePos.transform.rotation);
        }

    }
    IEnumerator Attack4(int _index, float _cooltime, float _movetime) // 발사
    {
        float time = 0.0f;
        isAttackable = false; 
        ViewMouse();
        animator.SetBool("attack1_toggle", true);

        yield return new WaitForSeconds(0.25f);

        this.isMovable = false;
        GameObject Laser = Instantiate(bullet[_index], FirePos.transform.position, FirePos.transform.rotation);

        StartCoroutine(Attack1AniDelay(_movetime));
        StartCoroutine(MoveDelay(_movetime));
        StartCoroutine(AttackDelay(_cooltime + _movetime));
    }


    void Dodge(float h, float v)
    {
        canFlash = false;
        animator.SetTrigger("flash");
        isMovable = false;
        controller.Move(controller.transform.forward * flashPower);
        isMovable = true;
        StartCoroutine(DodgeDelay(flashDelay));
    }

    IEnumerator DodgeDelay(float _sec)
    {
        yield return new WaitForSeconds(_sec);
        canFlash = true;
    }
    IEnumerator AttackDelay(float _sec)
    {
        yield return new WaitForSeconds(_sec);
        isAttackable = true;
    }
    IEnumerator Attack1AniDelay(float _sec)
    {
        yield return new WaitForSeconds(_sec);
        animator.SetBool("attack1_toggle", false);
    }
    IEnumerator Attack2AniDelay(float _sec)
    {
        yield return new WaitForSeconds(_sec);
        animator.SetBool("attack2_toggle", false);
    }
    IEnumerator MoveDelay(float _sec)
    {
        yield return new WaitForSeconds(_sec);
        isMovable = true;
    }

    void Damaged(float dmg)
    {
        animator.SetTrigger("damaged");
        this.hp -= dmg;
        if (hp < 0)
        {
            Death();
        }
    }
    void ViewMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스위치로 레이 구하기
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero); // 하늘보는 평면
        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
            viewX = pointTolook.x; viewZ = pointTolook.z;
        }
    }

    void Death() // 사망
    {
        isAlive = false;
        isMovable = false;
        animator.SetBool("isAlive", false);
    }
}
