using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum LoadDataStatus {Load, Save, Title}

public class LoadingSceneManager : MonoBehaviour
{
    public Image LoadingGage;

    float time;
    public float fTime; // �������� ���ʸ��� �� ���ΰ�?
    float a;

    float b;
    public int GageText; // �ε����� �� �ۼ�Ʈ���� ǥ���ϴ� ����

    public TMP_Text Text; // �ε� �ۼ�Ʈ ǥ�ÿ� �ؽ�Ʈ

    public int LoadData; // �ε�� ���� ��������

    public bool Tutorial; // Ʃ�丮�� �� �̵���

    public void Awake()
    {
        Tutorial = PlayerPrefs.HasKey("Tutorial");
        LoadData = PlayerPrefs.GetInt("Load", LoadData);
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        Debug.Log("�ڷ�ƾ ����");
        Time.timeScale = 1;

        while (a < 1f)
        {
            time += Time.deltaTime / fTime;
            a = Mathf.Lerp(0, 1, time);
            LoadingGage.fillAmount = a;

            b = time * 100;
            GageText = (int)b;

            if (GageText > 100)
                GageText = 100;

            Text.text = GageText + " %";

            yield return null;
        }

        if (GageText >= 100)
        {
            WhatMove(LoadData);
        }

        yield return null;
    }


    public void WhatMove(int LoadData)
    {
        if (Tutorial == false)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            switch (LoadData)
            {
                case (int)LoadDataStatus.Load:
                case (int)LoadDataStatus.Save:
                    //Ÿ��Ʋ���� �ΰ�������
                    SceneManager.LoadScene("Main");
                    break;
                case (int)LoadDataStatus.Title:
                    //�ΰ��ӿ��� Ÿ��Ʋ��
                    SceneManager.LoadScene("Start");
                    break;
                default:
                    break;
            }
        }
    }

}
