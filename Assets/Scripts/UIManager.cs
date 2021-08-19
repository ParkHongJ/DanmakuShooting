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

    public Slider Option_BGSlider;
    public Slider Option_SFXSlider;


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
        SceneManager.LoadScene("MainScene");
        Global_Data.Instance.IsIngame = true;
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
                player.transform.localRotation = Quaternion.Euler(0, 90, 0);
                player.GetComponent<Animator>().SetInteger("moving", 4);
                WASD.SetActive(false);
                Space.SetActive(true);
                break;
            case 3:
                player.transform.localRotation = Quaternion.Euler(0, 180, 0);
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
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    if (Global_Data.Instance.IsIngame == true)
        //    {
        //        if (Minimap.activeSelf == true)
        //        {
        //            Minimap.SetActive(false);
        //        }
        //        else
        //        {
        //            Minimap.SetActive(true);
        //        }
        //    }
        //}

    }
}
