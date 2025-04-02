using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStatus
{
    playerDeath,        // 0
    playerDisease,      //1
    playerLowHealth,    //2
    playerLowBody,      //3
    playerDefault,      //4
}

public class PlayerCondition
{
    private MainData mainData;

    public PlayerCondition(MainData mainData)
    {
        this.mainData = mainData;
    }
    public PlayerStatus PlayAllAction()
    {
        if(CheckDeath())
        {
            return PlayerStatus.playerDeath;
        } else if (CheckDisease())
        {
            return PlayerStatus.playerDisease;
        }
        else if (CheckLowBody())
        {
            return PlayerStatus.playerLowBody;
        }
        else if (CheckLowHealth())
        {
            return PlayerStatus.playerLowHealth;
        }
        else
        {
            return PlayerStatus.playerDefault;
        }
    }

    //사망
    public bool CheckDeath()
    {
        if(mainData.GetLifePoint() <= 0)
        {
            return true;
        } else
        {
            return false;
        }

    }

    //질병
    public bool CheckDisease()
    {
        if (mainData.isFoodPoisoning || mainData.isHallucination 
            || mainData.isCold || mainData.isCancer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //건강
    public bool CheckLowBody()
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

    //체력
    public bool CheckLowHealth()
    {
        if (mainData.GetHealthPoint() <= 50)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
