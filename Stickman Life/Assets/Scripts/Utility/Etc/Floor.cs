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
            myCamera = Camera.main; // Camera.main�� ����ϸ� ���� ī�޶� ���� ������ �� �ֽ��ϴ�.
        }

        if (myCamera != null)
        {
            float baseHeight = 16f; // ���� ���� ũ�⸦ 16���� ����
            float desiredHeight = -1f;

            // ���ϴ� ���� ���
            float ratio = desiredHeight / baseHeight;

            // ���� ī�޶� ���� ���
            float actualHeight = myCamera.pixelHeight / myCamera.pixelWidth * myCamera.orthographicSize * 2;

            // ���ο� ī�޶� ���� ����
            float newOrthographicSize = actualHeight * ratio / 2;

            Debug.Log("New Orthographic Size: " + newOrthographicSize);

            // y ��ǥ�� ���ϴ� ���̷� ����
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, newOrthographicSize);
        }
        else
        {
            Debug.LogError("ī�޶� ã�� �� �����ϴ�.");
        }
    }
}
