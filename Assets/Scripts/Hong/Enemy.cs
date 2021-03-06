using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject BulletObj;
    public Transform firePos;
    public GameObject SandSwamp;
    public float bulletSpeed = 10;
    public GameObject player;
    public GameObject[] bullets;
    [SerializeField]
    int hp;
    //private void Awake()
    //{
    //    for (int i = 0; i < 200; i++)
    //    {
    //        bullets[i] = Instantiate(BulletObj);
    //    }
    //}
    private LineRenderer lineRenderer;

    // Use this for initialization
    void Start()
    {
        hp = 100;
    }
    public void GetDamaged(int _damage)
    {
        hp -= _damage;
        if( hp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {

    }
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        //    {
        //        Vector3 temp = new Vector3(0, 0f, 0);
        //        temp.x = hit.point.x;
        //        temp.z = hit.point.z;
        //        RaycastHit _hit;
        //        if (Physics.Raycast(firePos.position, firePos.forward, out _hit, 50f))
        //        {
        //            if (_hit.transform.CompareTag("player"))
        //            {
        //                Vector3 temp1 = new Vector3(0, 0, 0);
        //                temp1.x = _hit.transform.position.x;
        //                temp1.z = _hit.transform.position.z;
        //                lineRenderer.SetPosition(1, firePos.position);
        //                lineRenderer.SetPosition(2, temp1);
        //                Debug.Log("맞음");
        //            }
        //        }
        //        Debug.Log(hit.point);
        //    }
        //}






        //Vector3 vec = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        //transform.LookAt(vec);
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        //    {
        //        GameObject bullet = Instantiate(SandSwamp);
        //        Vector3 temp = new Vector3(0, 0, 0);
        //        temp.x = hit.point.x;
        //        temp.z = hit.point.z;
        //        bullet.transform.position = temp;
        //        bullet.SetActive(true);

        //        Debug.Log(hit.point);
        //    }
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    StartCoroutine(Pattern());
        //}
    }
    void Fire()
    {
        GameObject bullet = Instantiate(BulletObj);
        bullet.SetActive(true);
        bullet.transform.position = firePos.position;
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }
    IEnumerator Pattern()
    {
        Fire();
        yield return new WaitForSeconds(.2f);
        //Fire();
        //yield return new WaitForSeconds(.2f);
        //Fire();
        //yield return new WaitForSeconds(.2f);
        //Fire();
        //yield return new WaitForSeconds(.2f);
    }
}
