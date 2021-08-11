using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float Damage = 10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

    public void Attack() // 애니메이션 트리거
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(this.transform.position.x, this.transform.position.y - 0.3f, this.transform.position.z), 0.8f);
        foreach (Collider hit in colliders)
        {
            if (hit.tag == "Player")
                Debug.LogFormat("{0} Dmaged {1}", hit.name, Damage);
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
