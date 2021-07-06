using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainPage;
    public GameObject ControlMethod_click;
    public GameObject Option_click;
    public GameObject Play_click;
    public GameObject Quit_click;
    public GameObject PlayingUI;
    public GameObject PlayerConversation;

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
        
    }
}
