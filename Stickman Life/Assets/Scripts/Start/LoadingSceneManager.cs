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
    public float fTime; // 게이지가 몇초만에 찰 것인가?
    float a;

    float b;
    public int GageText; // 로딩까지 몇 퍼센트인지 표시하는 정수

    public TMP_Text Text; // 로딩 퍼센트 표시용 텍스트

    public int LoadData; // 로드된 씬의 목적변수

    public bool Tutorial; // 튜토리얼 씬 이동용

    public void Awake()
    {
        Tutorial = PlayerPrefs.HasKey("Tutorial");
        LoadData = PlayerPrefs.GetInt("Load", LoadData);
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        Debug.Log("코루틴 실행");
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
                    //타이틀에서 인게임으로
                    SceneManager.LoadScene("Main");
                    break;
                case (int)LoadDataStatus.Title:
                    //인게임에서 타이틀로
                    SceneManager.LoadScene("Start");
                    break;
                default:
                    break;
            }
        }
    }

}
