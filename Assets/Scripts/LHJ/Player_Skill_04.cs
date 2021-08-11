using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Player_Skill_04 : MonoBehaviour
{
    private LineRenderer LineRender;

    public float Damage = 10.0f;
    public float Distance = 15.0f;
    public float Delay = 0.25f;
    public float LiveTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        LineRender = this.GetComponent<LineRenderer>();
        InvokeRepeating("Attack", 0.0f, Delay);
        Destroy(this.gameObject, LiveTime);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, Distance))
        {
            Debug.LogFormat("{0}", hit.transform.name);
            LineRender.SetPosition(0, this.transform.position);
            LineRender.SetPosition(1, hit.transform.position);
            Debug.DrawRay(transform.position, (transform.TransformDirection(Vector3.forward) * hit.distance), Color.yellow, 1000.0f);
        }
        else
        {
            LineRender.SetPosition(0, this.transform.position);
            LineRender.SetPosition(1, transform.position + (transform.TransformDirection(Vector3.forward) * Distance));
            Debug.DrawRay(transform.position, (transform.TransformDirection(Vector3.forward) * Distance), Color.white, 1000.0f);
        }

    }
}
