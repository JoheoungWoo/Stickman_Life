using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public int Count; // 튜토리얼 진행 버튼
    public SoundManager SoundManager;

    // 안내판
    public GameObject Board01;
    public GameObject Board02;
    public GameObject Board03;
    public GameObject Board04;
    public GameObject Board05;
    public GameObject Board06;
    public GameObject Board07;
    public GameObject Board08;
    public GameObject Board09;
    public GameObject Board10;
    public GameObject Board11;
    public GameObject Board12;
    public GameObject Board13;
    public GameObject Board14;
    public GameObject Board15;

    // 오브젝트
    public GameObject LVandGold;
    public GameObject Exp;
    public GameObject Diseases;
    public GameObject TimeUI;
    public GameObject SpeedTwiceBtn;
    public GameObject EscapeBtn;
    public GameObject Coins;
    public GameObject StatusArea;
    public GameObject Column1;
    public GameObject Column2;
    public GameObject Column3;
    public GameObject ButtonArea;

    public void Awake()
    {
        SoundManager.PlayMainBgm();
    }

    public void Update()
    {
        switch (Count)
        {
            case 0:
                break;
            case 1:
                Board01.gameObject.SetActive(false);
                Board02.gameObject.SetActive(true);
                LVandGold.gameObject.SetActive(true);
                Exp.gameObject.SetActive(true);
                break;
            case 2:
                Board02.gameObject.SetActive(false);
                LVandGold.gameObject.SetActive(false);
                Exp.gameObject.SetActive(false);

                Board03.gameObject.SetActive(true);
                Diseases.gameObject.SetActive(true);
                break;
            case 3:
                Board03.gameObject.SetActive(false);
                Diseases.gameObject.SetActive(false);

                Board04.gameObject.SetActive(true);
                TimeUI.gameObject.SetActive(true);
                SpeedTwiceBtn.gameObject.SetActive(true);
                EscapeBtn.gameObject.SetActive(true);
                break;
            case 4:
                Board04.gameObject.SetActive(false);
                TimeUI.gameObject.SetActive(false);
                SpeedTwiceBtn.gameObject.SetActive(false);
                EscapeBtn.gameObject.SetActive(false);

                Board05.gameObject.SetActive(true);
                Coins.gameObject.SetActive(true);
                break;
            case 5:
                Board05.gameObject.SetActive(false);
                Coins.gameObject.SetActive(false);

                Board06.gameObject.SetActive(true);
                StatusArea.gameObject.SetActive(true);
                break;
            case 6:
                Board06.gameObject.SetActive(false);

                Board07.gameObject.SetActive(true);
                Column2.gameObject.SetActive(false);
                Column3.gameObject.SetActive(false);
                break;
            case 7:
                Board07.gameObject.SetActive(false);

                Board08.gameObject.SetActive(true);
                Column1.gameObject.SetActive(false);
                Column2.gameObject.SetActive(true);
                break;
            case 8:
                Board08.gameObject.SetActive(false);

                Board09.gameObject.SetActive(true);
                break;
            case 9:
                Board09.gameObject.SetActive(false);

                Board10.gameObject.SetActive(true);
                Column2.gameObject.SetActive(false);
                Column3.gameObject.SetActive(true);
                break;
            case 10:
                Board10.gameObject.SetActive(false);

                Board11.gameObject.SetActive(true);
                break;
            case 11:
                Board11.gameObject.SetActive(false);
                StatusArea.gameObject.SetActive (false);

                Board12.gameObject.SetActive(true);
                ButtonArea.gameObject.SetActive (true);
                break;
            case 12:
                Board12.gameObject.SetActive(false);

                Board13.gameObject.SetActive(true);
                break;
            case 13:
                Board13.gameObject.SetActive(false);

                Board14.gameObject.SetActive(true);
                break;
            case 14:
                Board14.gameObject.SetActive(false);
                ButtonArea.gameObject.SetActive(false);

                Board15.gameObject.SetActive(true);
                break;
        }
    }

    public void Next()
    {
        if (Count == 14)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            SceneManager.LoadScene("Loading");
        }
        else
        {
            SoundManager.PlayBgm3();
            Count++;
        }
    }
}
