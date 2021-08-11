using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public GameObject WASD, Space, QE, RLClick;
    public GameObject fade;
    public GameObject player;
    private int controlmethod_page = 1;
    public void ControlMethod_Button()
    {
        ControlMethod_click.SetActive(true);
        WASD.GetComponent<ControlMethod_WASD>().restart();
        QE.GetComponent<ControlMethod_QE>().restart();
        RLClick.GetComponent<ControlMethod_Click>().restart();
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
        WASD.SetActive(true);
        Space.SetActive(false);
        QE.SetActive(false);
        RLClick.SetActive(false);
        WASD.GetComponent<ControlMethod_WASD>().reseting();
        QE.GetComponent<ControlMethod_QE>().reseting();
        RLClick.GetComponent<ControlMethod_Click>().reseting();
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

    public void GameStart()
    {
        SceneManager.LoadScene("LHJ");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void SetControlMethodPage(int num)
    {
        switch(num)
        {
            case 1:
                player.GetComponent<Animator>().SetInteger("moving", 1);
                WASD.SetActive(true);
                RLClick.SetActive(false);
                break;
            case 2:
                player.transform.localRotation = Quaternion.Euler(0, 180, 0);
                player.GetComponent<Animator>().SetInteger("moving", 4);
                WASD.SetActive(false);
                Space.SetActive(true);
                break;
            case 3:
                Space.SetActive(false);
                QE.SetActive(true);
                player.GetComponent<Animator>().SetInteger("moving", 0);
                break;
            case 4:
                QE.SetActive(false);
                RLClick.SetActive(true);
                player.GetComponent<Animator>().SetInteger("moving", 2);
                break;
        }
        //fade.SetActive(false);
        return;
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
                    //-----------------------옵션 정보들 mainpage 옵션에 넘겨주는 작업---------------------
                    Option_BGSlider.value = OptionIngame_BGSlider.value;
                    Option_SFXSlider.value = OptionIngame_SFXSlider.value;
                }
            }
        }
        if(Global_Data.Instance.IsIngame==false&& ControlMethod_click.activeSelf==true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (controlmethod_page == 4)
                {
                    ControlMethod_X();
                    controlmethod_page = 1;
                }
                else
                    controlmethod_page += 1;
                SetControlMethodPage(controlmethod_page);
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
