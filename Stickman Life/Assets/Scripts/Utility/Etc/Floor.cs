using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Camera myCamera;

    private void Start()
    {
        if (!myCamera)
        {
            myCamera = Camera.main; // Camera.main을 사용하면 메인 카메라를 직접 가져올 수 있습니다.
        }

        if (myCamera != null)
        {
            float baseHeight = 16f; // 기준 세로 크기를 16으로 설정
            float desiredHeight = -1f;

            // 원하는 비율 계산
            float ratio = desiredHeight / baseHeight;

            // 실제 카메라 높이 계산
            float actualHeight = myCamera.pixelHeight / myCamera.pixelWidth * myCamera.orthographicSize * 2;

            // 새로운 카메라 높이 설정
            float newOrthographicSize = actualHeight * ratio / 2;

            Debug.Log("New Orthographic Size: " + newOrthographicSize);

            // y 좌표를 원하는 높이로 설정
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, newOrthographicSize);
        }
        else
        {
            Debug.LogError("카메라를 찾을 수 없습니다.");
        }
    }
}
