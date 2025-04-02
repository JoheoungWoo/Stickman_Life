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

            Debug.Log($"{ite}�̸� {data}��� �����Դϴ�.");
        }*/

        var data = toolTip.FindToolTipData("�").GetAllData();
        Debug.Log($"{data.type}��� ���� �����߰�, {data.data}");

        data = toolTip.FindToolTipData("ȯ��").GetAllData();
        Debug.Log($"{data.type}��� ���� �����߰�, {data.data}");


        data = toolTip.FindToolTipData("���").GetAllData();
        Debug.Log($"{data.type}��� ���� �����߰�, {data.data}");

    }


    private void Init()
    {
        mainData = new MainData();

        gameManager.InitGame(mainData);
        uiManager.Init(mainData);

        //�� �ʱ�ȭ
        mainData.SetBodyPoint(mainData.maxBodyPoint);
        mainData.SetFoodPoint(mainData.originMaxFoodPoint);     //maxFoodPoint�� �ָ� �ǰ��� 20�� ���ۺ��� ���Ұ� �ǹǷ�
        mainData.SetHealthPoint(mainData.maxHealthPoint);
        mainData.SetLifePoint(mainData.maxLifePoint);
        mainData.SetMentalPoint(mainData.maxMentalPoint);

        //UI �ݿ�
        uiManager.ChangeLevelText(mainData.level);
        uiManager.ChangeBodyBarText(mainData.GetBodyPoint());
        uiManager.ChangeFoodBarText(mainData.GetFoodPoint());
        uiManager.ChangeHealthBarText(mainData.GetHealthPoint());
        uiManager.ChangeLifeBarText(mainData.GetLifePoint());
        uiManager.ChangeMentalBarTextt(mainData.GetMentalPoint());

        uiManager.ChangeGoldText(mainData.nowGold);
        uiManager.ChangeExpBarText();

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


        //null����
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
            case nameof(SkillName.IncreaseBodyRecoveryBtn):
                //myString = newSkillData[(int)SkillName.IncreaseBodyRecoveryBtn].myItemName.ToString();
                myString = $"�ǰ� ��� ���� LV {mainData.healthRepairLevel + 1}";
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
                    mainData.AddExp(4);
                    uiManager.ChangeExpBarText();
                }
                break;
            case nameof(fooditemName.�Ŷ��):
                if(!gameManager.food.UpFoodStatus(20, 30))
                {
                    uiManager.PrintError("��尡 �����մϴ�.");
                }
                else
                {
                    mainData.AddExp(4);
                    uiManager.ChangeExpBarText();
                }
                break;
            case nameof(fooditemName.���):
                if(!gameManager.food.UpFoodStatus(25, 40))
                {
                    uiManager.PrintError("��尡 �����մϴ�.");
                }
                else
                {
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
                switch(gameManager.disease.TreatFoodPoisoning(75)) {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case -2:
                        uiManager.PrintError("���ߵ� ���°� �ƴմϴ�.");
                        break;
                    case 1:
                        mainData.AddExp(7);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
            case nameof(medicineItemName.�����ż���):
                switch (gameManager.disease.TreatHallucination(75))
                {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case -2:
                        uiManager.PrintError("ȯ�� ���°� �ƴմϴ�.");
                        break;
                    case 1:
                        mainData.AddExp(7);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
            case nameof(medicineItemName.�����):
                switch (gameManager.disease.TreatCold(75))
                {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case -2:
                        uiManager.PrintError("���� ���°� �ƴմϴ�.");
                        break;
                    case 1:
                        mainData.AddExp(7);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
            case nameof(medicineItemName.�׾���):
                switch (gameManager.disease.TreatCancer(100))
                {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case -2:
                        uiManager.PrintError("�� ���°� �ƴմϴ�.");
                        break;
                    case 1:
                        mainData.AddExp(7);
                        uiManager.ChangeExpBarText();
                        break;
                    default:
                        break;
                }
                break;
            case nameof(medicineItemName.�ǰ�������):
                switch (gameManager.disease.TreatBodyPoint(60))
                {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
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
