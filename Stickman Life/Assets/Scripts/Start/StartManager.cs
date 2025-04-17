using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    public GameObject LogoScene;
    public GameObject MenuScene;

    public int StartScene; //  ��𿡼� ������ ���ΰ�?

    public void Start()
    {
        StartScene = PlayerPrefs.GetInt("Load", StartScene);

        if (StartScene == 2)
        {
            LogoScene.gameObject.SetActive(false);
            MenuScene.gameObject.SetActive(true);
            PlayerPrefs.DeleteKey("Load");
        }
    }
}
