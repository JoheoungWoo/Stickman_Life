using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disease : Action
{
    float nowTImer = 0f;
    float maxTimer = 60f;

    int diseasePercent;

    private MainData mainData;

    public Disease(MainData mainData)
    {
        this.mainData = mainData;
        ChangeDiease();
    }

    public void ChangeDiease()
    {
        diseasePercent = mainData.diseasePercentArr[mainData.level - 1].value;
    }
 
    #region 질병 획득
    public void GetAllDisease()
    {
        ChangeDiease();
        GetFoodPoisoning();
        GetHallucination();
        GetCold();
        GetCancer();
    }

    public void GetFoodPoisoning()
    {
        if(Random.Range(0, 100) <= diseasePercent && !mainData.isFoodPoisoning) 
        {
            mainData.isFoodPoisoning = true;
            Debug.Log($"{diseasePercent}확률로 식중독에 걸렸습니다.");
        }
    }

    public void GetHallucination()
    {
        if (Random.Range(0, 100) <= diseasePercent && !mainData.isHallucination)
        {
            mainData.isHallucination = true;
            Debug.Log($"{diseasePercent}확률로 환각에 걸렸습니다.");
        }
    }

    public void GetCold()
    {
        if (Random.Range(0, 100) <= diseasePercent && !mainData.isCold)
        {
            mainData.isCold = true;
            Debug.Log($"{diseasePercent}확률로 감기에 걸렸습니다.");
        }
    }

    public void GetCancer()
    {
        if (Random.Range(0, 100) <= diseasePercent && !mainData.isCancer)
        {
            mainData.isCancer = true;
            Debug.Log($"{diseasePercent}확률로 암에 걸렸습니다.");
        }
    }
    #endregion

    #region 질병 치료 및 아이템
    public int TreatFoodPoisoning(int minusGold)
    {
        //조건 체크
        if(mainData.nowGold - minusGold < 0)
        {
            Debug.Log("골드가 부족합니다. 식중독 치료 실패");
            return -1;
        } 
        if(!mainData.isFoodPoisoning)
        {
            Debug.Log("식중독 상태가 아닙니다.");
            return -2;
        }

        //데이터 반영
        mainData.isFoodPoisoning = false;
        mainData.nowGold -= minusGold;
        Debug.Log("감기 치료 완료");

        //UI 반영
        DataManager.Instance.uiManager.ChangeFoodPoisoning(mainData.isFoodPoisoning);
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);

        return 1;
    }

    public int TreatHallucination(int minusGold)
    {
        //조건 체크
        if (mainData.nowGold - minusGold < 0)
        {
            Debug.Log("골드가 부족합니다. 환각 치료 실패");
            return -1;
        }
        if (!mainData.isHallucination)
        {
            Debug.Log("환각 상태가 아닙니다.");
            return -2;
        }

        //데이터 반영
        mainData.isHallucination = false;
        mainData.nowGold -= minusGold;
        Debug.Log("환각 치료 완료");

        //UI 반영
        DataManager.Instance.uiManager.ChangeHallucination(mainData.isHallucination);
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);

        return 1;
    }

    public int TreatCold(int minusGold)
    {
        //조건 체크
        if (mainData.nowGold - minusGold < 0)
        {
            Debug.Log("골드가 부족합니다. 감기 치료 실패");
            return -1;
        }
        if (!mainData.isCold)
        {
            Debug.Log("감기 상태가 아닙니다.");
            return -2;
        }

        //데이터 반영
        mainData.isCold = false;
        mainData.nowGold -= minusGold;
        Debug.Log("감기 치료 완료");

        //UI 반영
        DataManager.Instance.uiManager.ChangeCold(mainData.isCold);
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);

        return 1;
    }

    public int TreatCancer(int minusGold)
    {
        //조건 체크
        if (mainData.nowGold - minusGold < 0)
        {
            Debug.Log("골드가 부족합니다. 암 치료 실패");
            return -1;
        }
        if (!mainData.isCancer)
        {
            Debug.Log("암 상태가 아닙니다.");
            return -2;
        }

        //데이터 반영
        mainData.isCancer = false;
        mainData.nowGold -= minusGold;
        Debug.Log("암 치료 완료");

        //UI 반영
        DataManager.Instance.uiManager.ChangeCancer(mainData.isCancer);
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);

        return 1;
    }

    public int TreatBodyPoint(int minusGold)
    {
        if (mainData.nowGold - minusGold < 0)
        {
            Debug.Log("골드가 부족합니다. 건강 보조제 실패");
            return -1;
        }

        //데이터 반영
        if (mainData.GetBodyPoint() + 60 >= mainData.maxBodyPoint)
        {
            mainData.SetBodyPoint(mainData.maxBodyPoint);
        } else
        {
            mainData.SetBodyPoint(mainData.GetBodyPoint() + 60);
        }
        mainData.nowGold -= minusGold;
        Debug.Log("건강보조제 완료");

        //UI 반영
        DataManager.Instance.uiManager.ChangeBodyBarText(mainData.GetBodyPoint());
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);

        return 1;
    }
    #endregion

    #region 질병에 의한 패널티
    public void CheckAllDisease()
    {
        CheckFoodPoisoning(mainData.isFoodPoisoning);
        CheckHallucination(mainData.isHallucination);
        CheckCold(mainData.isCold);
        CheckCancer(mainData.isCancer);
    }

    public void CheckFoodPoisoning(bool isFoodPoisoning)
    {
        if(isFoodPoisoning)
        {
            DataManager.Instance.uiManager.ChangeFoodPoisoning(isFoodPoisoning);
            Debug.Log("현재 식중독 상태입니다.");
        }
    }

    public void CheckHallucination(bool ishallucination)
    {
        if(ishallucination)
        {
            DataManager.Instance.uiManager.ChangeHallucination(ishallucination);
            Debug.Log("현재 환각 상태입니다.");
        }
    }

    public void CheckCold(bool isCold)
    {
        if(isCold)
        {
            DataManager.Instance.uiManager.ChangeCold(isCold);
            Debug.Log("현재 감기 상태입니다.");
        }
    }

    public void CheckCancer(bool isCancer)
    {
        if(isCancer)
        {
            DataManager.Instance.uiManager.ChangeCancer(isCancer);
            Debug.Log("현재 암 상태입니다.");
        }
    }
    #endregion

    public override void Play()
    {
        nowTImer += Time.deltaTime;
        if(nowTImer >= maxTimer)
        {
            nowTImer = 0;
            GetAllDisease();
        }
        CheckAllDisease();
    }
}
