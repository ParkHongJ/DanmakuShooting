using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlMethod_QE : MonoBehaviour
{
    public GameObject Fade;
    public GameObject Q, E;
    private int num = 1;
    float time = 0.0f;
    public Image hpbar;
    private float hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void reseting()
    {
        num = 1;
        Fade.GetComponent<Animator>().speed = 0.0f;
        time = 2.0f;
    }
    public void restart()
    {
        Fade.GetComponent<Animator>().speed = 1.0f;
    }
    void Play(int n)
    {
        switch (n)
        {
            case 1:
                Fade.transform.localPosition = Q.transform.localPosition;
                hpbar.fillAmount = 1;
                break;
            case 2:
                Fade.transform.localPosition = E.transform.localPosition;
                hpbar.fillAmount = 0.81f;
                break;
        }

    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= 2.0f)
        {
            time = 0.0f;
            Play(num);

            if (num != 2)
                num++;
            else
                num = 1;


        }

    }

}
