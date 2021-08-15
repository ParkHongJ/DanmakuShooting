using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vibration : MonoBehaviour
{
    private Vector3 initialPosition;
    public float amplitude; // the amount it moves
    public float frequency; // the period of the earthquake

    void Start()
    {
        initialPosition = transform.position; // store this to avoid floating point error drift
    }



    void FixedUpdate()
    {
        Vector3 directionOfShake = transform.forward;
        transform.position = initialPosition + directionOfShake * Mathf.Sin(frequency * Time.fixedDeltaTime) * amplitude;
    }
}
