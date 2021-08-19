using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IngameUiManager : MonoBehaviour
{
    public GameObject PlayingUI;
    public GameObject PlayerConversation;
    public GameObject Option_click_Ingame;
    public GameObject GameoverBlur;
    public GameObject GameoverT;
    public GameObject AnyButtonclickT;

    public Slider OptionIngame_BGSlider;
    public Slider OptionIngame_SFXSlider;
    public Player Player_s;
    public void Option_X_Ingame()
    {
        Option_click_Ingame.SetActive(false);
    }

    public void Option_GoToMenu()
    {
        Option_click_Ingame.SetActive(false);
        //MainPage.SetActive(true);
        Global_Data.Instance.IsIngame = false;
    }




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //게임 중 옵션 클릭 버튼 눌렀는지 확인
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Global_Data.Instance.IsIngame == true)
            {
                if (Option_click_Ingame.activeSelf == false)
                {
                    Option_click_Ingame.SetActive(true);
                }
            }
        }
        //게임 오버가 되었으면
        if (Global_Data.Instance.IsGameOver == true)
        {
            GameoverBlur.SetActive(true);
            GameoverT.SetActive(true);
            GameoverBlur.GetComponent<Blur>().enabled = true;
            GameoverT.GetComponent<Blur>().enabled = true;
        }
        if (GameoverT.GetComponent<Blur>().blurok == true)
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
                    //MainPage.SetActive(true);
                    Global_Data.Instance.IsIngame = false;
                    PlayingUI.SetActive(false);
                    Option_click_Ingame.SetActive(false);
                    //-----------------------옵션 정보들 mainpage 옵션에 넘겨주는 작업---------------------
                    // Option_BGSlider.value = OptionIngame_BGSlider.value;
                    //  Option_SFXSlider.value = OptionIngame_SFXSlider.value;
                }
            }
        }
    }
}
