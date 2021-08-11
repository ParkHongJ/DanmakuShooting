using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_01 : MonoBehaviour
{
    public float Delay = 0.3f;
    public int Count = 5;
    public float Distance = 2.0f;
    public float Damage = 11.0f;
    public GameObject Spike;

    public int spawned = 0;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        for (; spawned < Count; spawned++)
        {
            yield return new WaitForSeconds(Delay);
            GameObject _spike = Instantiate(Spike, (this.transform.position + (this.transform.forward * Distance * spawned)), transform.rotation, this.transform);
            _spike.SetActive(true);
            _spike.gameObject.GetComponent<Spike>().Damage = this.Damage;
        }
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
