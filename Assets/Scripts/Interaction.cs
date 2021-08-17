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
    public GameObject canvas;
    public Camera uicamera;
    GameObject tmp;
    // Start is called before the first frame update
    private Coroutine C_Uiup, C_Uidown,typing;
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
        if(other.tag=="item")
        {
            int itemnum = other.GetComponent<DropItemNum>().GetNum();
            tmp = Resources.Load<GameObject>("Prefab/item_infor");
            tmp.gameObject.transform.SetParent(canvas.transform);
            tmp.transform.localPosition = uicamera.WorldToScreenPoint(other.transform.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //print("exit" + "\n" + "pp");
        //colider_n = null;
        //colok = false;
        ////npc = null;
        //con_first = false;
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
                print("1");
                aniok = true;
                playerconversation.SetActive(true);
                con_first = true;
            }
        }
        if (aniok)//모션 세팅 끝나면
        {
           
            C_Uidown = StartCoroutine(UIDown(playingui));
            C_Uiup = StartCoroutine(UIUp(playerconversation));

            if (upok && downok)//모션이 끝나면
            {
                print("2");
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
