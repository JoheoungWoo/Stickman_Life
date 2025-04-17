using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    public GameObject AchievementScene; // ���� ��
    public GameObject MenuScene; // �޴� ��
    public GameObject CheckScene; // üũ ��
    public JsonDataManager jsonDataManager; //JSON ������ ����

    public Button Continue; // �̾��ϱ� ��ư

    public SoundManager soundManager; // ����Ŵ���

    public bool Tutorial; // Ʃ�丮�� �Ϸ� ����

    public void Awake()
    {
        soundManager.PlayMainBgm();
    }

    private void Start()
    {
        jsonDataManager = new JsonDataManager();
        
        //üũ
        if(jsonDataManager.CheckExistFile("playerData"))
        {

        } 
        else
        {
            Continue.interactable = false;
        }
    }

    public void AchievementSceneShift() // ���� ������ �̵�
    {
        soundManager.PlayBgm3();
        MenuScene.gameObject.SetActive(false);
        AchievementScene.gameObject.SetActive(true);
    }

    public void MenuSceneShift() // �޴� ������ �̵�
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

        //üũ
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
