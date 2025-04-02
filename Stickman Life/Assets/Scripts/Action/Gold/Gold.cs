using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Action
{
    float nowGoldTimer = 0f;
    float maxGoldTimer = 2f;

    float additionalCurrentGoldTimer = 0f;
    float additionalMaxGoldTimer;

    int upGoldValue = 1;      //2¾¿ Áõ°¡

    private MainData mainData;

    public Gold(MainData mainData)
    {
        this.mainData = mainData;
    }
    public void UpGold(float timer)
    {
        nowGoldTimer += timer;
        if (nowGoldTimer >= maxGoldTimer)
        {
            Debug.Log($"°ñµå´Â {nowGoldTimer} ¸¸Å­ÀÇ µô·¹ÀÌ ¸¶´Ù {upGoldValue} È¹µæ");
            nowGoldTimer = 0;

            mainData.nowGold += upGoldValue;
            if (mainData.nowGold + upGoldValue >= mainData.maxGold)
            {
                mainData.nowGold = mainData.maxGold;
            }
            DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);
        }
    }

    public void SetAdditionalMaxGoldTimer(int level)
    {
        additionalMaxGoldTimer = mainData.goldUpArr[level];
    }

    public void UpPassiveGold(float timer)
    {
        additionalCurrentGoldTimer += timer;
        if (additionalCurrentGoldTimer >= additionalMaxGoldTimer)
        {
            Debug.Log($"Ãß°¡°ñµå´Â {additionalCurrentGoldTimer} ¸¸Å­ÀÇ µô·¹ÀÌ ¸¶´Ù {upGoldValue} È¹µæ");
            additionalCurrentGoldTimer = 0;

            mainData.nowGold += upGoldValue;
            if (mainData.nowGold + upGoldValue >= mainData.maxGold)
            {
                mainData.nowGold = mainData.maxGold;
            }
            DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);
        }
    }
    public override void Play()
    {
        UpGold(Time.deltaTime);
        if (mainData.goldUpLevel >= 1)
        {
            SetAdditionalMaxGoldTimer(mainData.goldUpLevel - 1);
            UpPassiveGold(Time.deltaTime);
        }
    }
}