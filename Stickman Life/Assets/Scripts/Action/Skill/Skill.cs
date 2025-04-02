using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { ���������, ü������, ü���������, ���ȹ�淮����}
public class Skill : Action
{
    private MainData mainData;
    private UIManager uiManager;

    public Skill(MainData mainData,UIManager uiManager)
    {
        this.mainData = mainData;
        this.uiManager = uiManager;
    }

    public int UpgradeLife()
    {
        return mainData.UpLifeSkill();
    }

    public int UpgradeHealth()
    {
        return mainData.UpHealthSkill();
    }

    public int UpgradeHealthRecovery()
    {
        return mainData.UpHealthRepairSkill();
    }

    public int UpgradeAdditionalGold()
    {
        return mainData.UpGoldSkill();
    }

    public override void Play(string type)
    {
        int result = default; //����� ������ �ʿ��Ұ�� PlayBool�� Ȱ��

        Debug.Log($"��ų �������.{type}");
        switch(type)
        {
            case nameof(SkillType.���������):
                result = UpgradeLife();
                switch(result)
                {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case -2:
                        uiManager.PrintError("��������� ��ų�� MAX���� �Դϴ�.");
                        break;
                    case 1:
                        uiManager.ChangeTMPText(nameof(SkillType.���������));
                        if (mainData.lifeUpLevel >= mainData.lifeUpMaxLevel)
                        {
                            uiManager.OFFButtons(nameof(SkillType.���������));
                        }
                        break;
                    default:
                        break;
                }
                break;
            case nameof(SkillType.ü������):
                result = UpgradeHealth();
                switch (result)
                {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case -2:
                        uiManager.PrintError("ü������ ��ų�� MAX���� �Դϴ�.");
                        break;
                    case 1:
                        uiManager.ChangeTMPText(nameof(SkillType.ü������));
                        if (mainData.healthUpLevel >= mainData.healthUpMaxLevel)
                        {
                            uiManager.OFFButtons(nameof(SkillType.ü������));
                        }
                        break;
                    default:
                        break;
                }
                break;
            case nameof(SkillType.ü���������):
                result = UpgradeHealthRecovery();
                switch (result)
                {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case -2:
                        uiManager.PrintError("ü��������� ��ų�� MAX���� �Դϴ�.");
                        break;
                    case 1:
                        uiManager.ChangeTMPText(nameof(SkillType.ü���������));
                        if (mainData.healthRepairLevel >= mainData.healthRepairMaxLevel)
                        {
                            uiManager.OFFButtons(nameof(SkillType.ü���������));
                        }
                        break;
                    default:
                        break;
                }
                break;
            case nameof(SkillType.���ȹ�淮����):
                result = UpgradeAdditionalGold();
                switch (result)
                {
                    case -1:
                        uiManager.PrintError("��尡 �����մϴ�.");
                        break;
                    case -2:
                        uiManager.PrintError("���ȹ�淮���� ��ų�� MAX���� �Դϴ�.");
                        break;
                    case 1:
                        uiManager.ChangeTMPText(nameof(SkillType.���ȹ�淮����));
                        if (mainData.goldUpLevel >= mainData.goldUpMaxLevel)
                        {
                            uiManager.OFFButtons(nameof(SkillType.���ȹ�淮����));
                        }
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
}