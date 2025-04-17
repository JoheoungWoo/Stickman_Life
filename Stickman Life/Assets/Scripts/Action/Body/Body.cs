using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Action
{
    private float nowTimer = 0f;
    private float maxTimer = 3f;

    private bool isBodyFullPanelty;     //������ 100 �г�Ƽ

    private int downBody;

    private float cancernowTimer = 0f;
    private float cancerMaxTimer = 5f;

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

    public bool CheckIsCancer()
    {
        if (mainData.isCancer)
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
            Debug.Log($"�ǰ� : ����{mainData.level} {nowTimer}���� {(downBody)}��ŭ ���ҵ�");
            nowTimer = 0;
            mainData.SetBodyPoint(mainData.GetBodyPoint() - (downBody) <= 0 ?
                0 : mainData.GetBodyPoint() - (downBody)
                );
            DataManager.Instance.uiManager.ChangeBodyBarText(mainData.GetBodyPoint());
        }
    }

    public void Cancer(float delay)
    {
        cancernowTimer += delay;
        if (cancernowTimer >= cancerMaxTimer)
        {
            cancernowTimer = 0;
            MinusBodyPoint(3);
            Debug.Log($"�ǰ� ������ ���Ͽ� : ����{mainData.level} {cancerMaxTimer}���� {3}��ŭ ���ҵ�");
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

        if (CheckIsCancer())
        {
            Cancer(Time.deltaTime);
        } else
        {

        }

        DownBodyPoint(Time.deltaTime);
    }
}
