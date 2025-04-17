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
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
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

    //�Ŵ�����
    public UIManager uiManager;
    public SoundManager soundManager;
    public GameManager gameManager;
    public AchievementManager achievementManager;

    public FoodItem foodItem;
    public DiseaseItem diseaseItem;

    public List<EatData> newItemList = new List<EatData>();
    public List<DiseaseData> newDiseaseItemList = new List<DiseaseData>();
    public List<SkillData> newSkillData = new List<SkillData>();

    public GameObject toolTIpPrefab;

    public Transform toolTipParent;


    public JsonDataManager jsonDataManager;

    public PlayerData tempData;

    private void Start()
    {
        Init();
    }

    public void InitUI()
    {
        //UI �ݿ�
        uiManager.ChangeLevelText(mainData.level);
        uiManager.ChangeBodyBarText(mainData.GetBodyPoint());
        uiManager.ChangeFoodBarText(mainData.GetFoodPoint());
        uiManager.ChangeHealthBarText(mainData.GetHealthPoint());
        uiManager.ChangeLifeBarText(mainData.GetLifePoint());
        uiManager.ChangeMentalBarTextt(mainData.GetMentalPoint());

        uiManager.ChangeGoldText(mainData.nowGold);
        uiManager.ChangeExpBarText();
    }

    private void Init()
    {
        mainData = new MainData();

        jsonDataManager = new JsonDataManager();

        try
        {
            if (!jsonDataManager.CheckExistFile("playerData"))
            {
                Debug.Log("json�����Ͱ� ��� �ʱ�ȭ �Ͽ����ϴ�.");

                //���� , �ʱ�ȭ , �ε��� ������ ��ħ
                jsonDataManager.CreateData("playerData");
                jsonDataManager.InitData("playerData");
                mainData.InitData(jsonDataManager.LoadData("playerData").MainDataToPlayerData());
            }
            else
            {
                Debug.Log("json�����Ͱ� �����Ƿ� �ε常 �����մϴ�.");
                var tempData = jsonDataManager.LoadData("playerData").MainDataToPlayerData();
                if (tempData.GetLifePoint() <= 0)
                {
                    //���� , �ʱ�ȭ , �ε��� ������ ��ħ
                    jsonDataManager.CreateData("playerData");
                    jsonDataManager.InitData("playerData");
                    mainData.InitData(jsonDataManager.LoadData("playerData").MainDataToPlayerData());
                }
                else
                {
                    //�����Ͱ� �����Ƿ� �ٷ� �ε�
                    mainData.InitData(jsonDataManager.LoadData("playerData").MainDataToPlayerData());
                }
            }
        }
        catch (System.Exception error)
        {
            Debug.Log($"{error} ������");
            //���� , �ʱ�ȭ , �ε��� ������ ��ħ
            jsonDataManager.CreateData("playerData");
            jsonDataManager.InitData("playerData");
            mainData.InitData(jsonDataManager.LoadData("playerData").MainDataToPlayerData());
        }

        

        //���Ǵ� ������ ����
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


        gameManager.InitGame(mainData);
        uiManager.Init(mainData);
        achievementManager.Init(mainData);
        InitUI();

        //null����
        foodItem?.Init(mainData.itemList);
        diseaseItem?.Init(mainData.diseaseItemList);

    }

    public void SaveData(MainData mainData)
    {
        jsonDataManager.UpdateData(
            new PlayerData
            {
                retryCount = 0,
                playerName = "savePlayer",
                lifePoint = mainData.GetLifePoint(),
                maxLifePoint = mainData.maxLifePoint,
                healthPoint = mainData.GetHealthPoint(),
                maxHealthPoint = mainData.maxHealthPoint,
                bodyPoint = mainData.GetBodyPoint(),
                maxBodyPoint = mainData.maxBodyPoint,
                mentalPoint = mainData.GetMentalPoint(),
                maxMentalPoint = mainData.maxMentalPoint,
                foodPoint = mainData.GetFoodPoint(),
                maxFoodPoint = mainData.maxFoodPoint,
                gold = mainData.nowGold,
                timeSpan = mainData.timeSpan,

                increaseLifePoint = mainData.lifeUpLevel,
                increaseHealthPoint = mainData.healthUpLevel,
                increaseHealthRecoveryPoint = mainData.healthRepairLevel,
                increaseGoldProsperityPoint = mainData.goldUpLevel,

                isFoodPoisoning = mainData.isFoodPoisoning,
                isHallucination = mainData.isHallucination,
                isCold = mainData.isCold,
                isCancer = mainData.isCancer,
                exp = mainData.nowExp,
                maxExp = mainData.maxExp,
                level = mainData.level,

                dietarysupplementCount = mainData.dietarysupplementCount,

                foodPoisoningCount = mainData.foodPoisoningCount,
                hallucinationCount = mainData.hallucinationCount,
                coldCount = mainData.coldCount,
                cancerCount = mainData.cancerCount,

                candyCount = mainData.candyCount,
                chipCount = mainData.chipCount,
                ramyunCount = mainData.ramyunCount,
                kimbabCount = mainData.kimbabCount,
                meatCount = mainData.meatCount,
            }, "playerData");
    }

    public void Delete() {
        jsonDataManager.ClearData("playerData");
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
        Debug.Log($"{itemName} ������ �̸�");
       if (IsMaxLevel(itemName))
        {
            return;
        }

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

    #region �����Ͱ������� �޼ҵ�
    public Sprite GetSprite(string itemName)
    {
        Sprite mySprite = null;
        switch (itemName)
        {
            //�Ļ��� ��ϸ� ����
            case nameof(fooditemName.����):
                mySprite = newItemList[(int)fooditemName.����].itemImage;
                break;
            case nameof(fooditemName.����Ĩ):
                mySprite = newItemList[(int)fooditemName.����Ĩ].itemImage;
                break;
            case nameof(fooditemName.�Ŷ��):
                mySprite = newItemList[(int)fooditemName.�Ŷ��].itemImage;
                break;
            case nameof(fooditemName.���):
                mySprite = newItemList[(int)fooditemName.���].itemImage;
                break;
            case nameof(fooditemName.���):
                mySprite = newItemList[(int)fooditemName.���].itemImage;
                break;

            //�Ƿ��� ��ϸ� ����
            case nameof(medicineItemName.�����):
                mySprite = newDiseaseItemList[(int)medicineItemName.�����].itemImage;
                break;
            case nameof(medicineItemName.�ǰ�������):
                mySprite = newDiseaseItemList[(int)medicineItemName.�ǰ�������].itemImage;
                break;
            case nameof(medicineItemName.�������):
                mySprite = newDiseaseItemList[(int)medicineItemName.�������].itemImage;
                break;
            case nameof(medicineItemName.�׾���):
                mySprite = newDiseaseItemList[(int)medicineItemName.�׾���].itemImage;
                break;
            case nameof(medicineItemName.�����ż���):
                mySprite = newDiseaseItemList[(int)medicineItemName.�����ż���].itemImage;
                break;

            //��ų�� ��ϸ� ����
            case nameof(SkillName.IncreaseLifeBtn):
                mySprite = newSkillData[(int)SkillName.IncreaseLifeBtn].skillImage;
                break;
            case nameof(SkillName.IncreaseHealthBtn):
                mySprite = newSkillData[(int)SkillName.IncreaseHealthBtn].skillImage;
                break;
            case nameof(SkillName.IncreaseHealthRecoveryBtn):
                mySprite = newSkillData[(int)SkillName.IncreaseHealthRecoveryBtn].skillImage;
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
            //�Ļ��� ��ϸ� ����
            case nameof(fooditemName.����):
                myString = newItemList[(int)fooditemName.����].myItemName.ToString();
                break;
            case nameof(fooditemName.����Ĩ):
                myString = newItemList[(int)fooditemName.����Ĩ].myItemName.ToString();
                break;
            case nameof(fooditemName.�Ŷ��):
                myString = newItemList[(int)fooditemName.�Ŷ��].myItemName.ToString();
                break;
            case nameof(fooditemName.���):
                myString = newItemList[(int)fooditemName.���].myItemName.ToString();
                break;
            case nameof(fooditemName.���):
                myString = newItemList[(int)fooditemName.���].myItemName.ToString();
                break;

            //�Ƿ��� ��ϸ� ����
            case nameof(medicineItemName.�����):
                myString = newDiseaseItemList[(int)medicineItemName.�����].myItemName.ToString();
                break;
            case nameof(medicineItemName.�ǰ�������):
                myString = newDiseaseItemList[(int)medicineItemName.�ǰ�������].myItemName.ToString();
                break;
            case nameof(medicineItemName.�������):
                myString = newDiseaseItemList[(int)medicineItemName.�������].myItemName.ToString();
                break;
            case nameof(medicineItemName.�׾���):
                myString = newDiseaseItemList[(int)medicineItemName.�׾���].myItemName.ToString();
                break;
            case nameof(medicineItemName.�����ż���):
                myString = newDiseaseItemList[(int)medicineItemName.�����ż���].myItemName.ToString();
                break;

            //��ų�� ��ϸ� ����
            case nameof(SkillName.IncreaseLifeBtn):
                //myString = newSkillData[(int)SkillName.IncreaseLifeBtn].myItemName.ToString();
                myString = $"����� ���� LV {mainData.lifeUpLevel + 1}";
                break;
            case nameof(SkillName.IncreaseHealthBtn):
                //myString = newSkillData[(int)SkillName.IncreaseHealthBtn].myItemName.ToString();
                myString = $"ü�� ���� LV {mainData.healthUpLevel + 1}";
                break;
            case nameof(SkillName.IncreaseHealthRecoveryBtn):
                //myString = newSkillData[(int)SkillName.IncreaseBodyRecoveryBtn].myItemName.ToString();
                myString = $"ü�� ��� ���� LV {mainData.healthRepairLevel + 1}";
                break;
            case nameof(SkillName.IncreaseGoldProsperityBtn):
                //myString = newSkillData[(int)SkillName.IncreaseGoldProsperityBtn].myItemName.ToString();
                myString = $"��� ȹ�淮 ���� LV {mainData.goldUpLevel + 1}";
                break;
        }
        return myString;
    }
    public string GetItemPrice(string itemName)
    {
        int myPrice = 0;
        switch (itemName)
        {
            //�Ļ��� ��ϸ� ����
            case nameof(fooditemName.����):
                myPrice = newItemList[(int)fooditemName.����].itemPrice;
                break;
            case nameof(fooditemName.����Ĩ):
                myPrice = newItemList[(int)fooditemName.����Ĩ].itemPrice;
                break;
            case nameof(fooditemName.�Ŷ��):
                myPrice = newItemList[(int)fooditemName.�Ŷ��].itemPrice;
                break;
            case nameof(fooditemName.���):
                myPrice = newItemList[(int)fooditemName.���].itemPrice;
                break;
            case nameof(fooditemName.���):
                myPrice = newItemList[(int)fooditemName.���].itemPrice;
                break;

            //�Ƿ��� ��ϸ� ����
            case nameof(medicineItemName.�����):
                myPrice = newDiseaseItemList[(int)medicineItemName.�����].itemPrice;
                break;
            case nameof(medicineItemName.�ǰ�������):
                myPrice = newDiseaseItemList[(int)medicineItemName.�ǰ�������].itemPrice;
                break;
            case nameof(medicineItemName.�������):
                myPrice = newDiseaseItemList[(int)medicineItemName.�������].itemPrice;
                break;
            case nameof(medicineItemName.�׾���):
                myPrice = newDiseaseItemList[(int)medicineItemName.�׾���].itemPrice;
                break;
            case nameof(medicineItemName.�����ż���):
                myPrice = newDiseaseItemList[(int)medicineItemName.�����ż���].itemPrice;
                break;

            //��ų�� ��ϸ� ����
            case nameof(SkillName.IncreaseLifeBtn):
                if (mainData.lifeUpLevel == 0)
                {
                    myPrice = mainData.lifeUpGoldArr[0];
                }
                else if (mainData.lifeUpLevel == 1)
                {
                    myPrice = mainData.lifeUpGoldArr[1];
                }
                else if (mainData.lifeUpLevel == 2)
                {
                    myPrice = mainData.lifeUpGoldArr[2];
                }
                break;
            case nameof(SkillName.IncreaseHealthBtn):
                if (mainData.healthUpLevel == 0)
                {
                    myPrice = mainData.healthUpGoldArr[0];
                }
                else if (mainData.healthUpLevel == 1)
                {
                    myPrice = mainData.healthUpGoldArr[1];
                }
                else if (mainData.healthUpLevel == 2)
                {
                    myPrice = mainData.healthUpGoldArr[2];
                }
                break;
            case nameof(SkillName.IncreaseHealthRecoveryBtn):
                if (mainData.healthRepairLevel == 0)
                {
                    myPrice = mainData.healthRepairUpGoldArr[0];
                }
                else if (mainData.healthRepairLevel == 1)
                {
                    myPrice = mainData.healthRepairUpGoldArr[1];
                }
                else if (mainData.healthRepairLevel == 2)
                {
                    myPrice = mainData.healthRepairUpGoldArr[2];
                }
                break;
            case nameof(SkillName.IncreaseGoldProsperityBtn):
                if (mainData.goldUpLevel == 0)
                {
                    myPrice = mainData.goldUpGoldArr[0];
                }
                else if (mainData.goldUpLevel == 1)
                {
                    myPrice = mainData.goldUpGoldArr[1];
                }
                else if (mainData.goldUpLevel == 2)
                {
                    myPrice = mainData.goldUpGoldArr[2];
                }
                break;
        }
        return myPrice.ToString();
    }

    public string GetItemExplanation(string itemName)
    {
        string myString = "";
        switch (itemName)
        {
            //�Ļ��� ��ϸ� ����
            case nameof(fooditemName.����):
                myString = newItemList[(int)fooditemName.����].itemExplanation;
                break;
            case nameof(fooditemName.����Ĩ):
                myString = newItemList[(int)fooditemName.����Ĩ].itemExplanation;
                break;
            case nameof(fooditemName.�Ŷ��):
                myString = newItemList[(int)fooditemName.�Ŷ��].itemExplanation;
                break;
            case nameof(fooditemName.���):
                myString = newItemList[(int)fooditemName.���].itemExplanation;
                break;
            case nameof(fooditemName.���):
                myString = newItemList[(int)fooditemName.���].itemExplanation;
                break;

            //�Ƿ��� ��ϸ� ����
            case nameof(medicineItemName.�����):
                myString = newDiseaseItemList[(int)medicineItemName.�����].itemExplanation;
                break;
            case nameof(medicineItemName.�ǰ�������):
                myString = newDiseaseItemList[(int)medicineItemName.�ǰ�������].itemExplanation;
                break;
            case nameof(medicineItemName.�������):
                myString = newDiseaseItemList[(int)medicineItemName.�������].itemExplanation;
                break;
            case nameof(medicineItemName.�׾���):
                myString = newDiseaseItemList[(int)medicineItemName.�׾���].itemExplanation;
                break;
            case nameof(medicineItemName.�����ż���):
                myString = newDiseaseItemList[(int)medicineItemName.�����ż���].itemExplanation;
                break;

            //��ų�� ��ϸ� ����
            case nameof(SkillName.IncreaseLifeBtn):
                if (mainData.lifeUpLevel == 0)
                {
                    myString = $"������� {mainData.lifeUpArr[0]} �����մϴ�.";
                }
                else if (mainData.lifeUpLevel == 1)
                {
                    myString = $"������� {mainData.lifeUpArr[1]} �����մϴ�.";
                }
                else if (mainData.lifeUpLevel == 2)
                {
                    myString = $"������� {mainData.lifeUpArr[2]} �����մϴ�.";
                }
                break;
            case nameof(SkillName.IncreaseHealthBtn):
                if (mainData.healthUpLevel == 0)
                {
                    myString = $"ü���� {mainData.healthUpArr[0]} �����մϴ�.";
                }
                else if (mainData.healthUpLevel == 1)
                {
                    myString = $"ü���� {mainData.healthUpArr[1]} �����մϴ�.";
                }
                else if (mainData.healthUpLevel == 2)
                {
                    myString = $"ü���� {mainData.healthUpArr[2]} �����մϴ�.";
                }
                break;
            case nameof(SkillName.IncreaseHealthRecoveryBtn):
                if (mainData.healthRepairLevel == 0)
                {
                    myString = $"ü���� {mainData.healthRepairArr[0]}�ʴ� 1�� �߰��� ȸ���˴ϴ�.";
                }
                else if (mainData.healthRepairLevel == 1)
                {
                    myString = $"ü���� {mainData.healthRepairArr[1]}�ʴ� 1�� �߰��� ȸ���˴ϴ�.";
                }
                else if (mainData.healthRepairLevel == 2)
                {
                    myString = $"ü���� {mainData.healthRepairArr[2]}�ʴ� 1�� �߰��� ȸ���˴ϴ�.";
                }
                break;
            case nameof(SkillName.IncreaseGoldProsperityBtn):
                if (mainData.goldUpLevel == 0)
                {
                    myString = $"��带 {mainData.goldUpArr[0]}�ʴ� 1�� �߰��� ȹ���մϴ�.";
                }
                else if (mainData.goldUpLevel == 1)
                {
                    myString = $"��带 {mainData.goldUpArr[1]}�ʴ� 1�� �߰��� ȹ���մϴ�.";
                }
                else if (mainData.goldUpLevel == 2)
                {
                    myString = $"��带 {mainData.goldUpArr[2]}�ʴ� 1�� �߰��� ȹ���մϴ�.";
                }
                break;
        }
        return myString;
    }
    #endregion

    #region ���� ����
    public void PerformAction(string itemName)
    {
        Debug.Log(itemName + "����3");
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
        Debug.Log($"EatFood�� ���������� , �̰��� ������ �̸��̴�. {itemName}");
        switch(itemName)
        {
            case nameof(fooditemName.����):
                if (!gameManager.food.UpFoodStatus(5, 10))
                {
                    uiManager.PrintError("��尡 �����մϴ�.");
                } else
                {
                    mainData.candyCount += 1;
                    mainData.AddExp(4);
                    uiManager.ChangeExpBarText();
                }
                break;
            case nameof(fooditemName.����Ĩ):
                if(!gameManager.food.UpFoodStatus(10, 15))
                {
                    uiManager.PrintError("��尡 �����մϴ�.");
                }
                else
                {
                    mainData.chipCount += 1;
                    mainData.AddExp(4);
                    uiManager.ChangeExpBarText();
                }
                break;
            case nameof(fooditemName.�Ŷ��):
                if(!gameManager.food.UpFoodStatus(20, 25))
                {
                    uiManager.PrintError("��尡 �����մϴ�.");
                }
                else
                {
                    mainData.ramyunCount += 1;
                    mainData.AddExp(4);
                    uiManager.ChangeExpBarText();
                }
                break;
            case nameof(fooditemName.���):
                if(!gameManager.food.UpFoodStatus(30, 40))
                {
                    uiManager.PrintError("��尡 �����մϴ�.");
                }
                else
                {
                    mainData.kimbabCount += 1;
                    mainData.AddExp(4);
                    uiManager.ChangeExpBarText();
                }
                break;
            case nameof(fooditemName.���):
                if(!gameManager.food.UpFoodStatus(40, 55))
                {
                    uiManager.PrintError("��尡 �����մϴ�.");
                }
                else
                {
                    mainData.meatCount += 1;
                    mainData.AddExp(4);
                    uiManager.ChangeExpBarText();
                }
                break;
        }
    }

    private void TakeMedicine(string itemName)
    {
        Debug.Log($"TakeMedicine�� ���������� , �̰��� ������ �̸��̴�. {itemName}");
        switch (itemName)
        {
            case nameof(medicineItemName.�������):
                switch(gameManager.disease.TreatFoodPoisoning(60)) {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case -2:
                        uiManager.PrintError("���ߵ� ���°� �ƴմϴ�.");
                        break;
                    case 1:
                        mainData.foodPoisoningCount += 1;
                        mainData.AddExp(10);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
            case nameof(medicineItemName.�����ż���):
                switch (gameManager.disease.TreatHallucination(60))
                {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case -2:
                        uiManager.PrintError("ȯ�� ���°� �ƴմϴ�.");
                        break;
                    case 1:
                        mainData.hallucinationCount += 1;
                        mainData.AddExp(10);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
            case nameof(medicineItemName.�����):
                switch (gameManager.disease.TreatCold(60))
                {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case -2:
                        uiManager.PrintError("���� ���°� �ƴմϴ�.");
                        break;
                    case 1:
                        mainData.coldCount += 1;
                        mainData.AddExp(10);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
            case nameof(medicineItemName.�׾���):
                switch (gameManager.disease.TreatCancer(60))
                {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case -2:
                        uiManager.PrintError("�� ���°� �ƴմϴ�.");
                        break;
                    case 1:
                        mainData.cancerCount += 1;
                        mainData.AddExp(10);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
            case nameof(medicineItemName.�ǰ�������):
                switch (gameManager.disease.TreatBodyPoint(40))
                {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case 1:
                        mainData.dietarysupplementCount += 1; //1�� ī��Ʈ ����
                        mainData.AddExp(10);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
        }
    }

    public bool IsMaxLevel(string itemName)
    {
        bool tempBool = false;
        switch (itemName)
        {
            case nameof(SkillName.IncreaseLifeBtn):
                tempBool = mainData.lifeUpLevel == mainData.lifeUpMaxLevel;
                break;
            case nameof(SkillName.IncreaseHealthBtn):
                tempBool = mainData.healthUpLevel == mainData.healthUpMaxLevel;
                break;
            case nameof(SkillName.IncreaseHealthRecoveryBtn):
                tempBool = mainData.healthRepairLevel == mainData.healthRepairMaxLevel;
                break;
            case nameof(SkillName.IncreaseGoldProsperityBtn):
                tempBool = mainData.goldUpLevel == mainData.goldUpMaxLevel;
                break;
            default:
                tempBool = false;
                break;
        }
        return tempBool;
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
            case nameof(SkillName.IncreaseHealthRecoveryBtn):
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

    #region ��ư ���� ���
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
