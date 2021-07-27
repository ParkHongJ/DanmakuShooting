using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiFire : ActtackPattern
{
    public GameObject BulletObj;
    public Transform firePos;
    public float bulletSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Pattern());
        }
    }
    public override void Attack()
    {
        GameObject bullet = Instantiate(BulletObj);
        bullet.SetActive(true);
        bullet.transform.position = firePos.position;
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }
    IEnumerator Pattern()
    {
        Attack();
        yield return new WaitForSeconds(.2f);
        Attack();
        yield return new WaitForSeconds(.2f);
        Attack();
        yield return new WaitForSeconds(.2f);
        Attack();
        yield return new WaitForSeconds(.2f);
    }
}
