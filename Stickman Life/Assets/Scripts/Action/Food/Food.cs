using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Action
{
    private float nowTimer = 0f;
    private float maxTimer;

    private int downFood;      //2�� ����

    private MainData mainData;

    public Food(MainData mainData)
    {
        this.mainData = mainData;
        //CheckFoodPoint();
    }

    public bool UpFoodStatus(int plusValue, int price)
    {
        //��� üũ
        if(mainData.nowGold - price < 0)
        {
            Debug.Log("��尡 �����մϴ�.");
            return false;
        }

        //��� ����
        mainData.nowGold -= price;

        //������ ����
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

        //UI ����
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);
        DataManager.Instance.uiManager.ChangeFoodBarText(mainData.GetFoodPoint());



        return true;

    }

    public bool CheckFoodPoisoning(int plusFoodPoint = 0)
    {
        // ���� ���ߵ� �����̸鼭 �������� 50 �ʰ��� 50���� ����
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
            Debug.Log($"������ : ����{mainData.level} {nowTimer}���� {downFood}��ŭ ���ҵ�");
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
