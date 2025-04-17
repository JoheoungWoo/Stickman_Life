using System.Collections;
using System.Collections.Generic;


//UI에 따른 리스트
//한글로하려다가 영어로해야 관리편해서 반강제로 아오ㅡㅡ
public enum DiseaseName { None = -1, FoodPoisoning, Hallucination, Cold, Cancer }
public enum StatusName { None = -1, Life, Health, Body, Mental, Food }
public enum ButtonName { None = -1, Work, Hobby, Eat, Therapy, Skill }
public enum SkillName {None = -1, IncreaseLifeBtn, IncreaseHealthBtn, IncreaseHealthRecoveryBtn, IncreaseGoldProsperityBtn}

//아이템 리스트
public enum medicineItemName { None = -1,영양수액, 향정신성약, 감기약, 항암제, 건강보조제 }
public enum fooditemName { None = -1, 사탕, 감자칩, 컵라면, 김밥, 고기}

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
    #region bool 신호값 정의
    public bool isFoodPoisoning;    //식중독을 걸렸는가
    public bool isHallucination;    //환각을 걸렸는가
    public bool isCold;             //감기를 걸렸는가
    public bool isCancer;           //암을 걸렸는가
    public bool isLevelUp;          //레벨업을 했는가
    public bool isMaxLevel;         //만렙인가

    public ReduceByLevel<int>[] diseasePercentArr = { //건강에 의하여 감소
        new ReduceByLevel<int>(4),
        new ReduceByLevel<int>(5),
        new ReduceByLevel<int>(6),
        new ReduceByLevel<int>(7),
        new ReduceByLevel<int>(8),
    };

    public bool skillBool;          //스킬 버튼을 눌렀는지
    public bool therapyBool;        //질병 버튼을 눌렀는지
    public bool hobbyBool;          //취미 버튼을 눌렀는지
    public bool eatBool;            //식사 버튼을 눌렀는지
    public bool workBool;           //운동 버튼을 눌렀는지
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

    public int nowGold = 100;           //골드 보유량
    public int maxGold = 9999;           //최대 골드 보유량

    public int level = 1;               //현재 레벨
    public int maxLevel = 5;            //최대 레벨

    public int nowExp = 0;              //현재 경험치
    public int maxExp = 100;            //최대 경험치

    private int lifePoint;              //생명력
    public int maxLifePoint = 100;     //최대 생명력
    public ReduceByLevel<float, int>[] lifePenaltyDelay = { //건강에 의하여 감소
        new ReduceByLevel<float, int>(5f, 3),
        new ReduceByLevel<float, int>(5f, 6),
        new ReduceByLevel<float, int>(5f, 9)
    };

    private int healthPoint;            //체력
    public int maxHealthPoint = 100;   //최대 체력

    private int bodyPoint;              //건강
    public int maxBodyPoint = 100;     //최대 건강
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

    private int mentalPoint;            //정신력
    public int maxMentalPoint = 100;   //최대 정신력
    public ReduceByLevel<float, int>[] mentalDelay = {
        new ReduceByLevel<float, int>(5f, 1),
        new ReduceByLevel<float, int>(4.5f, 1),
        new ReduceByLevel<float, int>(4f, 1),
        new ReduceByLevel<float, int>(3.5f, 1),
        new ReduceByLevel<float, int>(3f, 1)
    };


    private int foodPoint;                      //포만감
    public int maxFoodPoint = 100;              //최대 포만감
    public int originMaxFoodPoint = 75;         //레벨업 하면 최대 값
    public int panaltyMaxFoodPoint = 50;        //식중독이면 최대 값

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

    //업적 체크를 위함
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


    #region 상점 데이터 식사
    public List<EatData> itemList = new List<EatData>();
    public List<DiseaseData> diseaseItemList = new List<DiseaseData>();

    /*
    public EatData candyItem = new EatData {myItemName = EatData.itemName.사탕, foodPointUp = 5 , itemPrice = 10};
    public EatData potatoChipsItem = new EatData {myItemName = EatData.itemName.감자칩, foodPointUp = 5 , itemPrice = 10};
    public EatData cupRamenItem = new EatData {myItemName = EatData.itemName.컵라면, foodPointUp = 5 , itemPrice = 10};
    public EatData kimbapItem = new EatData {myItemName = EatData.itemName.김밥, foodPointUp = 5 , itemPrice = 10};
    public EatData meatItem = new EatData {myItemName = EatData.itemName.고기, foodPointUp = 5 , itemPrice = 10};
    */
    #endregion


    #region 기본적인 능력치
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
    #region 부가적인 능력치 변동
    public void AddExp(int exp)
    {
        nowExp += exp;
    }

    /// <summary>
    /// -1은 골드부족 , -2는 레벨이 맥스인 경우 , 1은 성공
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
        //0 1 2인데 maxLevel은 3이므로 -1 해줌
        if (lifeUpLevel < lifeUpMaxLevel)
        {
            //조건 체크
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
        //레벨 체크
        if (healthUpLevel < healthUpMaxLevel)
        {
            //골드 체크
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
            //골드 체크
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
            //골드 체크
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
        lifePenaltyDelay = new ReduceByLevel<float, int>[]{ //건강에 의하여 감소
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