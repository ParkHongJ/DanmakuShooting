using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Blur : MonoBehaviour
{
    float time = 0;
    public bool blurok = false;
    public float Alpha = 0.8f;
    Color m_color;


    public void Bluring()
    {
        if(this.gameObject.tag=="Text")
        {
            if (time / 3 < Alpha)
            {
                m_color = new Color(m_color.r, m_color.g, m_color.b, time / 3);
                GetComponent<Text>().color = m_color;
                time += Time.deltaTime;
            }
            else
            {
                blurok = true;
            }
        }
      if(this.gameObject.tag=="Image")
        {
            if (time / 3 < Alpha)
            {
                m_color = new Color(m_color.r, m_color.g, m_color.b, time / 3);
                GetComponent<Image>().color = m_color;
                time += Time.deltaTime;
            }
            else
            {
                blurok = true;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if(tag=="Image")
        {
            m_color = this.gameObject.GetComponent<Image>().color;
        }
       if(tag=="Text")
        {
            m_color = this.gameObject.GetComponent<Text>().color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!blurok)
        {
            Bluring();
        }
    }
}
