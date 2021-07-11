using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject MainPage;
    public GameObject ControlMethod_click;
    public GameObject Option_click;
    public GameObject Play_click;
    public GameObject Quit_click;
    public GameObject PlayingUI;
    public GameObject PlayerConversation;
    public GameObject Option_click_Ingame;
    public GameObject GameoverBlur;
    public GameObject GameoverT;
    public GameObject AnyButtonclickT;
    public void ControlMethod_Button()
    {
        ControlMethod_click.SetActive(true);
    }

    public void Option_Button()
    {
        Option_click.SetActive(true);
    }

    public void Play_Button()
    {
        Play_click.SetActive(true);
    }

    public void Quit_Button()
    {
        Quit_click.SetActive(true);
    }

    public void ControlMethod_X()
    {
        ControlMethod_click.SetActive(false);
    }

    public void Option_X()
    {
        Option_click.SetActive(false);
    }

    public void Option_X_Ingame()
    {
        Option_click_Ingame.SetActive(false);
    }

    public void Option_GoToMenu()
    {
        Option_click_Ingame.SetActive(false);
        MainPage.SetActive(true);
        Global_Data.Instance.IsIngame = false;
    }

    public void Play_X()
    {
        Play_click.SetActive(false);
    }

    public void Quit_Cancel()
    {
        Quit_click.SetActive(false);
    }

    //게임 종료
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //게임 중 옵션 클릭 버튼 눌렀는지 확인
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Global_Data.Instance.IsIngame==true)
            {
                if (Option_click_Ingame.activeSelf == false)
                {
                    Option_click_Ingame.SetActive(true);
                }
            }
        }

        if(Global_Data.Instance.IsGameOver==true)
        {
            GameoverBlur.SetActive(true);
            GameoverT.SetActive(true);
            GameoverBlur.GetComponent<Blur>().enabled = true;
            GameoverT.GetComponent<Blur>().enabled = true;
        }  
        if(GameoverT.GetComponent<Blur>().blurok==true)
        {
            AnyButtonclickT.SetActive(true);
            AnyButtonclickT.GetComponent<Blink>().enabled = true;
        }
    }
}
