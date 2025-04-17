using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Action
{
    float nowTimer = 0f;
    float maxTimer = 2f;


    float additionalCurrentHealthTimer = 0f;
    float additionalMaxHealthTimer;

    int upHealth = 1;      //2�� ����

    private MainData mainData;

    public Health(MainData mainData)
    {
        this.mainData = mainData;
    }

    public bool CheckCold()
    {
        if(mainData.isCold)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void UpHealthPoint(float timer)
    {
        nowTimer += timer;
        if (nowTimer >= maxTimer)
        {
            Debug.Log($"ü���� {nowTimer} ��ŭ�� ������");
            nowTimer = 0;

            mainData.SetHealthPoint(mainData.GetHealthPoint() + upHealth >= mainData.maxHealthPoint ?
                mainData.maxHealthPoint : mainData.GetHealthPoint() + upHealth
                );
            DataManager.Instance.uiManager.ChangeHealthBarText(mainData.GetHealthPoint());
        }
    }

    public void SetAdditionalMaxHealthTimer(int level)
    {
        additionalMaxHealthTimer = mainData.healthRepairArr[level];
    }

    public void UpPassiveHealth(float timer)
    {
        additionalCurrentHealthTimer += timer;
        if (additionalCurrentHealthTimer >= additionalMaxHealthTimer)
        {
            Debug.Log($"�߰�ü��ȸ���� {additionalCurrentHealthTimer} ��ŭ�� ������ ���� {upHealth} ȸ��");
            additionalCurrentHealthTimer = 0;

            mainData.SetHealthPoint(mainData.GetHealthPoint() + upHealth >= mainData.maxHealthPoint ?
                            mainData.maxHealthPoint : mainData.GetHealthPoint() + upHealth
                            );
            DataManager.Instance.uiManager.ChangeHealthBarText(mainData.GetHealthPoint());
        }
    }

    public override void Play()
    {
        if(CheckCold())
        {
            nowTimer = 0;
            return;
        }

        if (mainData.healthRepairLevel >= 1)
        {
            SetAdditionalMaxHealthTimer(mainData.healthRepairLevel - 1);
            UpPassiveHealth(Time.deltaTime);
        }
        UpHealthPoint(Time.deltaTime);
    }
}
