using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSCRIPT : MonoBehaviour
{
    public GameObject num1, num2, num3, num4, num5;


    // Start is called before the first frame update
    void Start()
    {
        print(num1.transform.position);
        print(num1.transform.localPosition);
        print(num2.transform.position);
        print(num2.transform.localPosition);
        print(num3.transform.position);
        print(num3.transform.localPosition);
        print(num4.transform.position);
        print(num4.transform.localPosition);
        print(num5.transform.position);
        print(num5.transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
