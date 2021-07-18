﻿using System.Collections;
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
    public GameObject Minimap;
    public Slider Option_BGSlider;
    public Slider Option_SFXSlider;
    public Slider OptionIngame_BGSlider;
    public Slider OptionIngame_SFXSlider;

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
        //게임 오버가 되었으면
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
        if (Global_Data.Instance.IsGameOver == true)//게임 오버 화면 나오고 마우스 클릭 했을때 나머지 원상태로 되돌리고, 메인 화면 으로 돌아가게 설정
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                if (GameoverT.GetComponent<Blur>().blurok == true && GameoverBlur.GetComponent<Blur>().blurok == true)
                {
                    GameoverBlur.GetComponent<Blur>().Resetting();
                    GameoverT.GetComponent<Blur>().Resetting();
                    GameoverBlur.SetActive(false);
                    GameoverT.SetActive(false);
                    GameoverBlur.GetComponent<Blur>().enabled = false;
                    GameoverT.GetComponent<Blur>().enabled = false;
                    AnyButtonclickT.SetActive(false);
                    AnyButtonclickT.GetComponent<Blink>().enabled = false;
                    Global_Data.Instance.IsGameOver = false;
                    MainPage.SetActive(true);
                    Global_Data.Instance.IsIngame = false;
                    PlayingUI.SetActive(false);
                    Option_click_Ingame.SetActive(false);
                    Option_BGSlider.value = OptionIngame_BGSlider.value;
                    Option_SFXSlider.value = OptionIngame_SFXSlider.value;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Global_Data.Instance.IsIngame == true)
            {
                if (Minimap.activeSelf == true)
                {
                    Minimap.SetActive(false);
                }
                else
                {
                    Minimap.SetActive(true);
                }
            }
        }

    }
}