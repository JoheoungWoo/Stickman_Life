using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Action
{
    private float nowTimer = 0f;
    private float maxTimer;

    private int downFood;      //2씩 증가

    private MainData mainData;

    public Food(MainData mainData)
    {
        this.mainData = mainData;
        //CheckFoodPoint();
    }

    public bool UpFoodStatus(int plusValue, int price)
    {
        //골드 체크
        if(mainData.nowGold - price < 0)
        {
            Debug.Log("골드가 부족합니다.");
            return false;
        }

        //골드 차감
        mainData.nowGold -= price;

        //데이터 적용
        if (CheckFoodPoisoning(plusValue))
        {
            mainData.SetFoodPoint(50);
        } else
        {
            if(mainData.GetFoodPoint() + plusValue >= mainData.maxFoodPoint)
            {
                mainData.SetFoodPoint(mainData.maxFoodPoint);
            } else
            {
                mainData.SetFoodPoint(mainData.GetFoodPoint() + plusValue);
            }
        }

        //UI 적용
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);
        DataManager.Instance.uiManager.ChangeFoodBarText(mainData.GetFoodPoint());



        return true;

    }

    public bool CheckFoodPoisoning(int plusFoodPoint = 0)
    {
        // 만일 식중독 상태이면서 포만감이 50 초과면 50으로 설정
        if (mainData.isFoodPoisoning && mainData.GetFoodPoint() + plusFoodPoint > 50)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void CheckFoodPoint()
    {
        if(CheckFoodPoisoning())
        {
            mainData.SetFoodPoint(50);
            DataManager.Instance.uiManager.ChangeFoodBarText(mainData.GetFoodPoint());
        }

        if (mainData.GetFoodPoint() > 50)
        {
            ChangeDelay();
        } else
        {
            ChangePenaltyDelay();
        }
    }

    public void ChangeDelay()
    {
        maxTimer = mainData.foodDelay[mainData.level - 1].delay;
        downFood = mainData.foodDelay[mainData.level - 1].value;
    }


    public void ChangePenaltyDelay()
    {
        maxTimer = mainData.foodPenaltyDelay[mainData.level - 1].delay;
        downFood = mainData.foodPenaltyDelay[mainData.level - 1].value;
    }

    public void DownFoodPoint(float timer)
    {
        CheckFoodPoint();

        nowTimer += timer;
        if (nowTimer >= maxTimer)
        {
            Debug.Log($"포만감 : 레벨{mainData.level} {nowTimer}마다 {downFood}만큼 감소됨");
            nowTimer = 0;

            mainData.SetFoodPoint(mainData.GetFoodPoint() - downFood <= 0 ?
                0 : mainData.GetFoodPoint() - downFood
                );
            DataManager.Instance.uiManager.ChangeFoodBarText(mainData.GetFoodPoint());

        }
    }

    public override void Play()
    {
        CheckFoodPoisoning();
        DownFoodPoint(Time.deltaTime);
    }
}
