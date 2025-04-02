using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceByLevel<T>
{
    public T value;

    public ReduceByLevel(T value)
    {
        this.value = value;
    }
}

public class ReduceByLevel<K, T>
{
    public K delay;
    public T value;

    public ReduceByLevel(K delay, T value) {
        this.delay = delay;
        this.value = value;
    }
}


public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static DataManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public MainData mainData;
    public UIManager uiManager;
    public SoundManager soundManager;
    public GameManager gameManager;
    public FoodItem foodItem;
    public DiseaseItem diseaseItem;

    public List<EatData> newItemList = new List<EatData>();
    public List<DiseaseData> newDiseaseItemList = new List<DiseaseData>();
    public List<SkillData> newSkillData = new List<SkillData>();

    public ToolTip toolTip = new ToolTip();
    public GameObject toolTIpPrefab;

    public Transform toolTipParent;


    private void Start()
    {
        Init();
        /*
        foreach(var item in toolTip)
        {

            Debug.Log($"{ite}이며 {data}라는 정보입니다.");
        }*/

        var data = toolTip.FindToolTipData("운동").GetAllData();
        Debug.Log($"{data.type}라는 것을 선택했고, {data.data}");

        data = toolTip.FindToolTipData("환각").GetAllData();
        Debug.Log($"{data.type}라는 것을 선택했고, {data.data}");


        data = toolTip.FindToolTipData("취미").GetAllData();
        Debug.Log($"{data.type}라는 것을 선택했고, {data.data}");

    }


    private void Init()
    {
        mainData = new MainData();

        gameManager.InitGame(mainData);
        uiManager.Init(mainData);

        //값 초기화
        mainData.SetBodyPoint(mainData.maxBodyPoint);
        mainData.SetFoodPoint(mainData.originMaxFoodPoint);     //maxFoodPoint를 주면 건강이 20이 시작부터 감소가 되므로
        mainData.SetHealthPoint(mainData.maxHealthPoint);
        mainData.SetLifePoint(mainData.maxLifePoint);
        mainData.SetMentalPoint(mainData.maxMentalPoint);

        //UI 반영
        uiManager.ChangeLevelText(mainData.level);
        uiManager.ChangeBodyBarText(mainData.GetBodyPoint());
        uiManager.ChangeFoodBarText(mainData.GetFoodPoint());
        uiManager.ChangeHealthBarText(mainData.GetHealthPoint());
        uiManager.ChangeLifeBarText(mainData.GetLifePoint());
        uiManager.ChangeMentalBarTextt(mainData.GetMentalPoint());

        uiManager.ChangeGoldText(mainData.nowGold);
        uiManager.ChangeExpBarText();

        //사용되는 데이터 생성
        mainData.itemList.Add(new EatData(newItemList[0]));
        mainData.itemList.Add(new EatData(newItemList[1]));
        mainData.itemList.Add(new EatData(newItemList[2]));
        mainData.itemList.Add(new EatData(newItemList[3]));
        mainData.itemList.Add(new EatData(newItemList[4]));

        mainData.diseaseItemList.Add(new DiseaseData(newDiseaseItemList[0]));
        mainData.diseaseItemList.Add(new DiseaseData(newDiseaseItemList[1]));
        mainData.diseaseItemList.Add(new DiseaseData(newDiseaseItemList[2]));
        mainData.diseaseItemList.Add(new DiseaseData(newDiseaseItemList[3]));
        mainData.diseaseItemList.Add(new DiseaseData(newDiseaseItemList[4]));


        //null방지
        foodItem?.Init(mainData.itemList);
        diseaseItem?.Init(mainData.diseaseItemList);
    }

    public void OpenCheckItemUI(string itemName)
    {
        uiManager.OpenItemCheckWindow();
        uiManager.itemCheckUIObj.transform.GetChild(0).GetChild(1).
            GetComponent<UnityEngine.UI.Image>().sprite = GetSprite(itemName);
        uiManager.itemCheckUIObj.transform.GetChild(0).GetChild(2).
            GetComponent<TMPro.TMP_Text>().text = GetItemName(itemName);
        uiManager.itemCheckUIObj.transform.GetChild(0).GetChild(3).
    GetComponent<TMPro.TMP_Text>().text = GetItemPrice(itemName);
        uiManager.itemCheckUIObj.transform.GetChild(1).GetChild(0).
            GetComponent<TMPro.TMP_Text>().text = GetItemExplanation(itemName);
    }

    public void OpenCheckSkillUI(string itemName)
    {
        uiManager.OpenSkillCheckWindow();
        uiManager.skillCheckUIObj.transform.GetChild(0).GetChild(1).
            GetComponent<UnityEngine.UI.Image>().sprite = GetSprite(itemName);
        uiManager.skillCheckUIObj.transform.GetChild(0).GetChild(2).
            GetComponent<TMPro.TMP_Text>().text = GetItemName(itemName);
        uiManager.skillCheckUIObj.transform.GetChild(0).GetChild(3).
    GetComponent<TMPro.TMP_Text>().text = GetItemPrice(itemName);
        uiManager.skillCheckUIObj.transform.GetChild(1).GetChild(0).
            GetComponent<TMPro.TMP_Text>().text = GetItemExplanation(itemName);
    }

    #region 데이터가져오는 메소드
    public Sprite GetSprite(string itemName)
    {
        Sprite mySprite = null;
        switch (itemName)
        {
            //식사의 목록만 정의
            case nameof(fooditemName.사탕):
                mySprite = newItemList[(int)fooditemName.사탕].itemImage;
                break;
            case nameof(fooditemName.감자칩):
                mySprite = newItemList[(int)fooditemName.감자칩].itemImage;
                break;
            case nameof(fooditemName.컵라면):
                mySprite = newItemList[(int)fooditemName.컵라면].itemImage;
                break;
            case nameof(fooditemName.김밥):
                mySprite = newItemList[(int)fooditemName.김밥].itemImage;
                break;
            case nameof(fooditemName.고기):
                mySprite = newItemList[(int)fooditemName.고기].itemImage;
                break;

            //의료의 목록만 정의
            case nameof(medicineItemName.감기약):
                mySprite = newDiseaseItemList[(int)medicineItemName.감기약].itemImage;
                break;
            case nameof(medicineItemName.건강보조제):
                mySprite = newDiseaseItemList[(int)medicineItemName.건강보조제].itemImage;
                break;
            case nameof(medicineItemName.영양수액):
                mySprite = newDiseaseItemList[(int)medicineItemName.영양수액].itemImage;
                break;
            case nameof(medicineItemName.항암제):
                mySprite = newDiseaseItemList[(int)medicineItemName.항암제].itemImage;
                break;
            case nameof(medicineItemName.향정신성약):
                mySprite = newDiseaseItemList[(int)medicineItemName.향정신성약].itemImage;
                break;

            //스킬의 목록만 정의
            case nameof(SkillName.IncreaseLifeBtn):
                mySprite = newSkillData[(int)SkillName.IncreaseLifeBtn].skillImage;
                break;
            case nameof(SkillName.IncreaseHealthBtn):
                mySprite = newSkillData[(int)SkillName.IncreaseHealthBtn].skillImage;
                break;
            case nameof(SkillName.IncreaseBodyRecoveryBtn):
                mySprite = newSkillData[(int)SkillName.IncreaseBodyRecoveryBtn].skillImage;
                break;
            case nameof(SkillName.IncreaseGoldProsperityBtn):
                mySprite = newSkillData[(int)SkillName.IncreaseGoldProsperityBtn].skillImage;
                break;
        }

        return mySprite;
    }

    public string GetItemName(string itemName)
    {
        string myString = "";
        switch (itemName)
        {
            //식사의 목록만 정의
            case nameof(fooditemName.사탕):
                myString = newItemList[(int)fooditemName.사탕].myItemName.ToString();
                break;
            case nameof(fooditemName.감자칩):
                myString = newItemList[(int)fooditemName.감자칩].myItemName.ToString();
                break;
            case nameof(fooditemName.컵라면):
                myString = newItemList[(int)fooditemName.컵라면].myItemName.ToString();
                break;
            case nameof(fooditemName.김밥):
                myString = newItemList[(int)fooditemName.김밥].myItemName.ToString();
                break;
            case nameof(fooditemName.고기):
                myString = newItemList[(int)fooditemName.고기].myItemName.ToString();
                break;

            //의료의 목록만 정의
            case nameof(medicineItemName.감기약):
                myString = newDiseaseItemList[(int)medicineItemName.감기약].myItemName.ToString();
                break;
            case nameof(medicineItemName.건강보조제):
                myString = newDiseaseItemList[(int)medicineItemName.건강보조제].myItemName.ToString();
                break;
            case nameof(medicineItemName.영양수액):
                myString = newDiseaseItemList[(int)medicineItemName.영양수액].myItemName.ToString();
                break;
            case nameof(medicineItemName.항암제):
                myString = newDiseaseItemList[(int)medicineItemName.항암제].myItemName.ToString();
                break;
            case nameof(medicineItemName.향정신성약):
                myString = newDiseaseItemList[(int)medicineItemName.향정신성약].myItemName.ToString();
                break;

            //스킬의 목록만 정의
            case nameof(SkillName.IncreaseLifeBtn):
                //myString = newSkillData[(int)SkillName.IncreaseLifeBtn].myItemName.ToString();
                myString = $"생명력 증가 LV {mainData.lifeUpLevel + 1}";
                break;
            case nameof(SkillName.IncreaseHealthBtn):
                //myString = newSkillData[(int)SkillName.IncreaseHealthBtn].myItemName.ToString();
                myString = $"체력 증가 LV {mainData.healthUpLevel + 1}";
                break;
            case nameof(SkillName.IncreaseBodyRecoveryBtn):
                //myString = newSkillData[(int)SkillName.IncreaseBodyRecoveryBtn].myItemName.ToString();
                myString = $"건강 재생 증가 LV {mainData.healthRepairLevel + 1}";
                break;
            case nameof(SkillName.IncreaseGoldProsperityBtn):
                //myString = newSkillData[(int)SkillName.IncreaseGoldProsperityBtn].myItemName.ToString();
                myString = $"골드 획득량 증가 LV {mainData.goldUpLevel + 1}";
                break;
        }
        return myString;
    }
    public string GetItemPrice(string itemName)
    {
        int myPrice = 0;
        switch (itemName)
        {
            //식사의 목록만 정의
            case nameof(fooditemName.사탕):
                myPrice = newItemList[(int)fooditemName.사탕].itemPrice;
                break;
            case nameof(fooditemName.감자칩):
                myPrice = newItemList[(int)fooditemName.감자칩].itemPrice;
                break;
            case nameof(fooditemName.컵라면):
                myPrice = newItemList[(int)fooditemName.컵라면].itemPrice;
                break;
            case nameof(fooditemName.김밥):
                myPrice = newItemList[(int)fooditemName.김밥].itemPrice;
                break;
            case nameof(fooditemName.고기):
                myPrice = newItemList[(int)fooditemName.고기].itemPrice;
                break;

            //의료의 목록만 정의
            case nameof(medicineItemName.감기약):
                myPrice = newDiseaseItemList[(int)medicineItemName.감기약].itemPrice;
                break;
            case nameof(medicineItemName.건강보조제):
                myPrice = newDiseaseItemList[(int)medicineItemName.건강보조제].itemPrice;
                break;
            case nameof(medicineItemName.영양수액):
                myPrice = newDiseaseItemList[(int)medicineItemName.영양수액].itemPrice;
                break;
            case nameof(medicineItemName.항암제):
                myPrice = newDiseaseItemList[(int)medicineItemName.항암제].itemPrice;
                break;
            case nameof(medicineItemName.향정신성약):
                myPrice = newDiseaseItemList[(int)medicineItemName.향정신성약].itemPrice;
                break;

            //스킬의 목록만 정의
            case nameof(SkillName.IncreaseLifeBtn):
                myPrice = newSkillData[(int)SkillName.IncreaseLifeBtn].itemPrice;
                break;
            case nameof(SkillName.IncreaseHealthBtn):
                myPrice = newSkillData[(int)SkillName.IncreaseHealthBtn].itemPrice;
                break;
            case nameof(SkillName.IncreaseBodyRecoveryBtn):
                myPrice = newSkillData[(int)SkillName.IncreaseBodyRecoveryBtn].itemPrice;
                break;
            case nameof(SkillName.IncreaseGoldProsperityBtn):
                myPrice = newSkillData[(int)SkillName.IncreaseGoldProsperityBtn].itemPrice;
                break;
        }
        return myPrice.ToString();
    }

    public string GetItemExplanation(string itemName)
    {
        string myString = "";
        switch (itemName)
        {
            //식사의 목록만 정의
            case nameof(fooditemName.사탕):
                myString = newItemList[(int)fooditemName.사탕].itemExplanation;
                break;
            case nameof(fooditemName.감자칩):
                myString = newItemList[(int)fooditemName.감자칩].itemExplanation;
                break;
            case nameof(fooditemName.컵라면):
                myString = newItemList[(int)fooditemName.컵라면].itemExplanation;
                break;
            case nameof(fooditemName.김밥):
                myString = newItemList[(int)fooditemName.김밥].itemExplanation;
                break;
            case nameof(fooditemName.고기):
                myString = newItemList[(int)fooditemName.고기].itemExplanation;
                break;

            //의료의 목록만 정의
            case nameof(medicineItemName.감기약):
                myString = newDiseaseItemList[(int)medicineItemName.감기약].itemExplanation;
                break;
            case nameof(medicineItemName.건강보조제):
                myString = newDiseaseItemList[(int)medicineItemName.건강보조제].itemExplanation;
                break;
            case nameof(medicineItemName.영양수액):
                myString = newDiseaseItemList[(int)medicineItemName.영양수액].itemExplanation;
                break;
            case nameof(medicineItemName.항암제):
                myString = newDiseaseItemList[(int)medicineItemName.항암제].itemExplanation;
                break;
            case nameof(medicineItemName.향정신성약):
                myString = newDiseaseItemList[(int)medicineItemName.향정신성약].itemExplanation;
                break;

            //스킬의 목록만 정의
            case nameof(SkillName.IncreaseLifeBtn):
                myString = newSkillData[(int)SkillName.IncreaseLifeBtn].itemExplanation;
                break;
            case nameof(SkillName.IncreaseHealthBtn):
                myString = newSkillData[(int)SkillName.IncreaseHealthBtn].itemExplanation;
                break;
            case nameof(SkillName.IncreaseBodyRecoveryBtn):
                myString = newSkillData[(int)SkillName.IncreaseBodyRecoveryBtn].itemExplanation;
                break;
            case nameof(SkillName.IncreaseGoldProsperityBtn):
                myString = newSkillData[(int)SkillName.IncreaseGoldProsperityBtn].itemExplanation;
                break;
        }
        return myString;
    }
    #endregion

    #region 상점 관련
    public void PerformAction(string itemName)
    {
        Debug.Log(itemName + "값임3");
        switch (GetType(itemName))
        {
            case ItemType.Medicine:
                TakeMedicine(itemName);
                break;
            case ItemType.Food:
                EatFood(itemName);
                break;
            case ItemType.Skill:
                UpgradeSkill(itemName);
                break;
            case ItemType.Unknown:
            default:
                break;
        }
    }

    private void EatFood(string itemName)
    {
        Debug.Log($"EatFood가 실행됬으며 , 이것은 아이템 이름이다. {itemName}");
        switch(itemName)
        {
            case nameof(fooditemName.사탕):
                if (!gameManager.food.UpFoodStatus(5, 10))
                {
                    uiManager.PrintError("골드가 부족합니다.");
                } else
                {
                    mainData.AddExp(4);
                    uiManager.ChangeExpBarText();
                }
                break;
            case nameof(fooditemName.감자칩):
                if(!gameManager.food.UpFoodStatus(10, 15))
                {
                    uiManager.PrintError("골드가 부족합니다.");
                }
                else
                {
                    mainData.AddExp(4);
                    uiManager.ChangeExpBarText();
                }
                break;
            case nameof(fooditemName.컵라면):
                if(!gameManager.food.UpFoodStatus(20, 30))
                {
                    uiManager.PrintError("골드가 부족합니다.");
                }
                else
                {
                    mainData.AddExp(4);
                    uiManager.ChangeExpBarText();
                }
                break;
            case nameof(fooditemName.김밥):
                if(!gameManager.food.UpFoodStatus(25, 40))
                {
                    uiManager.PrintError("골드가 부족합니다.");
                }
                else
                {
                    mainData.AddExp(4);
                    uiManager.ChangeExpBarText();
                }
                break;
            case nameof(fooditemName.고기):
                if(!gameManager.food.UpFoodStatus(40, 55))
                {
                    uiManager.PrintError("골드가 부족합니다.");
                }
                else
                {
                    mainData.AddExp(4);
                    uiManager.ChangeExpBarText();
                }
                break;
        }
    }

    private void TakeMedicine(string itemName)
    {
        Debug.Log($"TakeMedicine가 실행됬으며 , 이것은 아이템 이름이다. {itemName}");
        switch (itemName)
        {
            case nameof(medicineItemName.영양수액):
                switch(gameManager.disease.TreatFoodPoisoning(75)) {
                    case -1:
                        uiManager.PrintError("골드가 부족합니다.");
                        break;
                    case -2:
                        uiManager.PrintError("식중독 상태가 아닙니다.");
                        break;
                    case 1:
                        mainData.AddExp(7);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
            case nameof(medicineItemName.향정신성약):
                switch (gameManager.disease.TreatHallucination(75))
                {
                    case -1:
                        uiManager.PrintError("골드가 부족합니다.");
                        break;
                    case -2:
                        uiManager.PrintError("환각 상태가 아닙니다.");
                        break;
                    case 1:
                        mainData.AddExp(7);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
            case nameof(medicineItemName.감기약):
                switch (gameManager.disease.TreatCold(75))
                {
                    case -1:
                        uiManager.PrintError("골드가 부족합니다.");
                        break;
                    case -2:
                        uiManager.PrintError("감기 상태가 아닙니다.");
                        break;
                    case 1:
                        mainData.AddExp(7);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
            case nameof(medicineItemName.항암제):
                switch (gameManager.disease.TreatCancer(100))
                {
                    case -1:
                        uiManager.PrintError("골드가 부족합니다.");
                        break;
                    case -2:
                        uiManager.PrintError("암 상태가 아닙니다.");
                        break;
                    case 1:
                        mainData.AddExp(7);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
            case nameof(medicineItemName.건강보조제):
                switch (gameManager.disease.TreatBodyPoint(60))
                {
                    case -1:
                        uiManager.PrintError("골드가 부족합니다.");
                        break;
                    case 1:
                        mainData.AddExp(7);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
        }
    }

    private void UpgradeSkill(string itemName)
    {
        switch (itemName)
        {
            case nameof(SkillName.IncreaseLifeBtn):
                uiManager.LifeUp();
                break;
            case nameof(SkillName.IncreaseHealthBtn):
                uiManager.HealthUp();
                break;
            case nameof(SkillName.IncreaseBodyRecoveryBtn):
                uiManager.HealthRecoveryUp();
                break;
            case nameof(SkillName.IncreaseGoldProsperityBtn):
                uiManager.IncreaseGoldUp();
                break;
        }
    }

    private ItemType GetType(string itemName)
    {
        if (System.Enum.TryParse(itemName, out medicineItemName medicineItem))
        {
            return ItemType.Medicine;
        }
        else if (System.Enum.TryParse(itemName, out fooditemName foodItem))
        {
            return ItemType.Food;
        }
        else if (System.Enum.TryParse(itemName, out SkillName skillItem))
        {
            return ItemType.Skill;
        }
        else
        {
            return ItemType.Unknown;
        }
    }
    #endregion

    #region 버튼 사운드 재생
    public void PlayBGM1()
    {
        soundManager.PlayBgm1();
    }
    public void PlayBGM2()
    {
        soundManager.PlayBgm2();
    }
    public void PlayBGM3()
    {
        soundManager.PlayBgm3();
    }

    #endregion
}
