using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController controller; // 캐릭터 이동 관련
    private Animator animator; // 애니메이터
    public int hp = 100; // 체력
    private float moveSpeed = 6.0f; // 속도
    private float flashPower = 5;
    private float rotateSpeed = 10.0f; // 회전 속도
    private float flashDelay = 0.5f;

    public bool isAlive = true; // 살아있는가?
    public bool isMovable = true; // 움직일 수 있는가?
    public bool canFlash = true; // 점멸중인가?
    float flashTimer = 0.0f;
    private float viewX, viewY, viewZ; // 보는 방향
    private float moveX, moveZ; // 이동 값

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        viewX = 0.0f;
        viewY = 0.0f;
        viewZ = 0.0f;
        isMovable = true; 
        canFlash = true; // 점멸중인가?
    }

    void FixedUpdate()
    {
        moveX = (Input.GetAxis("Horizontal")); // X 이동 값 
        moveZ = (Input.GetAxis("Vertical")); // Z 이동 값 
        Vector2 inputDir = new Vector2(moveX, moveZ); // 입력 방향
        Vector3 inputDir3 = new Vector3(moveX, 0, moveZ); // 입력 방향

        /// 회피
        if (Input.GetButton("Jump") && canFlash && isAlive)
        {
            canFlash = false;
            animator.SetBool("isFlash", true);
            isMovable = false;
            StartCoroutine(DodgeMove(0.2f));
            this.Dodge(moveX, moveZ);
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            this.Dead();
        }

            /// 이동
            if (isMovable)
        {
            controller.Move(new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed);
        }

        //transform.position += new Vector3((moveX / 100) * m_speed, 0, (moveZ / 100) * m_speed);
        //m_rigidbody.AddForce(moveX * 10 * 10, 0, moveZ * 10);
        //if (isMovable)
        //{
        //    rigidbody.velocity = new Vector3(moveX * (moveSpeed), rigidbody.velocity.y, moveZ * (moveSpeed));
        //}


        /// 회전
        if ((Mathf.Abs(moveX) >= 0.13f || Mathf.Abs(moveZ) >= 0.13f) && isMovable)
        {
            viewX = Mathf.Lerp(viewX, moveX, 0.8f);
            viewZ = Mathf.Lerp(viewZ, moveZ, 0.8f);
        }
        Vector3 dir = new Vector3(viewX, viewY, viewZ); // 동일 높이에서 어디로 바라보고 있는지 
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg; // x,y 값으로 각도 구하기
        if(isMovable)
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, angle, 0.0f), rotateSpeed * Time.deltaTime); // 부드러운 회전

        //Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스위치로 레이 구하기
        //Plane GroupPlane = new Plane(Vector3.up, Vector3.zero); // 하늘보는 평면
        //float rayLength;

        //if (GroupPlane.Raycast(cameraRay, out rayLength))
        //{
        //    Vector3 pointTolook = cameraRay.GetPoint(rayLength);

        //    transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        //}

        /// 애니메이션
        if (isMovable)
            this.UpdateAnimation(moveX, moveZ);
    }

    void Update()
    {

    }

    void Dead()
    {
        animator.SetBool("isAlive", false);
        isMovable = false;
        isAlive = false;
    }


    void UpdateAnimation(float h, float v)
    {
        if (animator == null)
            return;

        animator.SetFloat("moveSpeed", new Vector2(h, v).magnitude); // 입력을 얼마나 넣었는가

        if (controller.velocity == Vector3.zero) // 이동중인가?
            animator.SetBool("isMove", false);
        else
            animator.SetBool("isMove", true);


        controller.SimpleMove(Vector3.forward * 0);
    }

    void Dodge(float h, float v)
    {
        controller.Move(controller.transform.forward * flashPower);
        isMovable = true;
        StartCoroutine(DodgeDelay(flashDelay));
    }

    IEnumerator DodgeMove(float _sec)
    {
        yield return new WaitForSeconds(_sec);
        animator.SetBool("isFlash", false);
    }

    IEnumerator DodgeDelay(float _sec)
    {
        yield return new WaitForSeconds(_sec);
        canFlash = true;
    }

}
