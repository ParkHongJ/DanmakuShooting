using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject m_player = null; // 플레이어 오브젝트
    public int m_mode = 0;
    public float distance = 7.0f;
    // Start is called before the first frame update
    void Start()
    {
        if(m_player == null)
        {
            Debug.Log("플레이어 카메라 - 플레이어 오브젝트가 지정이 안됐습니다.");
            m_player = GameObject.FindWithTag("Player").gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player == null)
            return;
        if( m_mode == 0 )
            this.transform.position = m_player.transform.position + new Vector3(0.0f, distance, -distance);
    }
}
