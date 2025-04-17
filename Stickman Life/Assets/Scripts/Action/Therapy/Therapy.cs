using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Therapy : Action
{
    int maxHealtPoint = 100;

    private MainData mainData;

    public Therapy(MainData mainData)
    {
        this.mainData = mainData;
    }
    public void Recover(int healValue,int price, string recoverType = null)
    {
        if (recoverType == null)
        {
            Debug.Log("치료할 타입을 지정해주세요.");
            //return;
        }

        switch(recoverType)
        {
            case "FoodPoisning":
                break;
            case "Hallucination":
                break;
            case "Cold":
                break;
            case "Cancer":
                break;
            default:
                break;
        }

        if(mainData.nowGold >= price)
        {
            mainData.SetHealthPoint(mainData.GetHealthPoint() + healValue);
            mainData.nowGold -= price;

            //모든 질병 치료
            mainData.isFoodPoisoning = false;
            mainData.isHallucination = false;
            mainData.isCold = false;
            mainData.isCancer = false;

            DataManager.Instance.uiManager.ChangeFoodPoisoning(mainData.isFoodPoisoning);
            DataManager.Instance.uiManager.ChangeHallucination(mainData.isHallucination);
            DataManager.Instance.uiManager.ChangeCold(mainData.isCold);
            DataManager.Instance.uiManager.ChangeCancer(mainData.isCancer);

            //경험치 추가
            //mainData.AddExp(10);

            //UI 반영
            DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);
            DataManager.Instance.uiManager.ChangeGoldText(mainData.GetHealthPoint());
            DataManager.Instance.uiManager.ChangeExpBarText();
        }
    }
    #region 치료 코드 정의
    public void RecoverFoodPoisning()
    {
        mainData.isFoodPoisoning = false;
        DataManager.Instance.uiManager.ChangeFoodPoisoning(mainData.isFoodPoisoning);
    }

    public void RecoverHallucination()
    {
        mainData.isHallucination = false;
        DataManager.Instance.uiManager.ChangeHallucination(mainData.isHallucination);
    }
 
    public void RecoverCold()
    {
        mainData.isCold = false;
        DataManager.Instance.uiManager.ChangeCold(mainData.isCold);
    }
    
    public void RecoverCancer()
    {
        mainData.isCancer = false;
        DataManager.Instance.uiManager.ChangeCancer(mainData.isCancer);
    }
    #endregion


    public override void Play(string recoverType = null)
    {
        Debug.Log("치료 실행");
        /*
        if(mainData.GetHealthPoint() >= maxHealtPoint)
        {
            
        }
        */
        Recover(10, 5, recoverType);
    }
}
