using System.Collections;
using System.Collections.Generic;


//UI에 따른 리스트
//한글로하려다가 영어로해야 관리편해서 반강제로 아오ㅡㅡ
public enum DiseaseName { None = -1, FoodPoisoning, Hallucination, Cold, Cancer }
public enum StatusName { None = -1, Life, Health, Body, Mental, Food }
public enum ButtonName { None = -1, Work, Hobby, Eat, Therapy, Skill }
public enum SkillName {None = -1, IncreaseLifeBtn, IncreaseHealthBtn, IncreaseBodyRecoveryBtn, IncreaseGoldProsperityBtn}

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
        new ReduceByLevel<float, int>(3.5f, 1),
        new ReduceByLevel<float, int>(3f, 1),
        new ReduceByLevel<float, int>(2.5f, 1),
        new ReduceByLevel<float, int>(2f, 1),
        new ReduceByLevel<float, int>(1.5f, 1)
    };


    private int foodPoint;                      //포만감
    public int maxFoodPoint = 100;              //최대 포만감
    public int originMaxFoodPoint = 75;         //레벨업 하면 최대 값
    public int panaltyMaxFoodPoint = 50;        //식중독이면 최대 값

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
}
