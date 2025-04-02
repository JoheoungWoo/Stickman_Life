using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disease : Action
{
    float nowTImer = 0f;
    float maxTimer = 60f;

    int diseasePercent;

    private MainData mainData;

    public Disease(MainData mainData)
    {
        this.mainData = mainData;
        ChangeDiease();
    }

    public void ChangeDiease()
    {
        diseasePercent = mainData.diseasePercentArr[mainData.level - 1].value;
    }
 
    #region ���� ȹ��
    public void GetAllDisease()
    {
        ChangeDiease();
        GetFoodPoisoning();
        GetHallucination();
        GetCold();
        GetCancer();
    }

    public void GetFoodPoisoning()
    {
        if(Random.Range(0, 100) <= diseasePercent && !mainData.isFoodPoisoning) 
        {
            mainData.isFoodPoisoning = true;
            Debug.Log($"{diseasePercent}Ȯ���� ���ߵ��� �ɷȽ��ϴ�.");
        }
    }

    public void GetHallucination()
    {
        if (Random.Range(0, 100) <= diseasePercent && !mainData.isHallucination)
        {
            mainData.isHallucination = true;
            Debug.Log($"{diseasePercent}Ȯ���� ȯ���� �ɷȽ��ϴ�.");
        }
    }

    public void GetCold()
    {
        if (Random.Range(0, 100) <= diseasePercent && !mainData.isCold)
        {
            mainData.isCold = true;
            Debug.Log($"{diseasePercent}Ȯ���� ���⿡ �ɷȽ��ϴ�.");
        }
    }

    public void GetCancer()
    {
        if (Random.Range(0, 100) <= diseasePercent && !mainData.isCancer)
        {
            mainData.isCancer = true;
            Debug.Log($"{diseasePercent}Ȯ���� �Ͽ� �ɷȽ��ϴ�.");
        }
    }
    #endregion

    #region ���� ġ�� �� ������
    public int TreatFoodPoisoning(int minusGold)
    {
        //���� üũ
        if(mainData.nowGold - minusGold < 0)
        {
            Debug.Log("��尡 �����մϴ�. ���ߵ� ġ�� ����");
            return -1;
        } 
        if(!mainData.isFoodPoisoning)
        {
            Debug.Log("���ߵ� ���°� �ƴմϴ�.");
            return -2;
        }

        //������ �ݿ�
        mainData.isFoodPoisoning = false;
        mainData.nowGold -= minusGold;
        Debug.Log("���� ġ�� �Ϸ�");

        //UI �ݿ�
        DataManager.Instance.uiManager.ChangeFoodPoisoning(mainData.isFoodPoisoning);
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);

        return 1;
    }

    public int TreatHallucination(int minusGold)
    {
        //���� üũ
        if (mainData.nowGold - minusGold < 0)
        {
            Debug.Log("��尡 �����մϴ�. ȯ�� ġ�� ����");
            return -1;
        }
        if (!mainData.isHallucination)
        {
            Debug.Log("ȯ�� ���°� �ƴմϴ�.");
            return -2;
        }

        //������ �ݿ�
        mainData.isHallucination = false;
        mainData.nowGold -= minusGold;
        Debug.Log("ȯ�� ġ�� �Ϸ�");

        //UI �ݿ�
        DataManager.Instance.uiManager.ChangeHallucination(mainData.isHallucination);
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);

        return 1;
    }

    public int TreatCold(int minusGold)
    {
        //���� üũ
        if (mainData.nowGold - minusGold < 0)
        {
            Debug.Log("��尡 �����մϴ�. ���� ġ�� ����");
            return -1;
        }
        if (!mainData.isCold)
        {
            Debug.Log("���� ���°� �ƴմϴ�.");
            return -2;
        }

        //������ �ݿ�
        mainData.isCold = false;
        mainData.nowGold -= minusGold;
        Debug.Log("���� ġ�� �Ϸ�");

        //UI �ݿ�
        DataManager.Instance.uiManager.ChangeCold(mainData.isCold);
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);

        return 1;
    }

    public int TreatCancer(int minusGold)
    {
        //���� üũ
        if (mainData.nowGold - minusGold < 0)
        {
            Debug.Log("��尡 �����մϴ�. �� ġ�� ����");
            return -1;
        }
        if (!mainData.isCancer)
        {
            Debug.Log("�� ���°� �ƴմϴ�.");
            return -2;
        }

        //������ �ݿ�
        mainData.isCancer = false;
        mainData.nowGold -= minusGold;
        Debug.Log("�� ġ�� �Ϸ�");

        //UI �ݿ�
        DataManager.Instance.uiManager.ChangeCancer(mainData.isCancer);
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);

        return 1;
    }

    public int TreatBodyPoint(int minusGold)
    {
        if (mainData.nowGold - minusGold < 0)
        {
            Debug.Log("��尡 �����մϴ�. �ǰ� ������ ����");
            return -1;
        }

        //������ �ݿ�
        if (mainData.GetBodyPoint() + 60 >= mainData.maxBodyPoint)
        {
            mainData.SetBodyPoint(mainData.maxBodyPoint);
        } else
        {
            mainData.SetBodyPoint(mainData.GetBodyPoint() + 60);
        }
        mainData.nowGold -= minusGold;
        Debug.Log("�ǰ������� �Ϸ�");

        //UI �ݿ�
        DataManager.Instance.uiManager.ChangeBodyBarText(mainData.GetBodyPoint());
        DataManager.Instance.uiManager.ChangeGoldText(mainData.nowGold);

        return 1;
    }
    #endregion

    #region ������ ���� �г�Ƽ
    public void CheckAllDisease()
    {
        CheckFoodPoisoning(mainData.isFoodPoisoning);
        CheckHallucination(mainData.isHallucination);
        CheckCold(mainData.isCold);
        CheckCancer(mainData.isCancer);
    }

    public void CheckFoodPoisoning(bool isFoodPoisoning)
    {
        if(isFoodPoisoning)
        {
            DataManager.Instance.uiManager.ChangeFoodPoisoning(isFoodPoisoning);
            Debug.Log("���� ���ߵ� �����Դϴ�.");
        }
    }

    public void CheckHallucination(bool ishallucination)
    {
        if(ishallucination)
        {
            DataManager.Instance.uiManager.ChangeHallucination(ishallucination);
            Debug.Log("���� ȯ�� �����Դϴ�.");
        }
    }

    public void CheckCold(bool isCold)
    {
        if(isCold)
        {
            DataManager.Instance.uiManager.ChangeCold(isCold);
            Debug.Log("���� ���� �����Դϴ�.");
        }
    }

    public void CheckCancer(bool isCancer)
    {
        if(isCancer)
        {
            DataManager.Instance.uiManager.ChangeCancer(isCancer);
            Debug.Log("���� �� �����Դϴ�.");
        }
    }
    #endregion

    public override void Play()
    {
        nowTImer += Time.deltaTime;
        if(nowTImer >= maxTimer)
        {
            nowTImer = 0;
            GetAllDisease();
        }
        CheckAllDisease();
    }
}
