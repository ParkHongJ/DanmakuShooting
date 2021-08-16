using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet_03 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(.5f);
    }
}
