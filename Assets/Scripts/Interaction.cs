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
    //private Vector3 ori_playconver;
   // private Vector3 ori_playui;
    private float time;
    private bool aniok = false;
    bool downok = false;
    bool upok = false;
    bool dialog_end = false;
    //NPC npc;
    string n_name;
    int npc_num;
    int know_num;
    public Text p_dialog;//대화 text
    public Image p_image;//대화 얼굴
    public Image skill1, skill2;//스킬 바뀌는거
    public string m_text;
    private bool con_first;
    public Camera uicamera;
    public GameObject tmp;
    private bool itemok = false;
    public GameObject iteminforUI;
    public GameObject enter_item;
    public Text item_infor_text;
    // Start is called before the first frame update
    private Coroutine C_Uiup, C_Uidown, typing;
    public Player Player_s;
    public Text potion_amount;
    private IEnumerator _typing()
    {
        //yield return new WaitForSeconds(2f);
        for (int i = 0; i <= m_text.Length; i++)
        {
            p_dialog.text = m_text.Substring(0, i);
            yield return new WaitForSeconds(0.02f);
        }
    }
    private void SetImage(int num)
    {
        
        //print("Faces/" + Global_Data.Instance.DialogList[num - 1].image_n);
        p_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Faces/" + Global_Data.Instance.DialogList[num - 1].image_n);
        p_image.gameObject.SetActive(true);
    }

    private void SetSkillIcon(Image image,int num)
    {
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icon/" + Global_Data.Instance.ItemList[num - 1].modelname);
    }
    
    void Start()
    {
        //ori_playconver = playerconversation.transform.position;
        //ori_playui = playingui.transform.position;
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
            string[] spstring = Global_Data.Instance.ItemList[itemnum - 1].explan.Split('&');
            item_infor_text.text = spstring[0] + "\n" + spstring[1];
            itemok = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "item")
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

    public void PlayDialog(int num)
    {

        npc_num = num;
        colok = true;
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
        else if (colok && Player_s.GetIsAlive() && con_first == false)
        {
            aniok = true;
            playerconversation.SetActive(true);
            con_first = true;
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
        if (dialog_end)
        {
            C_Uidown = StartCoroutine(UIDown(playerconversation));
            C_Uiup = StartCoroutine(UIUp(playingui));
            if (upok && downok)
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
    void StopMethod(Coroutine co)
    {
        if (co != null)
        {
            StopCoroutine(co);
        }
    }

    bool GetNextText()
    {
        if (know_num == 0)
        {
            know_num = npc_num;
            m_text = Global_Data.Instance.DialogList[know_num - 1].text;
            SetImage(know_num);
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
                SetImage(know_num);
                return true;
            }
        }
    }

    private bool ItemClassification(int i)
    {
        switch (i)
        {
            case 1:
                if (Player_s.GetPotion() >= 7)
                    return false;
                else
                {
                    Player_s.SetPotion(Player_s.GetPotion() + 1);
                    potion_amount.text = Player_s.GetPotion().ToString();
                }
                return true;
            case 2:
                SetSkillIcon(skill1,2);
                Player_s.SetAttack1_type(1);
                return true;
            case 3:
                SetSkillIcon(skill1, 3);
                Player_s.SetAttack1_type(2);
                return true;
            case 4:
                SetSkillIcon(skill1, 4);
                Player_s.SetAttack1_type(3);
                return true;
            case 5:
                SetSkillIcon(skill2, 5);
                Player_s.SetAttack2_type(11);
                return true;
            case 6:
                SetSkillIcon(skill2, 6);
                Player_s.SetAttack2_type(12);
                return true;
            case 7:
                SetSkillIcon(skill2, 7);
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
            else if (colok && Player_s.GetIsAlive() && con_first == false)
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
                    print(Global_Data.Instance.ItemList[enter_item.GetComponent<DropItemNum>().GetNum() - 1].itemname);
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


        if (dialog_end)
        {
            C_Uidown = StartCoroutine(UIDown(playerconversation));
            C_Uiup = StartCoroutine(UIUp(playingui));
            if (upok && downok)
            {
                colider_n.gameObject.GetComponent<NPC>().DestroySelf();
                dialog_end = false;
                upok = false;
                downok = false;
                StopMethod(C_Uidown);
                StopMethod(C_Uiup);
                playerconversation.SetActive(false);
            }
        }

        if (Global_Data.Instance.st1_mon <= 0)
        {
            PlayDialog(28);
        }
        if (Global_Data.Instance.st2_mon <= 0)
        {
            PlayDialog(46);
        }

    }
}
