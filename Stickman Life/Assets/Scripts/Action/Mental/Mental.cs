using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mental : Action
{
    private float nowTimer;
    private float maxTimer;

    private int downMantal;      //2�� ����

    private MainData mainData;

    public Mental(MainData mainData)
    {
        this.mainData = mainData;
        CheckMentalPoint();
    }

    public void CheckHallucination(bool isHallucination)
    {
        if(isHallucination)
        {
            Debug.Log("ȯ����");
        }
    }


    public void CheckMentalPoint()
    {
        ChangeDelay();
    }

    public void ChangeDelay()
    {
        maxTimer = mainData.mentalDelay[mainData.level - 1].delay;
        downMantal = mainData.mentalDelay[mainData.level - 1].value;
    }

    //������ ���ؼ� �׳� -1������ �ϱ����
    public void ChangeDelay(int level)
    {
        maxTimer = mainData.mentalDelay[level - 1].delay;
        downMantal = mainData.mentalDelay[level - 1].value;
    }

    public void DownMentalPoint(float timer)
    {
        CheckMentalPoint();

        nowTimer += timer;
        if(!mainData.isHallucination)
        {
            if (nowTimer >= maxTimer)
            {
                Debug.Log($"��Ż : ����{mainData.level} {nowTimer}���� {downMantal}��ŭ ���ҵ�");
                nowTimer = 0;

                mainData.SetMentalPoint(mainData.GetMentalPoint() - downMantal <= 0 ?
                0 : mainData.GetMentalPoint() - downMantal
                );
                DataManager.Instance.uiManager.ChangeMentalBarTextt(mainData.GetMentalPoint());
            }
        } else
        {
            if (nowTimer >= maxTimer / 2)
            {
                Debug.Log($"��Ż : ����{mainData.level} {nowTimer}���� {downMantal}��ŭ ���ҵ�");
                nowTimer = 0;

                mainData.SetMentalPoint(mainData.GetMentalPoint() - downMantal <= 0 ?
                0 : mainData.GetMentalPoint() - downMantal
                );
                DataManager.Instance.uiManager.ChangeMentalBarTextt(mainData.GetMentalPoint());
            }
        }
    }

    public override void Play()
    {
        DownMentalPoint(Time.deltaTime);
    }
}
