using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickBtn : MonoBehaviour , IPointerClickHandler
{
    public enum ShopType { Shop, Skill }

    public void OnPointerClick(PointerEventData eventData)
    { 
        switch(gameObject.name)
        {
            case "EatItemCopy":
                SetCheckUI(ShopType.Shop);
                DataManager.Instance.PlayBGM2();
                break;
            case "ThrapyItemCopy":
                SetCheckUI(ShopType.Shop);
                DataManager.Instance.PlayBGM2();
                break;
            case "BuyBtn":
                BuyItem();
                DataManager.Instance.PlayBGM2();
                break;
            case "IncreaseLifeBtn":
                SetCheckUI(ShopType.Skill);
                DataManager.Instance.PlayBGM2();
                break;
            case "IncreaseHealthBtn":
                SetCheckUI(ShopType.Skill);
                DataManager.Instance.PlayBGM2();
                break;
            case "IncreaseBodyRecoveryBtn":
                SetCheckUI(ShopType.Skill);
                DataManager.Instance.PlayBGM2();
                break;
            case "IncreaseGoldProsperityBtn":
                SetCheckUI(ShopType.Skill);
                DataManager.Instance.PlayBGM2();
                break;
            case "SkillBuyBtn":
                SkillBuyItem();
                break;
            default:
                break;
        }
    }

    #region 디버깅 기능
    //이름과 가격 불러오기 테스트
    public void LoadItemTest()
    {
        Debug.Log($"아이템 이름 : {gameObject?.name}");
        Debug.Log($"아이템 이름 : {transform?.GetChild(2).GetComponent<TMPro.TMP_Text>().text}");
        //Debug.Log($"아이템 가격 : {transform.GetChild(3).GetComponent<TMPro.TMP_Text>().text}");
    }
    #endregion

    #region UI세팅

    public void SetCheckUI(ShopType shopType)
    {
        if(ShopType.Shop == shopType)
        {
            DataManager.Instance.OpenCheckItemUI(transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
        } else if(ShopType.Skill == shopType)
        {
            DataManager.Instance.OpenCheckSkillUI(this.name);
        } else
        {

        }
    }

    public void BuyItem()
    {
        DataManager.Instance.uiManager.CloseItemCheckWindow();
        DataManager.Instance.PerformAction(transform.parent.GetChild(0).GetChild(2).
            GetComponent<TMPro.TMP_Text>().text);
    }

    public void SkillBuyItem()
    {
        DataManager.Instance.uiManager.CloseSkillCheckWindow();
        DataManager.Instance.PerformAction(ParseSkillName(transform.parent.GetChild(0).GetChild(2).
            GetComponent<TMPro.TMP_Text>().text));
    }

    public string ParseSkillName(string itemName)
    {
        SkillName tempSkillName = default;

        // 찾을 문자열 "LV"의 인덱스를 찾습니다.
        int lvIndex = itemName.IndexOf("LV");

        // "LV"를 찾았을 경우에만 처리합니다.
        if (lvIndex != -1)
        {
            // "LV" 이전의 문자열을 가져옵니다.
            string nameBeforeLV = itemName.Substring(0, lvIndex).Trim();

            // 스킬 이름을 설정합니다.
            switch (nameBeforeLV)
            {
                case "생명력 증가":
                    tempSkillName = SkillName.IncreaseLifeBtn;
                    break;
                case "체력 증가":
                    tempSkillName = SkillName.IncreaseHealthBtn;
                    break;
                case "건강 재생 증가":
                    tempSkillName = SkillName.IncreaseBodyRecoveryBtn;
                    break;
                case "골드 획득량 증가":
                    tempSkillName = SkillName.IncreaseGoldProsperityBtn;
                    break;
                default:
                    break;
            }

            Debug.Log(nameBeforeLV + "값임");
            return tempSkillName.ToString();
        }
        else
        {
            // "LV"를 찾지 못했을 경우 원래 문자열을 그대로 반환합니다.
            return itemName;
        }
    }

    #endregion
}
