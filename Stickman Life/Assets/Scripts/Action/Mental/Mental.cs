using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mental : Action
{
    private float nowTimer;
    private float maxTimer;

    private int downMantal;      //2씩 증가

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
            Debug.Log("환각임");
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

    //통일을 위해서 그냥 -1값으로 하기로함
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
                Debug.Log($"멘탈 : 레벨{mainData.level} {nowTimer}마다 {downMantal}만큼 감소됨");
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
                Debug.Log($"멘탈 : 레벨{mainData.level} {nowTimer}마다 {downMantal}만큼 감소됨");
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
