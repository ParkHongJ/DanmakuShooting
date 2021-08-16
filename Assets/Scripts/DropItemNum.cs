using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemNum : MonoBehaviour
{
    private int num;

    public void SetNum(int p)
    {
        num = p;
    }
    public int GetNum()
    {
        return num;
    }

    public void DestroyedSelf()
    {
        Destroy(gameObject);
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
