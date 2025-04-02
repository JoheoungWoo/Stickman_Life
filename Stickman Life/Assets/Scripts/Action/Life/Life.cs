using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Action
{
    private float nowTimer = 0f;
    private float maxTimer;

    private float foodNowTimer = 0f;
    //�������� ������ 10�ʸ��� 10�� �ٿ�Ǵ°� ������
    private float foodMaxTimer = 10f;

    private int downLife;
    private bool isHealthPointPanalty;

    private MainData mainData;


    public Life(MainData mainData)
    {
        this.mainData = mainData;
    }

    /// <summary>
    /// Food�� 0 �����̸� ����
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
    /// Health�� 0 ���ϸ� ����
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
        //���� ������ ���Ͽ� health + 1����
        DataManager.Instance.uiManager.ChangeHealthBarText(mainData.GetHealthPoint());
        DataManager.Instance.uiManager.ChangeLifeBarText(mainData.GetLifePoint());
    }



    /// <summary>
    /// Body�� 50 �̸��� �Ǹ� ����
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
            //�ǰ� 50�ʰ�
        }
    }

    public void DownLifePointForFood(float timer)
    {

        foodNowTimer += timer;
        if (foodNowTimer >= foodMaxTimer)
        {
            Debug.Log($"������ �������� ���� ����� : {foodNowTimer}���� {10}��ŭ ���ҵ�");
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
            Debug.Log($"����� : ����{mainData.level} {nowTimer}���� {downLife}��ŭ ���ҵ�");
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
