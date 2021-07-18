using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : ActtackPattern
{
    // Start is called before the first frame update
    private LineRenderer lineRenderer;
    public Transform firePos;
    // Use this for initialization
    void Start()
    {
        //라인렌더러 설정
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetColors(Color.red, Color.yellow);
        lineRenderer.SetWidth(0.1f, 0.1f);
    }


    float effectdisplaytime = 0.5f;
    float timer;
    float timeBetweenBullets = 0.15f;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && timer >= timeBetweenBullets)
        {
            Attack();
        }
        if (timer >= timeBetweenBullets * effectdisplaytime)
        {
            DisableEffects();
        }
    }
    public override void Attack() 
    {
        timer = 0f;
        Debug.DrawRay(firePos.position, firePos.forward * 10f, Color.black);

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePos.position);

            RaycastHit _hit;
            if (Physics.Raycast(firePos.position, firePos.forward, out _hit, range))
            {
                if (_hit.transform.CompareTag("Player"))
                {
                    Debug.Log("맞음");
                    lineRenderer.SetPosition(1, _hit.point);
                    Debug.Log("실행댐");

                }
            }
            else //맞은게 없다면
            {
                lineRenderer.SetPosition(1, firePos.position + firePos.forward * range);
                Debug.Log("실행댐");
            }
            Debug.Log(hit.point);
        }
    }
    void DisableEffects()
    {
        lineRenderer.enabled = false;
    }
}
