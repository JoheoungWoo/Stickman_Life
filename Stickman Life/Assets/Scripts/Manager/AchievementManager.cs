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

    public Sprite[] archieveSprite; // 업적 아이콘

    public void Init(MainData mainData)
    {
        this.mainData = mainData;
        isInit = true;
    }

    #region 업적 관련 처리
    //업적 확인
    public bool CheckAchievement(AchievementList clear)
    {
        if (PlayerPrefs.HasKey(clear.ToString()) == true)
        {
            //값이 있을경우 실행
            if (PlayerPrefs.GetInt(clear.ToString(), 0) == 1)
            {
                //이미 값이 있으니 false
                return false;
            } else
            {
                //클리어 판정을 위하여 true로
                return true;
            }
            
        } else
        {
            //값이 없을 경우
            PlayerPrefs.SetInt(clear.ToString(), 0);
            return false;
        }
    }

    //업적 갱신
    public void UpdateAchievement()
    {
        //라이프 싸이클
        //max레벨에 도달한 경우
        if(mainData.level == mainData.maxLevel)
        {
            if(CheckAchievement(AchievementList.Achieve01))
            {
                ClearAchievement(AchievementList.Achieve01);
            }
        }

        //히포크라테스
        //모든 질병 1회이상 치료
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

        //골드 리치
        //보유 골드량 1000이상
        if (mainData.nowGold >= 1000)
        {
            if (CheckAchievement(AchievementList.Achieve03))
            {
                ClearAchievement(AchievementList.Achieve03);
            }
        }

        //벌크 업
        //모든 스킬 항목이 멕스레벨일 경우
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

        //넥스트 레벨
        //1600이상을 달성한경우
        if (mainData.nowExp >= 1600)
        {
            if (CheckAchievement(AchievementList.Achieve05))
            {
                ClearAchievement(AchievementList.Achieve05);
            }
        }

        //구르메
        //모든 종류 식사 1회 식사
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

        //도핑 마스터
        //건강보조제 10회이상 구매
        if (mainData.dietarysupplementCount >= 10)
        {
            if (CheckAchievement(AchievementList.Achieve07))
            {
                ClearAchievement(AchievementList.Achieve07);
            }
        }

        //타임리스 차이드
        //레빌1에서 15분 머무른 경우
        if (mainData.level == 1 && mainData.timeSpan / 60 >= 15)
        {
            if (CheckAchievement(AchievementList.Achieve08))
            {
                ClearAchievement(AchievementList.Achieve08);
            }
        }

        //이터널 라이프
        //2시간 버티기
        if (mainData.timeSpan / 60 >= 120)
        {
            if (CheckAchievement(AchievementList.Achieve09))
            {
                ClearAchievement(AchievementList.Achieve09);
            }
        }

        //엔드리스
        //3시간 버티기
        if (mainData.timeSpan / 60 >= 180)
        {
            if (CheckAchievement(AchievementList.Achieve10))
            {
                ClearAchievement(AchievementList.Achieve10);
            }
        }
    }

    //업적 클리어
    public void ClearAchievement(AchievementList clear)
    {
        if (PlayerPrefs.HasKey(clear.ToString()) == true)
        {
            PlayerPrefs.SetInt(clear.ToString(), 1);
            ClearPanel(clear);
        }
    }



    //업적 클리어 이후 클리어 판넬
    public void ClearPanel(AchievementList clear)
    {
        Debug.Log("이거뭐라나옴" + AchievementList.Achieve01);
        var obj = ONPanelWindow();
        switch(clear)
        {
            case AchievementList.Achieve01:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve01], "라이프 싸이클");
                break;
            case AchievementList.Achieve02:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve02], "히포크라테스");
                break;
            case AchievementList.Achieve03:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve03], "골드 리치");
                break;
            case AchievementList.Achieve04:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve04], "벌크 업");
                break;
            case AchievementList.Achieve05:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve05], "넥스트 레벨");
                break;
            case AchievementList.Achieve06:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve06], "구르메");
                break;
            case AchievementList.Achieve07:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve07], "도핑 마스터");
                break;
            case AchievementList.Achieve08:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve08], "타임리스 차일드");
                break;
            case AchievementList.Achieve09:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve09], "이터널 라이프");
                break;
            case AchievementList.Achieve10:
                SetData(obj, archieveSprite[(int)AchievementList.Achieve10], "엔드리스");
                break;
            default:
                SetData(obj,null,"없는 업적");
                break;
        }
        Invoke(nameof(OFFPanelWindow), 2f);
    }

    #endregion

    #region UI 처리
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

    #region 기타 처리
    public void SetData(GameObject obj,Sprite image, string text)
    {
        obj.transform.GetChild(0).GetComponent<Image>().sprite = image;
        obj.transform.GetChild(1).GetComponent<Text>().text = $"업적 '{text}' 달성!";
        
    }
    #endregion

    void Update()
    {
        if(!isInit)
        {
            return;
        }

        //데이터 알아서 갱신!
        UpdateAchievement();
    }
}
