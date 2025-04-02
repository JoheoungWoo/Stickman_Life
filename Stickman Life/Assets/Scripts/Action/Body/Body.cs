using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Action
{
    private float nowTimer = 0f;
    private float maxTimer = 3f;

    private bool isBodyFullPanelty;     //포만감 100 패널티

    private int downBody;

    private MainData mainData;

    public Body(MainData mainData)
    {
        this.mainData = mainData;
        CheckBodyPoint();
    }

    public bool CheckFullFoodPoint()
    {
        if (mainData.GetFoodPoint() >= 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    

    public void MinusBodyPoint(int minusLifePoint)
    {
        mainData.SetBodyPoint(mainData.GetBodyPoint() - minusLifePoint <= 0 ?
            0 : mainData.GetBodyPoint() - minusLifePoint);

        DataManager.Instance.uiManager.ChangeBodyBarText(mainData.GetBodyPoint());
    }

    public void CheckBodyPoint()
    {
        if (mainData.GetMentalPoint() > 0)
        {
            ChangeDelay();
        }
        else
        {
            ChangePenaltyDelay();
        }
    }

    public void ChangeDelay()
    {
        maxTimer = mainData.bodyDelay[mainData.level - 1].delay;
        downBody = mainData.bodyDelay[mainData.level - 1].value;
    }


    public void ChangePenaltyDelay()
    {
        maxTimer = mainData.bodyPenaltyDelay[mainData.level - 1].delay;
        downBody = mainData.bodyPenaltyDelay[mainData.level - 1].value;
    }

    public void DownBodyPoint(float timer)
    {
        CheckBodyPoint();


        nowTimer += timer;
        if (nowTimer >= maxTimer)
        {
            var minusPanaltyBody = 0;
            if (mainData.isCancer)
            {
                minusPanaltyBody = 5;
            }
            Debug.Log($"건강 : 레벨{mainData.level} {nowTimer}마다 {(downBody + minusPanaltyBody)}만큼 감소됨");
            nowTimer = 0;
            mainData.SetBodyPoint(mainData.GetBodyPoint() - (downBody + minusPanaltyBody) <= 0 ?
                0 : mainData.GetBodyPoint() - (downBody + minusPanaltyBody)
                );
            DataManager.Instance.uiManager.ChangeBodyBarText(mainData.GetBodyPoint());
        }
    }

    public override void Play()
    {
        if (CheckFullFoodPoint())
        {
            if(!isBodyFullPanelty)
            {
                MinusBodyPoint(20);
                isBodyFullPanelty = true;
            }
        } else
        {
            isBodyFullPanelty = false;
        }

        DownBodyPoint(Time.deltaTime);
    }
}
