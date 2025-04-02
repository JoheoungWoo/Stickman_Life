using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSceneManager : MonoBehaviour
{
    public StartFader SF;

    public GameObject LogoScene;
    public GameObject StartScene;

    public void Awake()
    {
        Invoke("Fade", 3f);
    }

    void Fade()
    {
        SF.FadeOut();
        Invoke("NextScene", 3f);
    }

    void NextScene()
    {
        SF.FadeOff();
        LogoScene.gameObject.SetActive(false);
        StartScene.gameObject.SetActive(true);
    }
}
