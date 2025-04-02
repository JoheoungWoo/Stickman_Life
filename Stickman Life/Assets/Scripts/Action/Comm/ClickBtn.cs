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

    #region ����� ���
    //�̸��� ���� �ҷ����� �׽�Ʈ
    public void LoadItemTest()
    {
        Debug.Log($"������ �̸� : {gameObject?.name}");
        Debug.Log($"������ �̸� : {transform?.GetChild(2).GetComponent<TMPro.TMP_Text>().text}");
        //Debug.Log($"������ ���� : {transform.GetChild(3).GetComponent<TMPro.TMP_Text>().text}");
    }
    #endregion

    #region UI����

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

        // ã�� ���ڿ� "LV"�� �ε����� ã���ϴ�.
        int lvIndex = itemName.IndexOf("LV");

        // "LV"�� ã���� ��쿡�� ó���մϴ�.
        if (lvIndex != -1)
        {
            // "LV" ������ ���ڿ��� �����ɴϴ�.
            string nameBeforeLV = itemName.Substring(0, lvIndex).Trim();

            // ��ų �̸��� �����մϴ�.
            switch (nameBeforeLV)
            {
                case "����� ����":
                    tempSkillName = SkillName.IncreaseLifeBtn;
                    break;
                case "ü�� ����":
                    tempSkillName = SkillName.IncreaseHealthBtn;
                    break;
                case "�ǰ� ��� ����":
                    tempSkillName = SkillName.IncreaseBodyRecoveryBtn;
                    break;
                case "��� ȹ�淮 ����":
                    tempSkillName = SkillName.IncreaseGoldProsperityBtn;
                    break;
                default:
                    break;
            }

            Debug.Log(nameBeforeLV + "����");
            return tempSkillName.ToString();
        }
        else
        {
            // "LV"�� ã�� ������ ��� ���� ���ڿ��� �״�� ��ȯ�մϴ�.
            return itemName;
        }
    }

    #endregion
}
