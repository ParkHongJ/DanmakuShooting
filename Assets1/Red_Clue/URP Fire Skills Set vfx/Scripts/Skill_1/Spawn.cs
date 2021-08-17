using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour

{
    public GameObject firepoint;
    public List<GameObject> fx = new List<GameObject>();
    public RotateToMouse rotateToMouse;


    private GameObject effectToSpawn;
    private float timeToFire = 0;

    void Start()
    {
        effectToSpawn = fx[0];
    }


    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / effectToSpawn.GetComponent<movement>().fireRate;
            SpawnVFX();
        }
    }

    void SpawnVFX()
    {
        GameObject fx;

        if (firepoint != null)
        {
            fx = Instantiate(effectToSpawn, firepoint.transform.position, Quaternion.identity);
            if (rotateToMouse != null)
            {
                fx.transform.localRotation = rotateToMouse.GetRotation();
            }
        }

        else
        {
            Debug.Log("No Fire Point");
        }
    }
}
