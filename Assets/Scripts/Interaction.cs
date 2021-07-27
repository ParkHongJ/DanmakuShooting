using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    Collider colider;
    bool colok = false;
    public GameObject playerconversation;
    public GameObject playingui;
    private Vector3 ori_playconver;
    private Vector3 ori_playui;
    private float time;
    public bool aniok = false;
    bool downok = false;
    bool upok = false;
    // Start is called before the first frame update
    void Start()
    {
        ori_playconver = playerconversation.transform.position;
        ori_playui = playingui.transform.position;
        time = 0.0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="quest")
        {
            colider = other;
            colok = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        colider = null;
        colok = false;
    }
    private void UIDown(GameObject gm)
    {
        Vector3 tmp = gm.transform.localPosition;
        tmp.y -= time;
        gm.transform.localPosition = tmp;
        if(tmp.y>=-520f)
            time += Time.deltaTime * 5f;
        if (tmp.y <- 520f)
        {
            tmp.y = -520f;
            gm.transform.localPosition = tmp;
            downok = true;
        }
    }
    private void UIUp(GameObject gm)
    {
        Vector3 tmp = gm.transform.localPosition;
        tmp.y += time;
        gm.transform.localPosition = tmp;
        if (tmp.y <= 0f)
            time += Time.deltaTime * 5f;
        if(tmp.y>0f)
        {
            tmp.y = 0f;
            gm.transform.localPosition = tmp;
            upok = true;
        }
    }
 
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(colok&&Global_Data.Instance.IsIngame)
            {
                aniok = true;
            }
        }
        if(aniok)
        {
            playerconversation.SetActive(true);
            UIDown(playingui);
            UIUp(playerconversation);
            if (upok&&downok)
                time = 0f;
        }
        //if(aniok==false&&upok&&downok)
        //{
        //    playerconversation.SetActive(false);
        //    UIDown(playerconversation);
        //    UIUp(playingui);
        //}
    }
}
