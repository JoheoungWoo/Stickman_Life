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

    //추후 이미지로 변경
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

    //추후 이쁘게 변경
    public GameObject defaultUIObj;
    public GameObject eatUIObj;
    public GameObject therapyUIObj;
    public GameObject itemCheckUIObj;
    public GameObject skillUIObj;
    public GameObject skillCheckUIObj;
    public GameObject menuUIObj;

    public GameManager gameManager;
    public Player player;

    //상점에서 보는 것들
    public ShopStatus foodStatus;
    public ShopStatus bodyStatus;

    //스킬의 obj등록
    public GameObject LifeUpObj;
    public GameObject healthUpObj;
    public GameObject healthRecoveryObj;
    public GameObject increaseGoldObj;


    //에러
    public TMP_Text errorText;

    public Stopwatch stopwatch;

    private MainData mainData;

    private PlayerCondition playerCondition;

    //행동을 정의
    private Skill skill;
    private Therapy therapy;
    private Hobby hobby;
    private Eat eat;
    private Work work;

    //복구할 UI
    private bool isEatUI;

    // 툴팁 UI

    public GameObject[] StatusTooltip;
    public GameObject DiseasesTooltip;
    public TMP_Text TipText;
    public bool onTip;

    // 2배속 버튼
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
                Debug.Log("Escape를 눌러따");
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
                Debug.Log("Escape를 눌러따");
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

        //캐릭터 상태 모습 표시
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

        //행동을 정의

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
            Debug.Log("치료 실행!!ui");
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

    #region 버튼들 변화하는 메소드 정의

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
        Debug.Log("실행되따1");
        var tempData = mainData.GetFoodPoint();
        Debug.Log($"실행되따2 값 {tempData} / {mainData.maxFoodPoint}");
        if (foodStatus.text.gameObject.activeSelf)
            foodStatus.text.text = tempData.ToString();
        if (foodStatus.image.gameObject.activeSelf)
            foodStatus.image.fillAmount = ((float)tempData / mainData.maxFoodPoint);
    }

    public void ChangeBodyBarTextVer2()
    {
        Debug.Log("실행되따2");
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

    #region 행동을 정의
    public void ClickWorkBtn()
    {
        mainData.workBool = true;

        //사운드 실행
        DataManager.Instance.PlayBGM2();
    }

    public void ClickHobbyBtn()
    {
        mainData.hobbyBool = true;

        //사운드 실행
        DataManager.Instance.PlayBGM2();
    }

    public void ClickEatBtn()
    {
        //mainData.eatBool = true;
        OpenEatWindow();

        //사운드 실행
        DataManager.Instance.PlayBGM2();
    }

    public void ClickTherapyBtn()
    {
        //mainData.therapyBool = true;
        OpenTherapyWindow();

        //사운드 실행
        DataManager.Instance.PlayBGM2();
    }


    public void ClickSkillBtn()
    {
        //mainData.skillBool = true;
        OpenSkillWindow();

        //사운드 실행
        DataManager.Instance.PlayBGM2();
    }
    #endregion

    #region 질병 상태 표시
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


    #region 디버깅용
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

    #region 버튼 상호작용 정의
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


    #region 아이템 구매할 경우 실행
    public void OpenItemCheckWindow()
    {
        itemCheckUIObj.SetActive(true);
        if(eatUIObj.activeInHierarchy)
        {
            eatUIObj.SetActive(false);
            isEatUI = true; //true면 EatUI로 복귀
        }
        if(therapyUIObj.activeInHierarchy)
        {
            therapyUIObj.SetActive(false);
            isEatUI = false; //false면 TherapyUI로 복귀
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

    #region 스킬 올릴 경우 실행
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

    #region 스킬 영역 동작 정의
    //생명력 증가
    public void LifeUp()
    {
        skill.Play(nameof(SkillType.생명력증가));
        ChangeLifeBarText(mainData.GetLifePoint());
        ChangeGoldText(mainData.nowGold);
        DataManager.Instance.PlayBGM2();
    }

    public void HealthUp()
    {
        skill.Play(nameof(SkillType.체력증가));
        ChangeHealthBarText(mainData.GetHealthPoint());
        ChangeGoldText(mainData.nowGold);
        DataManager.Instance.PlayBGM2();
    }

    public void HealthRecoveryUp()
    {
        skill.Play(nameof(SkillType.체력재생증가));
        ChangeGoldText(mainData.nowGold);
        DataManager.Instance.PlayBGM2();
    }

    public void IncreaseGoldUp()
    {
        skill.Play(nameof(SkillType.골드획득량증가));
        ChangeGoldText(mainData.nowGold);
        DataManager.Instance.PlayBGM2();
    }

    public void OFFButtons(string buttonType)
    {
        switch (buttonType)
        {
            case nameof(SkillType.생명력증가):
                OFFInteractableBtn(LifeUpObj.transform.GetChild(1).GetComponent<Button>());
                break;
            case nameof(SkillType.체력증가):
                OFFInteractableBtn(healthUpObj.transform.GetChild(1).GetComponent<Button>());
                break;
            case nameof(SkillType.체력재생증가):
                OFFInteractableBtn(healthRecoveryObj.transform.GetChild(1).GetComponent<Button>());
                break;
            case nameof(SkillType.골드획득량증가):
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
            case nameof(SkillType.생명력증가):
                ChangeSkillLevel(LifeUpObj.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>(), mainData.lifeUpLevel);
                break;
            case nameof(SkillType.체력증가):
                ChangeSkillLevel(healthUpObj.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>(),mainData.healthUpLevel);
                break;
            case nameof(SkillType.체력재생증가):
                ChangeSkillLevel(healthRecoveryObj.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>(), mainData.healthRepairLevel);
                break;
            case nameof(SkillType.골드획득량증가):
                ChangeSkillLevel(increaseGoldObj.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>(), mainData.goldUpLevel);
                break;
            default:
                break;
        }
    }

    //상호작용 버튼 끄기
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

    #region 툴팁 제어 영역

    /// <summary>
    /// 생명력
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
    /// 건강
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
    /// 체력
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
    /// 정신력
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
    /// 포만감
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
    /// 식중독
    /// </summary>
    public void Tooltip05()
    {
        if (onTip == false)
        {
            DiseasesTooltip.gameObject.SetActive(true);
            TipText.text = "<color=yellow><식중독></color> 포만감이 50 이상으로 회복될 수 없습니다.";
            onTip = true;
            Invoke("EndTip", 1.5f);
        }
    }

    /// <summary>
    /// 환각
    /// </summary>
    public void Tooltip06()
    {
        if (onTip == false)
        {
            DiseasesTooltip.gameObject.SetActive(true);
            TipText.text = "<color=yellow><환각></color> 정신력 소모량이 2배가 됩니다.";
            onTip = true;
            Invoke("EndTip", 1.5f);
        }
    }

    /// <summary>
    /// 감기
    /// </summary>
    public void Tooltip07() 
    {
        if (onTip == false)
        {
            DiseasesTooltip.gameObject.SetActive(true);
            TipText.text = "<color=yellow><감기></color> 체력이 자동으로 재생되지 않습니다.";
            onTip = true;
            Invoke("EndTip", 1.5f);
        }
    }

    /// <summary>
    /// 암
    /// </summary>
    public void Tooltip08()
    {
        if (onTip == false)
        {
            DiseasesTooltip.gameObject.SetActive(true);
            TipText.text = "<color=yellow><암></color> 건강이 추가로 감소하기 시작합니다.";
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
