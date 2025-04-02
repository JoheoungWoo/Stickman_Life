using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : Action
{
    private MainData mainData;

    public Eat(MainData mainData)
    {
        this.mainData = mainData;
    }

    public void EatFood(int plusEat, int price)
    {
        if (DataManager.Instance.mainData.nowGold >= price)
        {
            DataManager.Instance.mainData.nowGold -= price;

            //100보다 크면 100으로 설정
            DataManager.Instance.mainData.SetFoodPoint(
                DataManager.Instance.mainData.GetFoodPoint() + plusEat >= 100 ? 
                100 : DataManager.Instance.mainData.GetFoodPoint() + plusEat);

            DataManager.Instance.mainData.AddExp(4);

            //UI 반영
            DataManager.Instance.uiManager.ChangeGoldText(DataManager.Instance.mainData.nowGold);
            DataManager.Instance.uiManager.ChangeFoodBarText(DataManager.Instance.mainData.GetFoodPoint());
            DataManager.Instance.uiManager.ChangeExpBarText();

            Debug.Log("이때 FoodPoint의 값입니다." + DataManager.Instance.mainData.GetFoodPoint());
        }
    }

    public override void Play()
    {
        EatFood(10,15);
    }
}
