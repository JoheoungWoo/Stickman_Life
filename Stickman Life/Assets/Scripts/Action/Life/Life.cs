using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Action
{
    private float nowTimer = 0f;
    private float maxTimer;

    private float foodNowTimer = 0f;
    //레벨과는 별개로 10초마다 10이 다운되는건 고정임
    private float foodMaxTimer = 10f;

    private int downLife;
    private bool isHealthPointPanalty;

    private MainData mainData;


    public Life(MainData mainData)
    {
        this.mainData = mainData;
    }

    /// <summary>
    /// Food가 0 이하이면 감소
    /// </summary>
    public bool CheckFoodPoint()
    {
        if (mainData.GetFoodPoint() <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Health가 0 이하면 감소
    /// </summary>
    /// <returns></returns>
    public bool CheckHealthPoint()
    {
        if (mainData.GetHealthPoint() <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MinusLifePoint(int minusLifePoint)
    {
        mainData.SetLifePoint(mainData.GetLifePoint() - minusLifePoint <= 0 ?
            0 : mainData.GetLifePoint() - minusLifePoint);
        //버그 방지를 위하여 health + 1해줌
        DataManager.Instance.uiManager.ChangeHealthBarText(mainData.GetHealthPoint());
        DataManager.Instance.uiManager.ChangeLifeBarText(mainData.GetLifePoint());
    }



    /// <summary>
    /// Body가 50 미만이 되면 감소
    /// </summary>
    public bool CheckLifePoint()
    {
        if (mainData.GetBodyPoint() <= 50)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeDelay(int index)
    {
        maxTimer = mainData.lifePenaltyDelay[index].delay;
        downLife = mainData.lifePenaltyDelay[index].value;
    }

    public void ChangeLifePoint()
    {
        var data = mainData.GetBodyPoint();
        if (data <= 50)
        {
            if (data <= 50 && data >= 26)
            {
                ChangeDelay(0);
            }

            else if (data <= 25 && data >= 1)
            {
                ChangeDelay(1);
            }
            else
            {
                ChangeDelay(2);
            }
        }
        else
        {
            //건강 50초과
        }
    }

    public void DownLifePointForFood(float timer)
    {

        foodNowTimer += timer;
        if (foodNowTimer >= foodMaxTimer)
        {
            Debug.Log($"포만감 소진으로 인한 생명력 : {foodNowTimer}마다 {10}만큼 감소됨");
            foodNowTimer = 0;

            mainData.SetLifePoint(mainData.GetLifePoint() - 10 <= 0 ?
                0 : mainData.GetLifePoint() - 10
                );

            DataManager.Instance.uiManager.ChangeLifeBarText(mainData.GetLifePoint());
            DataManager.Instance.uiManager.ChangeFoodBarText(mainData.GetFoodPoint());

        }
    }


    public void DownLifePoint(float timer)
    {
        ChangeLifePoint();

        nowTimer += timer;
        if (nowTimer >= maxTimer)
        {
            Debug.Log($"생명력 : 레벨{mainData.level} {nowTimer}마다 {downLife}만큼 감소됨");
            nowTimer = 0;

            mainData.SetLifePoint(mainData.GetLifePoint() - downLife <= 0 ?
                0 : mainData.GetLifePoint() - downLife
                );
            DataManager.Instance.uiManager.ChangeLifeBarText(mainData.GetLifePoint());

        }
    }



    public override void Play()
    {
        if (CheckHealthPoint())
        {
            if (!isHealthPointPanalty)
            {
                MinusLifePoint(10);
                isHealthPointPanalty = true;
            }
        }
        else
        {
            isHealthPointPanalty = false;
        }

        if (CheckLifePoint())
        {
            DownLifePoint(Time.deltaTime);
        }

        if (CheckFoodPoint())
        {
            DownLifePointForFood(Time.deltaTime);
        }
        else
        {
            foodNowTimer = 0f;
        }


    }
}
