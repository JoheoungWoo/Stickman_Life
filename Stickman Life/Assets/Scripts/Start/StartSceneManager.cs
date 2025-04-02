using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartSceneManager : MonoBehaviour
{
    public Image LoadingGage;

    float time;
    public float fTime; // 게이지가 몇초만에 찰 것인가?
    float a;

    float b;
    public int GageText; // 로딩까지 몇 퍼센트인지 표시하는 정수

    public TMP_Text Text; // 로딩 퍼센트 표시용 텍스트

    public GameObject Load; // 로드 게이지 오브젝트

    public GameObject StartButton; // 게임시작 버튼

    public void Awake()
    {
        StartCoroutine(Loading());
    }

    #region 로딩 연출 제어부

    IEnumerator Loading()
    {

        while (a < 1f)
        {
            time += Time.deltaTime / fTime;
            a = Mathf.Lerp(0, 1, time);
            LoadingGage.fillAmount = a;

            b = time * 100;
            GageText = (int)b;

            Text.text = GageText + " %";

            yield return null;
        }

        if (GageText >= 100)
        {
            LoadDelete01();
        }

        yield return null;
    }

    public void LoadDelete01()
    {
        Invoke("LoadDelete02", 2f);
    }

    public void LoadDelete02()
    {
        Load.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(true);
    }

    #endregion
}
