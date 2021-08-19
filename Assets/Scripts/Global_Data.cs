using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//글러벌 데이터 <싱글톤>

public class Global_Data : MonoBehaviour
{

    private static Global_Data instance;

    public List<Item> ItemList = new List<Item>();
    public List<Dialog> DialogList = new List<Dialog>();
    public bool IsIngame = true;    //ingame 여부 확인
    public bool IsGameOver = false;

    public int st1_mon = 8, st2_mon = 50;

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
