using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isClicking = false;
    public float currentClickTime = 0.0f;
    public float clickTimeThreshold = 2.0f; // 2초로 설정, 필요에 따라 변경 가능

    void Update()
    {
        if (isClicking)
        {
            currentClickTime += Time.deltaTime;

            if (currentClickTime >= clickTimeThreshold)
            {
                // 여기에 2초 이상 클릭 중일 때 실행할 코드를 추가하세요.
                Debug.Log("Button clicked for more than 2 seconds!");
                ResetClickTimer();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("클릭 시작");
        isClicking = true;
        currentClickTime = 0.0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetClickTimer();
    }

    private void ResetClickTimer()
    {
        isClicking = false;
        currentClickTime = 0.0f;
    }
}
