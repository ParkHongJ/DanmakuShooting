using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : AttackPattern
{
    // Start is called before the first frame update
    private LineRenderer lineRenderer;
    public Transform firePos;
    // Use this for initialization
    void Start()
    {
        //라인렌더러 설정
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
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

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePos.position);
            int layerMask = (1 << LayerMask.NameToLayer("Bullet"));  // Everything에서 Bullet 레이어만 제외하고 충돌 체크함
            layerMask = ~layerMask;
            RaycastHit _hit;
            if (Physics.Raycast(firePos.position, firePos.forward, out _hit, range, layerMask))
            {
                Debug.Log(_hit.collider.tag);
                if (_hit.transform.CompareTag("Enemy") || _hit.transform.CompareTag("Wall"))
                {
                    lineRenderer.SetPosition(1, _hit.point);
                }
            }
            else //맞은게 없다면
            {
                lineRenderer.SetPosition(1, firePos.position + firePos.forward * range);
                Debug.Log("실행댐");
            }
        }
    }
    void DisableEffects()
    {
        lineRenderer.enabled = false;
    }
}
