using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hobby : Action
{
    private MainData mainData;

    public Hobby(MainData mainData)
    {
        this.mainData = mainData;
    }

    /// <summary>
    /// �ݹ� , ��Ż���� , ü�°��� , ���������� 
    /// </summary>
    public void PlayHobby(System.Action<int> callback, int plusMental,int minusHealth, int minusFood)
    {


        if (DataManager.Instance.mainData.GetHealthPoint() - minusHealth < 0)
        {
            callback(-1);
            return;
        }
        if (DataManager.Instance.mainData.GetFoodPoint() - minusFood < 0)
        {
            callback(-2);
            return;
        }

        DataManager.Instance.mainData.SetHealthPoint(DataManager.Instance.mainData.GetHealthPoint() - minusHealth <= 0 ?
                        0 : DataManager.Instance.mainData.GetHealthPoint() - minusHealth);
        DataManager.Instance.mainData.SetFoodPoint(DataManager.Instance.mainData.GetFoodPoint() - minusFood <= 0 ?
            0 : DataManager.Instance.mainData.GetFoodPoint() - minusFood);
        DataManager.Instance.mainData.SetMentalPoint(DataManager.Instance.mainData.GetMentalPoint() + plusMental >= 100 ?
            100 : DataManager.Instance.mainData.GetMentalPoint() + plusMental
            );
        DataManager.Instance.mainData.AddExp(3);

        //UI �ݿ�
        DataManager.Instance.uiManager.ChangeHealthBarText(DataManager.Instance.mainData.GetHealthPoint());
        DataManager.Instance.uiManager.ChangeFoodBarText(DataManager.Instance.mainData.GetFoodPoint());
        DataManager.Instance.uiManager.ChangeMentalBarTextt(DataManager.Instance.mainData.GetMentalPoint());
        DataManager.Instance.uiManager.ChangeExpBarText();

        callback(1);
        return;
    }

    public override void Play()
    {
        // �ݹ� �Լ��� �ζ������� �����Ͽ� ����
        PlayHobby((result) =>
        {
            // �ݹ� �Լ����� �ʿ��� �۾� ����
            if (result == 1)
            {

            }
            else if (result == -1)
            {
                DataManager.Instance.uiManager.PrintError("ü���� �����մϴ�.");
            }
            else if (result == -2)
            {
                DataManager.Instance.uiManager.PrintError("�������� �����մϴ�.");
            }
        }, 20 , 10 , 5);
    }
}
