using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class FadeOut : MonoBehaviour
{
    public float animTime = 2f;         // Fade 애니메이션 재생 시간 (단위:초).  

    private Image fadeImage;            // UGUI의 Image컴포넌트 참조 변수.  

    private float start = 0f;           // Mathf.Lerp 메소드의 첫번째 값.  
    private float end = 1f;             // Mathf.Lerp 메소드의 두번째 값.  
    private float time = 0f;            // Mathf.Lerp 메소드의 시간 값.  

    public bool isPlaying = false;     // Fade 애니메이션의 중복 재생을 방지하기위해서 사용.  
    void Awake()
    {
        // Image 컴포넌트를 검색해서 참조 변수 값 설정.  
        fadeImage = GetComponent<Image>();
    }
    public void SetPos(Vector3 pos)
    {
        this.gameObject.transform.localPosition = pos;
    }
    public void SetSize(float width, float height)
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

    }
    // Fade 애니메이션을 시작시키는 메소드.  
    public void StartFadeAnim()
    {
        // 애니메이션이 재생중이면 중복 재생되지 않도록 리턴 처리.  
        if (isPlaying == true)
            return;

        // Fade 애니메이션 재생.  
        StartCoroutine("PlayFadeOut");
    }

    // Fade 애니메이션 메소드.  
    IEnumerator PlayFadeOut()
    {
        // 애니메이션 재생중.  
        isPlaying = true;

        // Image 컴포넌트의 색상 값 읽어오기.  
        Color color = fadeImage.color;
        time = 0f;
        color.a = Mathf.Lerp(start, end, time);

        while (color.a < 1f)
        {
            // 경과 시간 계산.  
            // 2초(animTime)동안 재생될 수 있도록 animTime으로 나누기.  
            time += Time.deltaTime / animTime;

            // 알파 값 계산.  
            color.a = Mathf.Lerp(start, end, time);
            // 계산한 알파 값 다시 설정.  
            fadeImage.color = color;

            yield return null;
        }
        // 애니메이션 재생 완료.  
        isPlaying = false;
    }
    private void Update()
    {
        StartFadeAnim();
    }
}