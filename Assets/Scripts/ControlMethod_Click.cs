using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlMethod_Click : MonoBehaviour
{
    public GameObject L, R;
    private int num = 1;
    float time = 2.0f;
    private float hp;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void reseting()
    {
        num = 1;
        R.GetComponent<Animator>().speed = 0.0f;
        L.GetComponent<Animator>().speed = 0.0f;
        time = 2.0f;
    }
    public void restart()
    {
        L.GetComponent<Animator>().speed = 1.0f;
    }
    void Play(int n)
    {
        switch (n)
        {
            case 1:
                animator.SetInteger("moving", 2);
                R.GetComponent<Animator>().speed = 0.0f;
                R.SetActive(false);
                L.SetActive(true);
                L.GetComponent<Animator>().speed = 1.0f;
                break;
            case 2:
                animator.SetInteger("moving", 3);
                L.GetComponent<Animator>().speed = 0.0f;
                L.SetActive(false);
                R.SetActive(true);
                R.GetComponent<Animator>().speed = 1.0f;
                break;
        }

    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= 2.0f)
        {
            print(time);
            time = 0.0f;
            Play(num);

            if (num != 2)
                num++;
            else
                num = 1;


        }

    }
}
