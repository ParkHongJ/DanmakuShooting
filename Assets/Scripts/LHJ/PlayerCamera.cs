using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject m_player = null; // 플레이어 오브젝트
    public float distance = 7.0f;
    public float rotateSpeed = 3.0f;
    public float rotateY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if(m_player == null)
        {
            Debug.Log("플레이어 카메라 - 플레이어 오브젝트가 지정이 안됐습니다.");
            m_player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, rotateY, transform.rotation.eulerAngles.z), rotateSpeed * Time.deltaTime);
        if (m_player == null)
            return;
        this.transform.position = m_player.transform.position + ((Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f) * Vector3.forward) * -distance) + new Vector3(0.0f, distance, 0.0f);
    }
}
