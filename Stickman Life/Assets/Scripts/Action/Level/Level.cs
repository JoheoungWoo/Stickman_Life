using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : Action
{
    private MainData mainData;

    public Level(MainData mainData)
    {
        this.mainData = mainData;
    }

    private bool CheckExp(int nowExp, int maxExp)
    {
        if(nowExp >= maxExp && mainData.maxLevel > mainData.level)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private bool IsMaxLevel()
    {
        if (mainData.level >= mainData.maxLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpLevel()
    {
        //레벨업과 동시에 모든 질병 초기화
        mainData.isFoodPoisoning = false;
        mainData.isHallucination = false;
        mainData.isCold = false;
        mainData.isCancer = false;

        DataManager.Instance.uiManager.ChangeFoodPoisoning(mainData.isFoodPoisoning);
        DataManager.Instance.uiManager.ChangeHallucination(mainData.isHallucination);
        DataManager.Instance.uiManager.ChangeCold(mainData.isCold);
        DataManager.Instance.uiManager.ChangeCancer(mainData.isCancer);

        //경험치는 두배 단위로 증가한다.
        mainData.nowExp = 0;
        mainData.maxExp = mainData.maxExp * 2;
        mainData.level += 1;
        mainData.nowGold += 250;

        mainData.SetBodyPoint(mainData.maxBodyPoint);

        if (!mainData.isFoodPoisoning)
        {
            mainData.SetFoodPoint(mainData.originMaxFoodPoint);
        } else
        {
            mainData.SetFoodPoint(mainData.panaltyMaxFoodPoint);
        }

        mainData.SetHealthPoint(mainData.maxHealthPoint);
        mainData.SetLifePoint(mainData.maxLifePoint);
        mainData.SetMentalPoint(mainData.maxMentalPoint);

        //UI도 변경
        DataManager.Instance.uiManager.ChangeBodyBarText(mainData.GetBodyPoint());
        DataManager.Instance.uiManager.ChangeFoodBarText(mainData.GetFoodPoint());
        DataManager.Instance.uiManager.ChangeHealthBarText(mainData.GetHealthPoint());
        DataManager.Instance.uiManager.ChangeLifeBarText(mainData.GetLifePoint());
        DataManager.Instance.uiManager.ChangeMentalBarTextt(mainData.GetMentalPoint());

        DataManager.Instance.uiManager.ChangeLevelText(mainData.level);
        DataManager.Instance.uiManager.ChangeExpBarText();
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);

        //만렙 여부 체크
        if (IsMaxLevel())
        {
            mainData.isMaxLevel = true;
            DataManager.Instance.uiManager.ChangeExpBarText();
        }


    }
    public override void Play()
    {
        if (CheckExp(mainData.nowExp, mainData.maxExp))
        {
            UpLevel();
        }
    }
}
