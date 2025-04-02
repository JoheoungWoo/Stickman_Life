using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isClicking = false;
    public float currentClickTime = 0.0f;
    public float clickTimeThreshold = 2.0f; // 2�ʷ� ����, �ʿ信 ���� ���� ����

    void Update()
    {
        if (isClicking)
        {
            currentClickTime += Time.deltaTime;

            if (currentClickTime >= clickTimeThreshold)
            {
                // ���⿡ 2�� �̻� Ŭ�� ���� �� ������ �ڵ带 �߰��ϼ���.
                Debug.Log("Button clicked for more than 2 seconds!");
                ResetClickTimer();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Ŭ�� ����");
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
