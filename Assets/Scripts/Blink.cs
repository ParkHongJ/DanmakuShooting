using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Blink : MonoBehaviour
{
    float time = 0;
    float b_time = 0;
    Color m_color;

    private void Start()
    {
        m_color = this.gameObject.GetComponent<Text>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 1f) // 버프 지속시간 -3초
        {
            m_color = new Color(m_color.r, m_color.g, m_color.b, 1);
            GetComponent<Text>().color = m_color; // 처음엔 켜져있고
            time += Time.deltaTime;
        }
        else
        {
            m_color = new Color(m_color.r, m_color.g, m_color.b, 0);
            GetComponent<Text>().color = m_color;
            b_time += Time.deltaTime;
            if(b_time>=0.3f)
            {
                time = 0f;
                b_time = 0f;
            }
        }
       
    }
}
