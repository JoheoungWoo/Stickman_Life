using System.Collections;
using System.Collections.Generic;


//UI�� ���� ����Ʈ
//�ѱ۷��Ϸ��ٰ� ������ؾ� �������ؼ� �ݰ����� �ƿ��Ѥ�
public enum DiseaseName { None = -1, FoodPoisoning, Hallucination, Cold, Cancer }
public enum StatusName { None = -1, Life, Health, Body, Mental, Food }
public enum ButtonName { None = -1, Work, Hobby, Eat, Therapy, Skill }
public enum SkillName {None = -1, IncreaseLifeBtn, IncreaseHealthBtn, IncreaseHealthRecoveryBtn, IncreaseGoldProsperityBtn}

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

    public int retryCount = 0;
    public string playerName = "test";
    public float timeSpan;

    public int lifeUpLevel = 0;
    public int lifeUpMaxLevel = 3;
    public int[] lifeUpArr = {5,10,15};
    public int[] lifeUpGoldArr = {50,100,200};

    public int healthUpLevel = 0;
    public int healthUpMaxLevel = 3;
    public int[] healthUpArr = { 10,10,10};
    public int[] healthUpGoldArr = {70,85,100};

    public int healthRepairLevel = 0;
    public int healthRepairMaxLevel = 3;
    public int[] healthRepairArr = { 4, 3, 2 };
    public int[] healthRepairUpGoldArr = {50,100,200};

    public int goldUpLevel = 0;
    public int goldUpMaxLevel = 3;
    public int[] goldUpArr = { 4, 3, 2 };
    public int[] goldUpGoldArr = {100,200,300};

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
        new ReduceByLevel<float, int>(5f, 1),
        new ReduceByLevel<float, int>(4.5f, 1),
        new ReduceByLevel<float, int>(4f, 1),
        new ReduceByLevel<float, int>(3.5f, 1),
        new ReduceByLevel<float, int>(3f, 1)
    };


    private int foodPoint;                      //������
    public int maxFoodPoint = 100;              //�ִ� ������
    public int originMaxFoodPoint = 75;         //������ �ϸ� �ִ� ��
    public int panaltyMaxFoodPoint = 50;        //���ߵ��̸� �ִ� ��

    public ReduceByLevel<float, int>[] foodDelay = { 
        new ReduceByLevel<float, int>(6f, 1),
        new ReduceByLevel<float, int>(5.5f, 1),
        new ReduceByLevel<float, int>(5f, 1),
        new ReduceByLevel<float, int>(4.5f, 1),
        new ReduceByLevel<float, int>(4f, 1)
    };

    public ReduceByLevel<float, int>[] foodPenaltyDelay = {
        new ReduceByLevel<float, int>(4f, 1),
        new ReduceByLevel<float, int>(3.5f, 1),
        new ReduceByLevel<float, int>(3f, 1),
        new ReduceByLevel<float, int>(2.5f, 1),
        new ReduceByLevel<float, int>(2f, 1)
    };

    //���� üũ�� ����
    public int dietarysupplementCount = 0;

    public int foodPoisoningCount = 0;
    public int hallucinationCount = 0;
    public int coldCount = 0;
    public int cancerCount = 0;

    public int candyCount = 0;
    public int chipCount = 0;
    public int ramyunCount = 0;
    public int kimbabCount = 0;
    public int meatCount = 0;


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
    /// 

    public void UpdateLifeSkill()
    {
        
    }

    public void UpdateHealthSkill()
    {

    }

    public void UpdateHealthRepairSkill()
    {

    }

    public void UpdateGoldSkill()
    {

    }

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

    public MainData()
    {

    }

    public void Init()
    {
        lifeUpMaxLevel = 3;
        lifeUpArr = new int[] { 5, 10, 15 };
        lifeUpGoldArr = new int[] { 50, 100, 200 };
        healthUpMaxLevel = 3;
        healthUpArr = new int[] { 10, 10, 10 };
        healthUpGoldArr = new int[] { 70, 85, 100 };
        healthRepairMaxLevel = 3;
        healthRepairArr = new int[] { 4, 3, 2 };
        healthRepairUpGoldArr = new int[] { 50, 100, 200 };
        goldUpMaxLevel = 3;
        healthRepairArr = new int[] { 4, 3, 2 };
        healthRepairUpGoldArr = new int[] { 50, 100, 200 };
        goldUpMaxLevel = 3;
        goldUpArr = new int[] { 4, 3, 2 };
        goldUpGoldArr = new int[] { 100, 200, 300 };
        lifePenaltyDelay = new ReduceByLevel<float, int>[]{ //�ǰ��� ���Ͽ� ����
            new ReduceByLevel<float, int>(5f, 3),
        new ReduceByLevel<float, int>(5f, 6),
        new ReduceByLevel<float, int>(5f, 9)
        };
        bodyDelay = new ReduceByLevel<float, int>[]{
            new ReduceByLevel<float, int>(8f, 2),
        new ReduceByLevel<float, int>(7f, 2),
        new ReduceByLevel<float, int>(6f, 2),
        new ReduceByLevel<float, int>(5f, 2),
        new ReduceByLevel<float, int>(4f, 2)
            };

        bodyPenaltyDelay = new ReduceByLevel<float, int>[]{
            new ReduceByLevel<float, int>(8f, 4),
        new ReduceByLevel<float, int>(7f, 4),
        new ReduceByLevel<float, int>(6f, 4),
        new ReduceByLevel<float, int>(5f, 4),
        new ReduceByLevel<float, int>(4f, 4)
            };

        mentalDelay = new ReduceByLevel<float, int>[]{
            new ReduceByLevel<float, int>(5f, 1),
        new ReduceByLevel<float, int>(4.5f, 1),
        new ReduceByLevel<float, int>(4f, 1),
        new ReduceByLevel<float, int>(3.5f, 1),
        new ReduceByLevel<float, int>(3f, 1)
            };
        foodDelay = new ReduceByLevel<float, int>[]{
            new ReduceByLevel<float, int>(6f, 1),
        new ReduceByLevel<float, int>(5.5f, 1),
        new ReduceByLevel<float, int>(5f, 1),
        new ReduceByLevel<float, int>(4.5f, 1),
        new ReduceByLevel<float, int>(4f, 1)
            };
        foodPenaltyDelay = new ReduceByLevel<float, int>[]{
            new ReduceByLevel<float, int>(4f, 1),
        new ReduceByLevel<float, int>(3.5f, 1),
        new ReduceByLevel<float, int>(3f, 1),
        new ReduceByLevel<float, int>(2.5f, 1),
        new ReduceByLevel<float, int>(2f, 1)
            };
    }

    public MainData(int retryCount, string playerName, int lifePoint, int maxLifePoint, int healthPoint, int maxHealthPoint, int bodyPoint, int maxBodyPoint, int mentalPoint,
    int maxMentalPoint , int foodPoint, int maxFoodPoint, int gold, float timeSpan, int increaseLifePoint, int increaseHealthPoint, int increaseHealthRecoveryPoint,
       int increaseGoldProsperityPoint, bool isFoodPoisoning, bool isHallucination, bool isCold,bool isCancer, int exp,int maxExp, int level,
       int dietarysupplementCount, int foodPoisoningCount, int hallucinationCount, int coldCount, int cancerCount,
       int candyCount, int chipCount, int ramyunCount, int kimbabCount, int meatCount)
    {
        this.retryCount = retryCount;
        this.playerName = playerName;
        SetLifePoint(lifePoint);
        this.maxLifePoint = maxLifePoint;
        SetHealthPoint(healthPoint);
        this.maxHealthPoint = maxHealthPoint;
        SetBodyPoint(bodyPoint);
        this.maxBodyPoint = maxBodyPoint;
        SetMentalPoint(mentalPoint);
        this.maxMentalPoint = maxMentalPoint;
        SetFoodPoint(foodPoint);
        this.maxFoodPoint = maxFoodPoint;
        this.nowGold = gold;
        this.timeSpan = timeSpan;
        lifeUpLevel = increaseLifePoint;
        healthUpLevel = increaseHealthPoint;
        healthRepairLevel = increaseHealthRecoveryPoint;
        goldUpLevel = increaseGoldProsperityPoint;
        this.isFoodPoisoning = isFoodPoisoning;
        this.isHallucination = isHallucination;
        this.isCold = isCold;
        this.isCancer = isCancer;
        this.nowExp = exp;
        this.maxExp = maxExp;
        this.level = level;
        this.dietarysupplementCount = dietarysupplementCount;
        this.foodPoisoningCount = foodPoisoningCount;
        this.hallucinationCount = hallucinationCount;
        this.coldCount = coldCount;
        this.cancerCount = cancerCount;
        this.candyCount = candyCount;
        this.chipCount = chipCount;
        this.ramyunCount = ramyunCount;
        this.kimbabCount = kimbabCount;
        this.meatCount = meatCount;
    }

    public void InitData(MainData mainData)
    {
        this.retryCount = mainData.retryCount;
        this.playerName = mainData.playerName;
        SetLifePoint(mainData.lifePoint);
        this.maxLifePoint = mainData.maxLifePoint;
        SetHealthPoint(mainData.healthPoint);
        this.maxHealthPoint = mainData.maxHealthPoint;
        SetBodyPoint(mainData.bodyPoint);
        this.maxBodyPoint = mainData.maxBodyPoint;
        SetMentalPoint(mainData.mentalPoint);
        this.maxMentalPoint = mainData.maxMentalPoint;
        SetFoodPoint(mainData.foodPoint);
        this.maxFoodPoint = mainData.maxFoodPoint;
        this.nowGold = mainData.nowGold;
        this.timeSpan = mainData.timeSpan;
        this.lifeUpLevel = mainData.lifeUpLevel;
        this.healthUpLevel = mainData.healthUpLevel;
        this.healthRepairLevel = mainData.healthRepairLevel;
        this.goldUpLevel = mainData.goldUpLevel;
        this.isFoodPoisoning = mainData.isFoodPoisoning;
        this.isHallucination = mainData.isHallucination;
        this.isCold = mainData.isCold;
        this.isCancer = mainData.isCancer;
        this.nowExp = mainData.nowExp;
        this.maxExp = mainData.maxExp;
        this.level = mainData.level;
        this.dietarysupplementCount = mainData.dietarysupplementCount;
        this.foodPoisoningCount = mainData.foodPoisoningCount;
        this.hallucinationCount = mainData.hallucinationCount;
        this.coldCount = mainData.coldCount;
        this.cancerCount = mainData.cancerCount;
        this.candyCount = mainData.candyCount;
        this.chipCount = mainData.chipCount;
        this.ramyunCount = mainData.ramyunCount;
        this.kimbabCount = mainData.kimbabCount;
        this.meatCount = mainData.meatCount;
    }
}