using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMethod_Space : MonoBehaviour
{
    public GameObject Fade;
    public FadeOut Fade_com;
    public GameObject Space;
    public Animator animator;
    float x, y;
    void Setting(Vector3 pos)
    {
        Fade_com.SetPos(pos);
    }

    // Start is called before the first frame update
    void Start()
    {
        x = Space.GetComponent<RectTransform>().sizeDelta.x;
        y = Space.GetComponent<RectTransform>().sizeDelta.y;
        animator.SetInteger("moving", 4);
        print(x+" ,"+ y);
       Vector3 pos = Space.transform.localPosition;
        Setting(pos);
    }

  
    IEnumerator PlaySpace()
    {
       
        yield return null;
    }
}
