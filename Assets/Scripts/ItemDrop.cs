﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class UnityObjectExtensions
{
    // 아래쪽과 맞추려면 IsRealNull이나 뭐 그런 걸로
    public static bool IsNull(this Object obj)
    {
        return ReferenceEquals(obj, null);
    }

    public static bool IsFakeNull(this Object obj)
    {
        return !ReferenceEquals(obj, null) && obj;
    }

    public static bool IsAssigned(this Object obj)
    {
        return obj;
    }
}
public class ItemDrop : MonoBehaviour,IHit
{
    public int hp;
    public bool isdead;
    public GameObject dropobj;
    public GameObject wreck;
    public GameObject b_col;
    public GameObject canbreakobj;
    public GameObject parent;
    public GameObject fire;
    public Vector3 worldpos;
    private bool w_null, b_null, e_null;

    private void Start()
    {
        w_null = false;
        b_null = false;
        e_null = false;
        hp = 3;
        isdead = false;
        if (wreck == null)
            w_null = true;
        if (b_col == null)
            b_null = true;
        if (fire == null)
            e_null = true;
    }

    private bool Candrop()
    {
        int ran = Random.Range(1, 11);

        //return true;///////////////////////////////////////////////////////////////////////////////////없애야할거
        if (ran == 1)
            return true;
        else
            return false;
    }

    private int ChoseDropObjNum()//itemlist에 있는 아이템 번호로 리턴
    {
        int ran = Random.Range(1, 11);
        print("2 : " + ran);
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

    public GameObject item_;

    private void SetDropObj()
    {
        int num = ChoseDropObjNum();
        dropobj = Resources.Load<GameObject>("Prefab/" + Global_Data.Instance.ItemList[num - 1].itemname);
        item_ = Instantiate(dropobj);
        dropobj.GetComponent<DropItemNum>().SetNum(int.Parse(Global_Data.Instance.ItemList[num - 1].num));
        //dropobj.transform.SetParent(parent.transform);
        //item_.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        item_.GetComponent<DropItemNum>().Setpos(worldpos);
    }

    //public bool IsNull(this Object obj)
    //{
    //    return ReferenceEquals(obj, null);
    //}

    private void Itemdrop()
    {
        if (Candrop())
        {
            SetDropObj();
            canbreakobj.SetActive(false);
            if (!w_null)
                wreck.SetActive(true);
            if (!b_null)
                b_col.SetActive(true);
        }
        else
        {
            canbreakobj.SetActive(false);
            if (!w_null)
                wreck.SetActive(true);
            if (!b_null)
                b_col.SetActive(true);
        }
    }
    public int nunn = 100;
    private IEnumerator Sinking()
    {

        //yield return new WaitForSeconds(2f);
        for (int i = 0; i <= nunn; i++)
        {
            fire.transform.position = new Vector3(fire.transform.position.x, fire.transform.position.y - 0.1f, fire.transform.position.z);
            yield return new WaitForSeconds(0.02f);
        }
    }
    void Breaking()
    {
        canbreakobj.SetActive(false);
        if (!w_null)
        {
            wreck.SetActive(true);
        }
        if (!b_null)
        {
            b_col.SetActive(true);
        }
        StartCoroutine(Sinking());
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
            if (tag == "Pillar")
                Itemdrop();
            if (tag == "Stove")
                Breaking();
            isdead = false;
            gameObject.GetComponent<ItemDrop>().enabled = false;
        }

    }

    public void GetDamaged(float damaged, int Type)
    {
        hp -= (int)damaged;
    }

    public void GetDamaged(float damaged)
    {
        hp -= (int)damaged;
    }
}

