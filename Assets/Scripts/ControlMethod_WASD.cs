using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMethod_WASD : MonoBehaviour
{
    public GameObject Fade;
    public GameObject W, A, S, D;
    private int num = 1;
    float time = 2.0f;
    public Animator animator;
    public GameObject player;
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
        Quaternion qu ;
        
       
        switch (n)
        {
            case 1:
                animator.SetInteger("moving", 1);
                player.transform.localRotation = Quaternion.Euler(0, 180, 0);
                Fade.transform.localPosition = W.transform.localPosition;
                Fade.GetComponent<Animator>().speed = 1.0f;
                break;
            case 2:
                player.transform.localRotation = Quaternion.Euler(0, 270, 0);
                Fade.transform.localPosition = A.transform.localPosition;
                Fade.GetComponent<Animator>().speed = 1.0f;
                break;
            case 3:
                player.transform.localRotation = Quaternion.Euler(0, 0, 0);
                Fade.transform.localPosition = S.transform.localPosition;
                Fade.GetComponent<Animator>().speed = 1.0f;
                break;
            case 4:
                player.transform.localRotation = Quaternion.Euler(0, 90, 0);
                Fade.transform.localPosition = D.transform.localPosition;
                Fade.GetComponent<Animator>().speed = 1.0f;
                break;
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if (time>=2.0f)
        {
            time = 0.0f;
            Play(num);
           
            if (num != 4)
                num++;
            else
                num = 1;
            
            
        }

    }
}
