using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    
    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;

    void Start()
    {
        


        if (muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
                Destroy(muzzleVFX, psMuzzle.main.duration);
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }

        }

        
    }


    void Update()
    {

        if (speed != 0)

        {
            StartCoroutine(Pleasework());
        }
        
        else
        {
            Debug.Log("No Speed");
        }


    }


    IEnumerator Pleasework()
    {
        yield return new WaitForSeconds(3.05f);


        transform.position -= transform.right * (speed * Time.deltaTime);
    }
        


        
    



    void OnCollisionEnter(Collision co)
    {
        speed = 0;

        ContactPoint contact = co.contacts[0];
        

        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab);
            var psHit = hitVFX.GetComponent<ParticleSystem>();
            if (psHit != null)
                Destroy(hitVFX, psHit.main.duration);

            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
        }

        Destroy(gameObject);

    }

}
