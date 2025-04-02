using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartSceneManager : MonoBehaviour
{
    public Image LoadingGage;

    float time;
    public float fTime; // �������� ���ʸ��� �� ���ΰ�?
    float a;

    float b;
    public int GageText; // �ε����� �� �ۼ�Ʈ���� ǥ���ϴ� ����

    public TMP_Text Text; // �ε� �ۼ�Ʈ ǥ�ÿ� �ؽ�Ʈ

    public GameObject Load; // �ε� ������ ������Ʈ

    public GameObject StartButton; // ���ӽ��� ��ư

    public void Awake()
    {
        StartCoroutine(Loading());
    }

    #region �ε� ���� �����

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
