using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

[System.Serializable]
public struct ShopStatus
{
    public Image image;
    public TMP_Text text;
}

public class UIManager : MonoBehaviour
{
    //
    public TMP_Text levelText;
    public TMP_Text goldText;
    public TMP_Text timeText;

    //���� �̹����� ����
    public TMP_Text lifeBarText;
    public TMP_Text bodyBarText;
    public TMP_Text healthBarText;
    public TMP_Text mentalBarText;
    public TMP_Text foodBarText;
    public TMP_Text expBarText;

    public Image[] diseaseImgs;

    public Image expGage;
    public Image lifeGage;
    public Image bodyGage;
    public Image healthGage;
    public Image mentalGage;
    public Image foodGage;

    //���� �̻ڰ� ����
    public GameObject defaultUIObj;
    public GameObject eatUIObj;
    public GameObject therapyUIObj;
    public GameObject itemCheckUIObj;
    public GameObject skillUIObj;
    public GameObject skillCheckUIObj;
    public GameObject menuUIObj;

    public GameManager gameManager;
    public Player player;

    //�������� ���� �͵�
    public ShopStatus foodStatus;
    public ShopStatus bodyStatus;

    //��ų�� obj���
    public GameObject LifeUpObj;
    public GameObject healthUpObj;
    public GameObject healthRecoveryObj;
    public GameObject increaseGoldObj;


    //����
    public TMP_Text errorText;

    public Stopwatch stopwatch;

    private MainData mainData;

    private PlayerCondition playerCondition;

    //�ൿ�� ����
    private Skill skill;
    private Therapy therapy;
    private Hobby hobby;
    private Eat eat;
    private Work work;

    //������ UI
    private bool isEatUI;

    // ���� UI

    public GameObject[] StatusTooltip;
    public GameObject DiseasesTooltip;
    public TMP_Text TipText;
    public bool onTip;

    // 2��� ��ư
    public Button speedTwiceBtn;
    public bool onSpeedTwice;



    public void Init(MainData mainData)
    {
        this.mainData = mainData;
        skill = new Skill(mainData,this);
        therapy = new Therapy(mainData);
        hobby = new Hobby(mainData);
        eat = new Eat(mainData);
        work = new Work(mainData);
        stopwatch = gameObject.AddComponent<Stopwatch>();
        stopwatch.StopwatchInit(timeText);
        stopwatch.PlayStopwatch();
    }

    private void Start()
    {
        playerCondition = playerCondition = new PlayerCondition(mainData);
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("Escape�� ������");
                if (!DataManager.Instance.gameManager.IsPause)
                {
                    OpenMenuWindow();
                } else
                {
                    CloseMenuWindow();
                }
            }
        } else if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("Escape�� ������");
                if (!DataManager.Instance.gameManager.IsPause)
                {
                    OpenMenuWindow();
                }
                else
                {
                    CloseMenuWindow();
                }
            }
        }

        //ĳ���� ���� ��� ǥ��
        switch (playerCondition.PlayAllAction())
        {
            case PlayerStatus.playerDeath:
                player.ChangePlayerDeathImg();
                ChangeLifeBarText(0);
                ChangeBodyBarText(0);
                ChangeHealthBarText(0);
                ChangeMentalBarTextt(0);
                ChangeFoodBarText(0);
                DataManager.Instance.gameManager.PauseGame();
                break;
            case PlayerStatus.playerDisease:
                player.ChangePlayerDiseaseImg();
                break;
            case PlayerStatus.playerLowBody:
                player.ChangePlayerLowBodyImg();
                break;
            case PlayerStatus.playerLowHealth:
                player.ChangePlayerLowHealthImg();
                break;
            case PlayerStatus.playerDefault:
                player.ChangePlayerDefaultImg();
                break;
            default:
                player.ChangePlayerDefaultImg();
                break;
        }

        //�ൿ�� ����

        if (mainData.skillBool)
        {
            mainData.skillBool = false;
            skill.Play();
        }

        if (mainData.workBool)
        {
            mainData.workBool = false;
            work.Play();
        }

        if (mainData.therapyBool)
        {
            Debug.Log("ġ�� ����!!ui");
            therapy.Play(null);
            mainData.therapyBool = false;
        }

        if (mainData.hobbyBool)
        {
            mainData.hobbyBool = false;
            hobby.Play();
        }
        if (mainData.eatBool)
        {
            mainData.eatBool = false;
            eat.Play();
        }

        if (foodStatus.image.gameObject.activeInHierarchy)
        {
            ChangeFoodBarTextVer2();
        }

        if (bodyStatus.image.gameObject.activeInHierarchy)
        {
            ChangeBodyBarTextVer2();
        }
    }

    #region ��ư�� ��ȭ�ϴ� �޼ҵ� ����

    public void ChangeLevelText(int level)
    {
        levelText.text = $"LV {level.ToString()}";
    }

    public void ChangeGoldText(int gold)
    {
        goldText.text = gold.ToString();
    }

    public void ChangeTimeText(int time)
    {
        timeText.text = time.ToString();
    }

    public void ChangeLifeBarText(int life)
    {
        lifeBarText.text = life.ToString();
        lifeGage.fillAmount = ((float)life / mainData.maxLifePoint);
    }

    public void ChangeBodyBarText(int body)
    {
        bodyBarText.text = body.ToString();
        bodyGage.fillAmount = ((float)body / mainData.maxBodyPoint);
    }

    public void ChangeHealthBarText(int health)
    {
        healthBarText.text = health.ToString();
        healthGage.fillAmount = ((float)health / mainData.maxHealthPoint);
    }

    public void ChangeMentalBarTextt(int mental)
    {
        mentalBarText.text = mental.ToString();
        mentalGage.fillAmount = ((float)mental / mainData.maxMentalPoint);
    }

    public void ChangeFoodBarText(int food)
    {
        foodBarText.text = food.ToString();
        foodGage.fillAmount = ((float)food / mainData.maxFoodPoint);
    }

    public void ChangeFoodBarTextVer2()
    {
        Debug.Log("����ǵ�1");
        var tempData = mainData.GetFoodPoint();
        Debug.Log($"����ǵ�2 �� {tempData} / {mainData.maxFoodPoint}");
        if (foodStatus.text.gameObject.activeSelf)
            foodStatus.text.text = tempData.ToString();
        if (foodStatus.image.gameObject.activeSelf)
            foodStatus.image.fillAmount = ((float)tempData / mainData.maxFoodPoint);
    }

    public void ChangeBodyBarTextVer2()
    {
        Debug.Log("����ǵ�2");
        var tempData = mainData.GetBodyPoint();
        if (bodyStatus.text.gameObject.activeSelf)
            bodyStatus.text.text = tempData.ToString();
        if (bodyStatus.image.gameObject.activeSelf)
            bodyStatus.image.fillAmount = ((float)tempData / mainData.maxBodyPoint);
    }

    public void ChangeExpBarText(bool isMaxLevel = false)
    {
        if(mainData.isMaxLevel)
        {
            expBarText.text = $"{mainData.nowExp.ToString()}";
            expGage.fillAmount = 1;
        } else
        {
            expBarText.text = $"{mainData.nowExp.ToString()} / {mainData.maxExp}";
            expGage.fillAmount = ((float)mainData.nowExp / mainData.maxExp);
        }
    }
    #endregion

    #region �ൿ�� ����
    public void ClickWorkBtn()
    {
        mainData.workBool = true;

        //���� ����
        DataManager.Instance.PlayBGM2();
    }

    public void ClickHobbyBtn()
    {
        mainData.hobbyBool = true;

        //���� ����
        DataManager.Instance.PlayBGM2();
    }

    public void ClickEatBtn()
    {
        //mainData.eatBool = true;
        OpenEatWindow();

        //���� ����
        DataManager.Instance.PlayBGM2();
    }

    public void ClickTherapyBtn()
    {
        //mainData.therapyBool = true;
        OpenTherapyWindow();

        //���� ����
        DataManager.Instance.PlayBGM2();
    }


    public void ClickSkillBtn()
    {
        //mainData.skillBool = true;
        OpenSkillWindow();

        //���� ����
        DataManager.Instance.PlayBGM2();
    }
    #endregion

    #region ���� ���� ǥ��
    public void ChangeFoodPoisoning(bool isStatus)
    {
        if(isStatus)
        {
            diseaseImgs[0].color = new Color(1, 1, 1);
        } else
        {
            diseaseImgs[0].color = new Color(0.3f, 0.3f, 0.3f);
        }
    }

    public void ChangeHallucination(bool isStatus)
    {
        if (isStatus)
        {
            diseaseImgs[1].color = new Color(1, 1, 1);
        }
        else
        {
            diseaseImgs[1].color = new Color(0.3f, 0.3f, 0.3f);
        }
    }

    public void ChangeCold(bool isStatus)
    {
        if (isStatus)
        {
            diseaseImgs[2].color = new Color(1, 1, 1);
        }
        else
        {
            diseaseImgs[2].color = new Color(0.3f, 0.3f, 0.3f);
        }
    }

    public void ChangeCancer(bool isStatus)
    {
        if (isStatus)
        {
            diseaseImgs[3].color = new Color(1, 1, 1);
        }
        else
        {
            diseaseImgs[3].color = new Color(0.3f, 0.3f, 0.3f);
        }
    }
    #endregion


    #region ������
	public void PrintError(string error) {
		if(error != null && error != "" && errorText.gameObject.activeSelf) {
			errorText.text = error;
			Invoke(nameof(OFFError), 1f);
		}
	}
	public void OFFError() {
		if(errorText.gameObject.activeSelf) {
			errorText.text = "";
		}
	}

    public void LevelUp()
    {
        mainData.nowExp = mainData.maxExp;
        ChangeExpBarText();
    }

    public void GoldUp()
    {
        mainData.nowGold += 100;
        if (mainData.nowGold + 100 >= mainData.maxGold)
        {
            mainData.nowGold = mainData.maxGold;
        }
        ChangeGoldText(mainData.nowGold);
    }

    public void BodyDown()
    {
        mainData.SetBodyPoint(mainData.GetBodyPoint() - 10 <= 0 ? 
            0 : mainData.GetBodyPoint() - 10);
        ChangeBodyBarText(mainData.GetBodyPoint());
    }

    public void MentalDown()
    {
        mainData.SetMentalPoint(mainData.GetMentalPoint() - 10 <= 0 ?
            0 : mainData.GetMentalPoint() - 10);
        ChangeMentalBarTextt(mainData.GetMentalPoint());
    }

    public void HealthDown()
    {
        mainData.SetHealthPoint(mainData.GetHealthPoint() - 10 <= 0 ?
            0 : mainData.GetHealthPoint() - 10);
        ChangeHealthBarText(mainData.GetHealthPoint());
    }

    public void FoodDown()
    {
        mainData.SetFoodPoint(mainData.GetFoodPoint() - 10 <= 0 ?
            0 : mainData.GetFoodPoint() - 10);
        ChangeFoodBarText(mainData.GetFoodPoint());
    }

    public void ExpUp()
    {
        mainData.AddExp(50);
        ChangeExpBarText();
    }

    public void SpeedChange()
    {
        if(onSpeedTwice == true)
        {
            SpeedDown();
        } else
        {
            SpeedUp();
        }
    }
    public void SpeedUp()
    {
        onSpeedTwice = true;
        speedTwiceBtn.image.color = new Color(0.6f, 0.6f, 0.6f);
        gameManager.timeScale = 2;
    }

    public void SpeedDown()
    {
        onSpeedTwice = false;
        speedTwiceBtn.image.color = new Color(1, 1, 1);
        gameManager.timeScale = 1;
    }

    #endregion

    #region ��ư ��ȣ�ۿ� ����
    public void OpenEatWindow()
    {
        defaultUIObj.SetActive(false);
        eatUIObj.SetActive(true);
    }
    public void CloseEatWindow()
    {
        defaultUIObj.SetActive(true);
        eatUIObj.SetActive(false);
        DataManager.Instance.PlayBGM2();
    }

    public void OpenTherapyWindow()
    {
        defaultUIObj.SetActive(false);
        therapyUIObj.SetActive(true);
    }
    public void CloseTherapyWindow()
    {
        defaultUIObj.SetActive(true);
        therapyUIObj.SetActive(false);
        DataManager.Instance.PlayBGM2();
    }

    public void OpenSkillWindow()
    {
        defaultUIObj.SetActive(false);
        skillUIObj.SetActive(true);
    }

    public void CloseSkillWindow()
    {
        defaultUIObj.SetActive(true);
        skillUIObj.SetActive(false);
        DataManager.Instance.PlayBGM2();
    }


    #region ������ ������ ��� ����
    public void OpenItemCheckWindow()
    {
        itemCheckUIObj.SetActive(true);
        if(eatUIObj.activeInHierarchy)
        {
            eatUIObj.SetActive(false);
            isEatUI = true; //true�� EatUI�� ����
        }
        if(therapyUIObj.activeInHierarchy)
        {
            therapyUIObj.SetActive(false);
            isEatUI = false; //false�� TherapyUI�� ����
        }
    }

    public void BackItemCheckWindow()
    {
        DataManager.Instance.PlayBGM2();
        itemCheckUIObj.SetActive(false);
        if(isEatUI)
        {
            eatUIObj.SetActive(true);
        } else
        {
            therapyUIObj.SetActive(true);
        }
    }

    public void CloseItemCheckWindow()
    {
        defaultUIObj.SetActive(true);
        itemCheckUIObj.SetActive(false);
        DataManager.Instance.PlayBGM2();
    }
    #endregion

    #region ��ų �ø� ��� ����
    public void OpenSkillCheckWindow()
    {
        skillCheckUIObj.SetActive(true);
        skillUIObj.SetActive(false);
    }

    public void BackSkillCheckWindow()
    {
        DataManager.Instance.PlayBGM2();
        skillCheckUIObj.SetActive(false);
        skillUIObj.SetActive(true);
    }

    public void CloseSkillCheckWindow()
    {
        defaultUIObj.SetActive(true);
        skillCheckUIObj.SetActive(false);
        DataManager.Instance.PlayBGM2();
    }
    #endregion

    public void OpenMenuWindow()
    {
        menuUIObj.SetActive(true);
        DataManager.Instance.gameManager.PauseGame();
    }

    public void CloseMenuWindow()
    {
        menuUIObj.SetActive(false);
        DataManager.Instance.gameManager.ContinueGame();
    }


    #endregion

    #region ��ų ���� ���� ����
    //����� ����
    public void LifeUp()
    {
        skill.Play(nameof(SkillType.���������));
        ChangeLifeBarText(mainData.GetLifePoint());
        ChangeGoldText(mainData.nowGold);
        DataManager.Instance.PlayBGM2();
    }

    public void HealthUp()
    {
        skill.Play(nameof(SkillType.ü������));
        ChangeHealthBarText(mainData.GetHealthPoint());
        ChangeGoldText(mainData.nowGold);
        DataManager.Instance.PlayBGM2();
    }

    public void HealthRecoveryUp()
    {
        skill.Play(nameof(SkillType.ü���������));
        ChangeGoldText(mainData.nowGold);
        DataManager.Instance.PlayBGM2();
    }

    public void IncreaseGoldUp()
    {
        skill.Play(nameof(SkillType.���ȹ�淮����));
        ChangeGoldText(mainData.nowGold);
        DataManager.Instance.PlayBGM2();
    }

    public void OFFButtons(string buttonType)
    {
        switch (buttonType)
        {
            case nameof(SkillType.���������):
                OFFInteractableBtn(LifeUpObj.transform.GetChild(1).GetComponent<Button>());
                break;
            case nameof(SkillType.ü������):
                OFFInteractableBtn(healthUpObj.transform.GetChild(1).GetComponent<Button>());
                break;
            case nameof(SkillType.ü���������):
                OFFInteractableBtn(healthRecoveryObj.transform.GetChild(1).GetComponent<Button>());
                break;
            case nameof(SkillType.���ȹ�淮����):
                OFFInteractableBtn(increaseGoldObj.transform.GetChild(1).GetComponent<Button>());
                break;
            default:
                break;
        }
    }

    public void ChangeTMPText(string buttonType)
    {
        switch (buttonType)
        {
            case nameof(SkillType.���������):
                ChangeSkillLevel(LifeUpObj.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>(), mainData.lifeUpLevel);
                break;
            case nameof(SkillType.ü������):
                ChangeSkillLevel(healthUpObj.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>(),mainData.healthUpLevel);
                break;
            case nameof(SkillType.ü���������):
                ChangeSkillLevel(healthRecoveryObj.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>(), mainData.healthRepairLevel);
                break;
            case nameof(SkillType.���ȹ�淮����):
                ChangeSkillLevel(increaseGoldObj.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>(), mainData.goldUpLevel);
                break;
            default:
                break;
        }
    }

    //��ȣ�ۿ� ��ư ����
    public void OFFInteractableBtn(Button btn)
    {
        btn.interactable = false;
    }
    
    public void ChangeSkillLevel(TMPro.TMP_Text tmpText, int level)
    {
        if(level == 3)
        {
            tmpText.text = "LV MAX";
        } else
        {
            tmpText.text = $"LV {level}";
        }
    }

    //
    #endregion

    #region ���� ���� ����

    /// <summary>
    /// �����
    /// </summary>
    public void Tooltip00()
    {
        if (onTip == false)
        {
            StatusTooltip[0].gameObject.SetActive(true);
            onTip = true;
            Invoke("EndTip", 1.5f);
        }
    }

    /// <summary>
    /// �ǰ�
    /// </summary>
    public void Tooltip01()
    {
        if (onTip == false)
        {
            StatusTooltip[1].gameObject.SetActive(true);
            onTip = true;
            Invoke("EndTip", 1.5f);
        }
    }

    /// <summary>
    /// ü��
    /// </summary>
    public void Tooltip02() 
    {
        if (onTip == false)
        {
            StatusTooltip[2].gameObject.SetActive(true);
            onTip = true;
            Invoke("EndTip", 1.5f);
        }
    }

    /// <summary>
    /// ���ŷ�
    /// </summary>
    public void Tooltip03()
    {
        if (onTip == false)
        {
            StatusTooltip[3].gameObject.SetActive(true);
            onTip = true;
            Invoke("EndTip", 1.5f);
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    public void Tooltip04()
    {
        if (onTip == false)
        {
            StatusTooltip[4].gameObject.SetActive(true);
            onTip = true;
            Invoke("EndTip", 1.5f);
        }
    }

    /// <summary>
    /// ���ߵ�
    /// </summary>
    public void Tooltip05()
    {
        if (onTip == false)
        {
            DiseasesTooltip.gameObject.SetActive(true);
            TipText.text = "<color=yellow><���ߵ�></color> �������� 50 �̻����� ȸ���� �� �����ϴ�.";
            onTip = true;
            Invoke("EndTip", 1.5f);
        }
    }

    /// <summary>
    /// ȯ��
    /// </summary>
    public void Tooltip06()
    {
        if (onTip == false)
        {
            DiseasesTooltip.gameObject.SetActive(true);
            TipText.text = "<color=yellow><ȯ��></color> ���ŷ� �Ҹ��� 2�谡 �˴ϴ�.";
            onTip = true;
            Invoke("EndTip", 1.5f);
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Tooltip07() 
    {
        if (onTip == false)
        {
            DiseasesTooltip.gameObject.SetActive(true);
            TipText.text = "<color=yellow><����></color> ü���� �ڵ����� ������� �ʽ��ϴ�.";
            onTip = true;
            Invoke("EndTip", 1.5f);
        }
    }

    /// <summary>
    /// ��
    /// </summary>
    public void Tooltip08()
    {
        if (onTip == false)
        {
            DiseasesTooltip.gameObject.SetActive(true);
            TipText.text = "<color=yellow><��></color> �ǰ��� �߰��� �����ϱ� �����մϴ�.";
            onTip = true;
            Invoke("EndTip", 1.5f);
        }
    }

    public void EndTip()
    {
        onTip = false;
        StatusTooltip[0].gameObject.SetActive(false);
        StatusTooltip[1].gameObject.SetActive(false);
        StatusTooltip[2].gameObject.SetActive(false);
        StatusTooltip[3].gameObject.SetActive(false);
        StatusTooltip[4].gameObject.SetActive(false);
        DiseasesTooltip.gameObject.SetActive(false);
    }

    #endregion
}
