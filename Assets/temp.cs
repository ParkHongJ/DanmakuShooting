using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = new Vector3();
        temp = transform.forward;
        temp.z += 10f;
        Debug.DrawRay(transform.position, temp, Color.blue);


        Vector3 tempx = new Vector3();
        tempx = transform.right;
        tempx.x += 10f;
        Debug.DrawRay(transform.position, tempx, Color.red);

        Vector3 tempx2 = new Vector3();
        tempx2 = -transform.right;
        tempx2.x -= 10f;
        Debug.DrawRay(transform.position, tempx2, Color.yellow);

        Vector3 temp3 = new Vector3();
        temp3 = temp + tempx;
        Debug.DrawRay(transform.position, temp3, Color.green);

        Vector3 temp4 = new Vector3();
        temp4 = temp3 + temp;
        Debug.DrawRay(transform.position, temp4, Color.cyan);
    }
}
