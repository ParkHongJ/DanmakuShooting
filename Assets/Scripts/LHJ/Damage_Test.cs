using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Test : MonoBehaviour, IHit
{
    void IHit.GetDamaged(float damaged, int Type)
    {
        Debug.LogFormat("{0},{1},{2}", this.transform.name, damaged, Type);
    }

    void IHit.GetDamaged(float damaged)
    {
        Debug.LogFormat("{0},{1}", this.transform.name, damaged);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
