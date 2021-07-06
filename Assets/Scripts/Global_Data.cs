using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//글러벌 데이터 <싱글톤>

public class Global_Data : MonoBehaviour
{

    private static Global_Data instance;
   
    public static Global_Data Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<Global_Data>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newSingleton = new GameObject("GlobalData Class").AddComponent<Global_Data>();
                    instance = newSingleton;
                }

            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<Global_Data>();

        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
