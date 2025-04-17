using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    public GameObject AchievementScene; // 업적 씬
    public GameObject MenuScene; // 메뉴 씬
    public GameObject CheckScene; // 체크 씬
    public JsonDataManager jsonDataManager; //JSON 데이터 관리

    public Button Continue; // 이어하기 버튼

    public SoundManager soundManager; // 사운드매니저

    public bool Tutorial; // 튜토리얼 완료 여부

    public void Awake()
    {
        soundManager.PlayMainBgm();
    }

    private void Start()
    {
        jsonDataManager = new JsonDataManager();
        
        //체크
        if(jsonDataManager.CheckExistFile("playerData"))
        {

        } 
        else
        {
            Continue.interactable = false;
        }
    }

    public void AchievementSceneShift() // 업적 씬으로 이동
    {
        soundManager.PlayBgm3();
        MenuScene.gameObject.SetActive(false);
        AchievementScene.gameObject.SetActive(true);
    }

    public void MenuSceneShift() // 메뉴 씬으로 이동
    {
        soundManager.PlayBgm3();
        AchievementScene.gameObject.SetActive(false);
        MenuScene.gameObject.SetActive(true);
    }

    public void GameExit()
    {
        soundManager.PlayBgm3();
        Application.Quit();
    }

    public void NewStart()
    {
        soundManager.PlayBgm3();

        //체크
        if (jsonDataManager.CheckExistFile("playerData"))
        {
            MenuScene.gameObject.SetActive(false);
            CheckScene.gameObject.SetActive(true);
        }
        else
        {
            //PlayerPrefs.SetInt("Load", (int)LoadDataStatus.Save);
            SceneManager.LoadScene("Loading");
        }
    }

    public void ReturnMenu()
    {
        soundManager.PlayBgm3();
        CheckScene.gameObject.SetActive(false);
        MenuScene.gameObject.SetActive(true);
    }

    public void DataClear()
    {
        jsonDataManager.ClearData("playerData");
        PlayerPrefs.SetInt("Load", (int)LoadDataStatus.Save);
        SceneManager.LoadScene("Loading");
    }

    public void GotoGame()
    {
        soundManager.PlayBgm3();

        if (jsonDataManager.CheckExistFile("playerData"))
        {
            PlayerPrefs.SetInt("Load", (int)LoadDataStatus.Load);
            SceneManager.LoadScene("Loading");
        }
    }
}
