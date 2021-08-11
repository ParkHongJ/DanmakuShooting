using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill_05 : MonoBehaviour
{
    public float Height = 1.5f;
    public float Radius = 3f;
    public float Damage = 10.0f;
    public float LiveTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Attack();
        Death();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Attack()
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(this.transform.position.x, this.transform.position.y + Height, this.transform.position.z), Radius);
        foreach (Collider hit in colliders)
        {
            if (hit.tag == "Player")
            {
                Debug.LogFormat("{0} Dmaged {1}", hit.name, Damage);
                Debug.Log("dam : " + Time.time);
            }
        }
    }

    void Death()
    {
        //Destroy(this.gameObject, LiveTime);
    }
}
