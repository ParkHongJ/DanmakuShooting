using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public int hp;
    public bool isdead;
    public GameObject dropobj;
    private void Start()
    {
        hp = 10;
        isdead = false;
    }

    private bool Candrop()
    {
        int ran = Random.Range(1, 11);
        return true;
       // print(ran);
        if (ran == 1)
            return true;
        else
            return false;
    }
    private int ChoseDropObjNum()//itemlist에 있는 아이템 번호로 리턴
    {
        int ran = Random.Range(1, 11);
        //print("2 : " + ran);
        switch (ran)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                return 1;
            case 5:
                return 2;
            case 6:
                return 3;
            case 7:
                return 4;
            case 8:
                return 5;
            case 9:
                return 6;
            case 10:
                return 7;
        }
        return -1;
    }
    private void SetDropObj()
    {
        int num = ChoseDropObjNum(); 
        dropobj = Resources.Load<GameObject>("Prefab/"+Global_Data.Instance.ItemList[num-1].objname);
        Instantiate(dropobj);
      
    }
    private void Itemdrop()
    {
        if (Candrop())
        {
            SetDropObj();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
  
        if (hp <= 0)
        {
            isdead = true;
        }
        if (isdead)
        {
            Itemdrop();
        }

    }
}

