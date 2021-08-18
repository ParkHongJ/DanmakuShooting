using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Interaction : MonoBehaviour
{
    Collider colider_n;
    bool colok = false;
    public GameObject playerconversation;
    public GameObject playingui;
    private Vector3 ori_playconver;
    private Vector3 ori_playui;
    private float time;
    private bool aniok = false;
    bool downok = false;
    bool upok = false;
    bool dialog_end = false;
    //NPC npc;
    string n_name;
    int npc_num;
    int know_num;
    public Text p_dialog;
    public Image p_image;
    public string m_text;
    private bool con_first;
    public Camera uicamera;
    public GameObject tmp;
    private bool itemok = false;
    public GameObject iteminforUI;
    public GameObject enter_item;
    public Text item_infor_text;
    // Start is called before the first frame update
    private Coroutine C_Uiup, C_Uidown,typing;
    public Player Player_s;

    private IEnumerator _typing()
    {
        //yield return new WaitForSeconds(2f);
        for (int i = 0; i <= m_text.Length; i++)
        {
            p_dialog.text = m_text.Substring(0, i);
            yield return new WaitForSeconds(0.02f);
        }
    }
    

    void Start()
    {
        ori_playconver = playerconversation.transform.position;
        ori_playui = playingui.transform.position;
        time = 0.0f;
        con_first = false;
        know_num = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "quest")
        {
            print("enter");
            colider_n = other;
            colok = true;
            n_name = colider_n.GetComponent<NPC>().name;
            npc_num = colider_n.GetComponent<NPC>().db_num;
        }
        if (other.tag == "item")
        {
            enter_item = other.gameObject;
            print("entser item");
            int itemnum = other.GetComponent<DropItemNum>().GetNum();
            iteminforUI.SetActive(true);
            //string p = Global_Data.Instance.ItemList[itemnum].explan;
            string[] spstring = Global_Data.Instance.ItemList[itemnum-1].explan.Split('&');
            item_infor_text.text = spstring[0] + "\n" + spstring[1];
            itemok = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="item")
        {
            iteminforUI.SetActive(false);
        }

        colider_n = null;
        colok = false;
        //npc = null;
        con_first = false;
        enter_item = null;
        itemok = false;
    }
    private IEnumerator UIDown(GameObject gm)
    {
        Vector3 tmp = gm.transform.localPosition;
        tmp.y -= time;
        gm.transform.localPosition = tmp;
        if (tmp.y >= -520f)
        {
            time += Time.deltaTime * 5f;
            yield return new WaitForSeconds(0.1f);
        }
        if (tmp.y < -520f)
        {
            tmp.y = -520f;
            gm.transform.localPosition = tmp;
            downok = true;
            yield break;// new WaitForSeconds(0.1f);
        }

    }
    private IEnumerator UIUp(GameObject gm)
    {
        Vector3 tmp = gm.transform.localPosition;
        tmp.y += time;
        gm.transform.localPosition = tmp;
        if (tmp.y <= 0f)
        {
            time += Time.deltaTime * 5f;
            yield return new WaitForSeconds(0.1f);
        }
        if (tmp.y > 0f)
        {
            tmp.y = 0f;
            gm.transform.localPosition = tmp;
            upok = true;
            yield break;// new WaitForSeconds(0.1f);
        }

    }


    void StopMethod(Coroutine co)
    {
        if (co != null)
        {
            StopCoroutine(co);
        }
    }

    bool GetNextText()
    {
        if(know_num==0)
        {
            know_num = npc_num;
            m_text = Global_Data.Instance.DialogList[know_num - 1].text;
            return true;
        }
        else
        {
            if (n_name != Global_Data.Instance.DialogList[know_num].name)
                   return false;
            else
            {
                know_num++;
                m_text = Global_Data.Instance.DialogList[know_num - 1].text;
                return true;
            }
        }
    }

    private bool ItemClassification(int i)
    {
        switch(i)
        {
            case 1:
                if (Player_s.GetPotion() >= 7)
                    return false;
                else
                    //Player_s.SetPotion(Player_s.GetPotion() + 1);
                return true;
            case 2:
                Player_s.SetAttack1_type(1);
                return true;
            case 3:
                Player_s.SetAttack1_type(2);
                return true;
            case 4:
                Player_s.SetAttack1_type(3);
                return true;
            case 5:
                Player_s.SetAttack2_type(11);
                return true;
            case 6:
                Player_s.SetAttack2_type(12);
                return true;
            case 7:
                Player_s.SetAttack2_type(13);
                return true;
        }
        return false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           if (con_first && upok && downok)
            {
                typing = StartCoroutine(_typing());
                if (!GetNextText())
                {
                    con_first = false;
                    upok = false;
                    downok = false;
                    know_num = 0;
                    StopMethod(typing);

                    dialog_end = true;
                }
            }
            else if (colok && Global_Data.Instance.IsIngame && con_first == false)
            {
                aniok = true;
                playerconversation.SetActive(true);
                con_first = true;
            }
            if (itemok == true)
            {
                if (ItemClassification(enter_item.GetComponent<DropItemNum>().GetNum()))
                {
                    iteminforUI.SetActive(false);
                    enter_item.GetComponent<DropItemNum>().DestroyedSelf();
                    itemok = false;
                }
            }

        }
        if (aniok)//모션 세팅 끝나면
        {
           
            C_Uidown = StartCoroutine(UIDown(playingui));
            C_Uiup = StartCoroutine(UIUp(playerconversation));

            if (upok && downok)//모션이 끝나면
            {
             
                time = 0f;
                StopMethod(C_Uidown);
                StopMethod(C_Uiup);
                aniok = false;
            }

        }
       

        if(dialog_end)
        {
            C_Uidown = StartCoroutine(UIDown(playerconversation));
            C_Uiup = StartCoroutine(UIUp(playingui));
            if(upok&&downok)
            {
             
                dialog_end = false;
                upok = false;
                downok = false; 
                StopMethod(C_Uidown);
                StopMethod(C_Uiup); 
                playerconversation.SetActive(false);
            }
        }

      


    }
}
