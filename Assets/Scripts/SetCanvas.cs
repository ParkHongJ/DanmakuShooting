using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanvas : MonoBehaviour
{
    private Camera uiCamera; //UI 카메라를 담을 변수
    private Canvas canvas; //캔버스를 담을 변수
    private RectTransform rectParent; //부모의 rectTransform 변수를 저장할 변수
    private RectTransform rectHp; //자신의 rectTransform 저장할 변수

    public Vector3 offset = Vector3.zero; 
    private Transform item; //위치
    public Interaction interaction_item;
    private bool can=false;
    void Start()
    {
        canvas = GetComponentInParent<Canvas>(); //부모가 가지고있는 canvas 가져오기, Enemy HpBar canvas임
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    //LateUpdate는 update 이후 실행함, 적의 움직임은 Update에서 실행되니 움직임 이후에 HpBar를 출력함
    private void LateUpdate()
    {
        //if (interaction_item.enter_item == null)
        //    can = false;
        //else
        //    can = true;
        //if(can)
        //{
        //    item = interaction_item.enter_item.transform;
        //    var screenPos = Camera.main.WorldToScreenPoint(item.position + offset); //월드좌표(3D)를 스크린좌표(2D)로 변경, offset은 오브젝트 머리 위치


        //    var localPos = Vector2.zero;
        //    RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);

        //    rectHp.localPosition = localPos;
        //}    
        item = interaction_item.enter_item.transform;
        var screenPos = Camera.main.WorldToScreenPoint(item.position + offset); //월드좌표(3D)를 스크린좌표(2D)로 변경, offset은 오브젝트 머리 위치


        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);

        rectHp.localPosition = localPos;
    }
}
