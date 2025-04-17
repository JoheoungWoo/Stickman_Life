using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AchievementList { Achieve01 , Achieve02 , Achieve03, Achieve04, Achieve05,
    Achieve06, Achieve07, Achieve08, Achieve09, Achieve10}

public class AchievementManager : MonoBehaviour
{
    private MainData mainData;
    public bool isInit = false;

    public GameObject panelObj;

    public Sprite[] archieveSprite; // ���� ������

    public void Init(MainData mainData)
    {
        this.mainData = mainData;
        isInit = true;
    }

    #region ���� ���� ó��
    //���� Ȯ��
    public bool CheckAchievement(AchievementList clear)
    {
        if (PlayerPrefs.HasKey(clear.ToString()) == true)
        {
            //���� ������� ����
            if (PlayerPrefs.GetInt(clear.ToString(), 0) == 1)
            {
                //�̹� ���� ������ false
                return false;
            } else
            {
                //Ŭ���� ������ ���Ͽ� true��
                return true;
            }
            
        } else
        {
            //���� ���� ���
            PlayerPrefs.SetInt(clear.ToString(), 0);
            return false;
        }
    }

    //���� ����
    public void UpdateAchievement()
    {
        //������ ����Ŭ
        //max������ ������ ���
        if(mainData.level == mainData.maxLevel)
        {
            if(CheckAchievement(AchievementList.Achieve01))
            {
                ClearAchievement(AchievementList.Achieve01);
            }
        }

        //����ũ���׽�
        //��� ���� 1ȸ�̻� ġ��
        if (mainData.foodPoisoningCount >= 1 &&
            mainData.hallucinationCount >= 1 &&
            mainData.coldCount >= 1 &&
            mainData.cancerCount >= 1)
        {
            if (CheckAchievement(AchievementList.Achieve02))
            {
                ClearAchievement(AchievementList.Achieve02);
            }
        }

        //��� ��ġ
        //���� ��差 1000�̻�
        if (mainData.nowGold >= 1000)
        {
            if (CheckAchievement(AchievementList.Achieve03))
            {
                ClearAchievement(AchievementList.Achieve03);
            }
        }

        //��ũ ��
        //��� ��ų �׸��� �߽������� ���
        if (mainData.lifeUpLevel == mainData.lifeUpMaxLevel &&
            mainData.healthUpLevel == mainData.healthUpMaxLevel &&
            mainData.healthRepairLevel == mainData.healthRepairMaxLevel &&
            mainData.goldUpLevel == mainData.goldUpMaxLevel)
        {
            if (CheckAchievement(AchievementList.Achieve04))
            {
                ClearAchievement(AchievementList.Achieve04);
            }
        }

        //�ؽ�Ʈ ����
        //1600�̻��� �޼��Ѱ��
        if (mainData.nowExp >= 1600)
        {
            if (CheckAchievement(AchievementList.Achieve05))
            {
                ClearAchievement(AchievementList.Achieve05);
            }
        }

        //������
        //��� ���� �Ļ� 1ȸ �Ļ�
        if (mainData.candyCount >= 1 &&
            mainData.chipCount >= 1 &&
            mainData.ramyunCount >= 1 &&
            mainData.kimbabCount >= 1 &&
            mainData.meatCount >= 1)
        {
            if (CheckAchievement(AchievementList.Achieve06))
            {
                ClearAchievement(AchievementList.Achieve06);
            }
        }

        //���� ������
        //�ǰ������� 10ȸ�̻� ����
        if (mainData.dietarysupplementCount >= 10)
        {
            if (CheckAchievement(AchievementList.Achieve07))
            {
                ClearAchievement(AchievementList.Achieve07);
            }
        }

        //Ÿ�Ӹ��� ���̵�
        //����1���� 15�� �ӹ��� ���
        if (mainData.level == 1 && mainData.timeSpan / 60 >= 15)
        {
            if (CheckAchievement(AchievementList.Achieve08))
            {
                ClearAchievement(AchievementList.Achieve08);
            }
        }

        //���ͳ� ������
        //2�ð� ��Ƽ��
        if (mainData.timeSpan / 60 >= 120)
        {
            if (CheckAchievement(AchievementList.Achieve09))
            {
                ClearAchievement(AchievementList.Achieve09);
            }
        }

        //���帮��
        //3�ð� ��Ƽ��
        if (mainData.timeSpan / 60 >= 180)
        {
            if (CheckAchievement(AchievementList.Achieve10))
            {
                ClearAchievement(AchievementList.Achieve10);
            }
        }
    }

    //���� Ŭ����
    public void ClearAchievement(AchievementList clear)
    {
        if (PlayerPrefs.HasKey(clear.ToString()) == true)
        {
            PlayerPrefs.SetInt(clear.ToString(), 1);
            ClearPanel(clear);
        }
    }



    //���� Ŭ���� ���� Ŭ���� �ǳ�
    public void ClearPanel(AchievementList clear)
    {
        Debug.Log("�̰Ź��󳪿�" + AchievementList.Achieve01);
        var obj = ONPanelWindow();
        switch(clear)
        {
            case AchievementList.Achieve01:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve01], "������ ����Ŭ");
                break;
            case AchievementList.Achieve02:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve02], "����ũ���׽�");
                break;
            case AchievementList.Achieve03:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve03], "��� ��ġ");
                break;
            case AchievementList.Achieve04:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve04], "��ũ ��");
                break;
            case AchievementList.Achieve05:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve05], "�ؽ�Ʈ ����");
                break;
            case AchievementList.Achieve06:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve06], "������");
                break;
            case AchievementList.Achieve07:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve07], "���� ������");
                break;
            case AchievementList.Achieve08:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve08], "Ÿ�Ӹ��� ���ϵ�");
                break;
            case AchievementList.Achieve09:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve09], "���ͳ� ������");
                break;
            case AchievementList.Achieve10:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve10], "���帮��");
                break;
            default:
                SetData(obj,null,"���� ����");
                break;
        }
        Invoke(nameof(OFFPanelWindow), 2f);
    }

    #endregion

    #region UI ó��
    public GameObject ONPanelWindow()
    {
        panelObj.SetActive(true);
        Invoke(nameof(OFFPanelWindow),2f);
        return panelObj;
    }

    public void OFFPanelWindow()
    {
        panelObj.SetActive(false);
    }
    #endregion

    #region ��Ÿ ó��
    public void SetData(GameObject obj,Sprite image, string text)
    {
        obj.transform.GetChild(0).GetComponent<Image>().sprite = image;
        obj.transform.GetChild(1).GetComponent<Text>().text = $"���� '{text}' �޼�!";
        
    }
    #endregion

    void Update()
    {
        if(!isInit)
        {
            return;
        }

        //������ �˾Ƽ� ����!
        UpdateAchievement();
    }
}
