using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaaa : AttackPattern
{
    GameObject BulletObj;
    public Transform firePos;
    public float bulletSpeed = 30;
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
        GameObject[] bullet = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            bullet[i] = Instantiate(BulletObj);
        }

        StartCoroutine(attack(bullet));



        //GameObject bullet1 = Instantiate(BulletObj); 
        //bullet1.SetActive(true);
        //bullet1.transform.position = firePos.position;

        //GameObject bullet2 = Instantiate(BulletObj);
        //bullet2.SetActive(true);
        //bullet2.transform.position = firePos.position + firePos.forward * 10;

        //GameObject bullet3 = Instantiate(BulletObj);
        //bullet3.SetActive(true);
        //bullet3.transform.position = firePos.position;

        //GameObject bullet4 = Instantiate(BulletObj);
        //bullet4.SetActive(true);
        //bullet4.transform.position = firePos.position;

        //GameObject bullet5 = Instantiate(BulletObj);
        //bullet5.SetActive(true);
        //bullet5.transform.position = firePos.position;
    }
    IEnumerator attack(GameObject[] bullet)
    {
        bullet[0].transform.position = firePos.position;
        bullet[0].SetActive(true);
        yield return new WaitForSeconds(.1f);

        bullet[1].transform.position = firePos.position + firePos.forward * 10f;
        bullet[1].SetActive(true);
        yield return new WaitForSeconds(.1f);

        bullet[2].transform.position = firePos.position + firePos.forward * 20f;
        bullet[2].SetActive(true);
        yield return new WaitForSeconds(.1f);

        bullet[3].transform.position = firePos.position + firePos.forward * 30f;
        bullet[3].SetActive(true);
        yield return new WaitForSeconds(.1f);

        bullet[4].transform.position = firePos.position + firePos.forward * 40f;
        bullet[4].SetActive(true);
        yield return null;
    }
}
