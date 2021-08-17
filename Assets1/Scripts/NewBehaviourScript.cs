using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : ActtackPattern
{

    public Transform firePos;
    public GameObject BulletObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Attack()
    {
        base.Attack();
        GameObject bullet = Instantiate(BulletObj);
        bullet.SetActive(true);
        bullet.transform.position = firePos.position;


    }
    IEnumerator temp()
    {
        Attack();
        yield return new WaitForSeconds(.2f);
    }
}
