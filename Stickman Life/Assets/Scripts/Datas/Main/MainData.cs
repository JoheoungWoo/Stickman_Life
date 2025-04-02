using System.Collections;
using System.Collections.Generic;


//UI�� ���� ����Ʈ
//�ѱ۷��Ϸ��ٰ� ������ؾ� �������ؼ� �ݰ����� �ƿ��Ѥ�
public enum DiseaseName { None = -1, FoodPoisoning, Hallucination, Cold, Cancer }
public enum StatusName { None = -1, Life, Health, Body, Mental, Food }
public enum ButtonName { None = -1, Work, Hobby, Eat, Therapy, Skill }
public enum SkillName {None = -1, IncreaseLifeBtn, IncreaseHealthBtn, IncreaseBodyRecoveryBtn, IncreaseGoldProsperityBtn}

//������ ����Ʈ
public enum medicineItemName { None = -1,�������, �����ż���, �����, �׾���, �ǰ������� }
public enum fooditemName { None = -1, ����, ����Ĩ, �Ŷ��, ���, ���}

public enum ItemType { Medicine, Food, Skill,Unknown}


[System.Serializable]
public struct DiseaseData
{
    public UnityEngine.Sprite itemImage;
    public medicineItemName myItemName;
    public int itemPrice;
    public string itemExplanation;

    public DiseaseData(DiseaseData diseaseData)
    {
        itemImage = diseaseData.itemImage;
        myItemName = diseaseData.myItemName;
        itemPrice = diseaseData.itemPrice;
        itemExplanation = diseaseData.itemExplanation;
    }
}

[System.Serializable]
public struct EatData
{
    public UnityEngine.Sprite itemImage;
    public fooditemName myItemName;
    public int foodPointUp;
    public int itemPrice;
    public string itemExplanation;

    public EatData(EatData eatData)
    {
        itemImage = eatData.itemImage;
        myItemName = eatData.myItemName;
        foodPointUp = eatData.foodPointUp;
        itemPrice = eatData.itemPrice;
        itemExplanation = eatData.itemExplanation;
    }
}

[System.Serializable]
public struct SkillData
{
    public UnityEngine.Sprite skillImage;
    public SkillName myItemName;
    public int itemPrice;
    public string itemExplanation;

    public SkillData(SkillData skillData)
    {
        skillImage = skillData.skillImage;
        myItemName = skillData.myItemName;
        itemPrice = skillData.itemPrice;
        itemExplanation = skillData.itemExplanation;
    }
}

[System.Serializable]
public class MainData
{
    #region bool ��ȣ�� ����
    public bool isFoodPoisoning;    //���ߵ��� �ɷȴ°�
    public bool isHallucination;    //ȯ���� �ɷȴ°�
    public bool isCold;             //���⸦ �ɷȴ°�
    public bool isCancer;           //���� �ɷȴ°�
    public bool isLevelUp;          //�������� �ߴ°�
    public bool isMaxLevel;         //�����ΰ�

    public ReduceByLevel<int>[] diseasePercentArr = { //�ǰ��� ���Ͽ� ����
        new ReduceByLevel<int>(4),
        new ReduceByLevel<int>(5),
        new ReduceByLevel<int>(6),
        new ReduceByLevel<int>(7),
        new ReduceByLevel<int>(8),
    };

    public bool skillBool;          //��ų ��ư�� ��������
    public bool therapyBool;        //���� ��ư�� ��������
    public bool hobbyBool;          //��� ��ư�� ��������
    public bool eatBool;            //�Ļ� ��ư�� ��������
    public bool workBool;           //� ��ư�� ��������
    #endregion

    public int lifeUpLevel = 0;
    public int lifeUpMaxLevel = 3;
    private int[] lifeUpArr = {5,10,15};
    private int[] lifeUpGoldArr = {50,100,200};

    public int healthUpLevel = 0;
    public int healthUpMaxLevel = 3;
    private int[] healthUpArr = { 10,10,10};
    private int[] healthUpGoldArr = {70,85,100};

    public int healthRepairLevel = 0;
    public int healthRepairMaxLevel = 3;
    public int[] healthRepairArr = { 4, 3, 2 };
    private int[] healthRepairUpGoldArr = {50,100,200};

    public int goldUpLevel = 0;
    public int goldUpMaxLevel = 3;
    public int[] goldUpArr = { 4, 3, 2 };
    private int[] goldUpGoldArr = {100,200,300};

    public int nowGold = 100;           //��� ������
    public int maxGold = 9999;           //�ִ� ��� ������

    public int level = 1;               //���� ����
    public int maxLevel = 5;            //�ִ� ����

    public int nowExp = 0;              //���� ����ġ
    public int maxExp = 100;            //�ִ� ����ġ

    private int lifePoint;              //�����
    public int maxLifePoint = 100;     //�ִ� �����
    public ReduceByLevel<float, int>[] lifePenaltyDelay = { //�ǰ��� ���Ͽ� ����
        new ReduceByLevel<float, int>(5f, 3),
        new ReduceByLevel<float, int>(5f, 6),
        new ReduceByLevel<float, int>(5f, 9)
    };

    private int healthPoint;            //ü��
    public int maxHealthPoint = 100;   //�ִ� ü��

    private int bodyPoint;              //�ǰ�
    public int maxBodyPoint = 100;     //�ִ� �ǰ�
    public ReduceByLevel<float, int>[] bodyDelay = {
        new ReduceByLevel<float, int>(8f, 2),
        new ReduceByLevel<float, int>(7f, 2),
        new ReduceByLevel<float, int>(6f, 2),
        new ReduceByLevel<float, int>(5f, 2),
        new ReduceByLevel<float, int>(4f, 2)
    };
    public ReduceByLevel<float, int>[] bodyPenaltyDelay = {
        new ReduceByLevel<float, int>(8f, 4),
        new ReduceByLevel<float, int>(7f, 4),
        new ReduceByLevel<float, int>(6f, 4),
        new ReduceByLevel<float, int>(5f, 4),
        new ReduceByLevel<float, int>(4f, 4)
    };

    private int mentalPoint;            //���ŷ�
    public int maxMentalPoint = 100;   //�ִ� ���ŷ�
    public ReduceByLevel<float, int>[] mentalDelay = {
        new ReduceByLevel<float, int>(3.5f, 1),
        new ReduceByLevel<float, int>(3f, 1),
        new ReduceByLevel<float, int>(2.5f, 1),
        new ReduceByLevel<float, int>(2f, 1),
        new ReduceByLevel<float, int>(1.5f, 1)
    };


    private int foodPoint;                      //������
    public int maxFoodPoint = 100;              //�ִ� ������
    public int originMaxFoodPoint = 75;         //������ �ϸ� �ִ� ��
    public int panaltyMaxFoodPoint = 50;        //���ߵ��̸� �ִ� ��

    public ReduceByLevel<float, int>[] foodDelay = { 
        new ReduceByLevel<float, int>(5f, 1),
        new ReduceByLevel<float, int>(4.5f, 1),
        new ReduceByLevel<float, int>(4f, 1),
        new ReduceByLevel<float, int>(3.5f, 1),
        new ReduceByLevel<float, int>(3f, 1)
    };

    public ReduceByLevel<float, int>[] foodPenaltyDelay = {
        new ReduceByLevel<float, int>(4f, 1),
        new ReduceByLevel<float, int>(3.5f, 1),
        new ReduceByLevel<float, int>(3f, 1),
        new ReduceByLevel<float, int>(2.5f, 1),
        new ReduceByLevel<float, int>(2f, 1)
    };

    #region ���� ������ �Ļ�
    public List<EatData> itemList = new List<EatData>();
    public List<DiseaseData> diseaseItemList = new List<DiseaseData>();

    /*
    public EatData candyItem = new EatData {myItemName = EatData.itemName.����, foodPointUp = 5 , itemPrice = 10};
    public EatData potatoChipsItem = new EatData {myItemName = EatData.itemName.����Ĩ, foodPointUp = 5 , itemPrice = 10};
    public EatData cupRamenItem = new EatData {myItemName = EatData.itemName.�Ŷ��, foodPointUp = 5 , itemPrice = 10};
    public EatData kimbapItem = new EatData {myItemName = EatData.itemName.���, foodPointUp = 5 , itemPrice = 10};
    public EatData meatItem = new EatData {myItemName = EatData.itemName.���, foodPointUp = 5 , itemPrice = 10};
    */
    #endregion


    #region �⺻���� �ɷ�ġ
    public void SetLifePoint(int lifePoint)
    {
        this.lifePoint = lifePoint;
    }

    public int GetLifePoint()
    {
        return lifePoint;
    }

    public void SetHealthPoint(int healthPoint)
    {
        this.healthPoint = healthPoint;
    }

    public int GetHealthPoint()
    {
        return healthPoint;
    }

    public void SetBodyPoint(int bodyPoint)
    {
        this.bodyPoint = bodyPoint;
    }

    public int GetBodyPoint()
    {
        return bodyPoint;
    }

    public void SetMentalPoint(int mentalPoint)
    {
        this.mentalPoint = mentalPoint;
    }

    public int GetMentalPoint()
    {
        return mentalPoint;
    }

    public void SetFoodPoint(int foodPoint)
    {
        this.foodPoint = foodPoint;
    }

    public int GetFoodPoint()
    {
        return foodPoint;
    }
    #endregion
    #region �ΰ����� �ɷ�ġ ����
    public void AddExp(int exp)
    {
        nowExp += exp;
    }

    /// <summary>
    /// -1�� ������ , -2�� ������ �ƽ��� ��� , 1�� ����
    /// </summary>
    public int UpLifeSkill()
    {
        //0 1 2�ε� maxLevel�� 3�̹Ƿ� -1 ����
        if (lifeUpLevel < lifeUpMaxLevel)
        {
            //���� üũ
            if(nowGold < lifeUpGoldArr[lifeUpLevel])
            {
                return -1;
            }

            nowGold -= lifeUpGoldArr[lifeUpLevel];

            maxLifePoint += lifeUpArr[lifeUpLevel];
            SetLifePoint(GetLifePoint() + lifeUpArr[lifeUpLevel] <= maxLifePoint ?
                GetLifePoint() + lifeUpArr[lifeUpLevel] : maxLifePoint
            );

            lifeUpLevel += 1;
            return 1;
        }
        else
        {
            return -2;
        }
    }

    public int UpHealthSkill()
    {
        //���� üũ
        if (healthUpLevel < healthUpMaxLevel)
        {
            //��� üũ
            if (nowGold < healthUpGoldArr[healthUpLevel])
            {
                return -1;
            }

            nowGold -= healthUpGoldArr[healthUpLevel];

            maxHealthPoint += healthUpArr[healthUpLevel];
            SetHealthPoint(GetHealthPoint() + healthUpArr[healthUpLevel] <= maxHealthPoint ?
                GetHealthPoint() + healthUpArr[healthUpLevel] : maxHealthPoint
            );
            healthUpLevel += 1;
            return 1;
        }
        else
        {
            return -2;
        }
    }

    public int UpHealthRepairSkill()
    {
        if (healthRepairLevel < healthRepairMaxLevel)
        {
            //��� üũ
            if (nowGold < healthRepairUpGoldArr[healthRepairLevel])
            {
                return -1;
            }

            nowGold -= healthRepairUpGoldArr[healthRepairLevel];
            healthRepairLevel += 1;
            return 1;
        }
        else
        {
            return -2;
        }
    }

    public int UpGoldSkill()
    {
        if (goldUpLevel < goldUpMaxLevel)
        {
            //��� üũ
            if (nowGold < goldUpGoldArr[goldUpLevel])
            {
                return -1;
            }

            nowGold -= goldUpGoldArr[goldUpLevel];
            goldUpLevel += 1;
            return 1;
        }
        else
        {
            return -2;
        }
    }
    #endregion
}
