using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { 생명력증가, 체력증가, 체력재생증가, 골드획득량증가}
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
        int result = default; //결과값 리턴이 필요할경우 PlayBool을 활용

        Debug.Log($"스킬 실행됬다.{type}");
        switch(type)
        {
            case nameof(SkillType.생명력증가):
                result = UpgradeLife();
                switch(result)
                {
                    case -1:
                        uiManager.PrintError("골드가 부족합니다.");
                        break;
                    case -2:
                        uiManager.PrintError("생명력증가 스킬이 MAX레벨 입니다.");
                        break;
                    case 1:
                        uiManager.ChangeTMPText(nameof(SkillType.생명력증가));
                        if (mainData.lifeUpLevel >= mainData.lifeUpMaxLevel)
                        {
                            uiManager.OFFButtons(nameof(SkillType.생명력증가));
                        }
                        break;
                    default:
                        break;
                }
                break;
            case nameof(SkillType.체력증가):
                result = UpgradeHealth();
                switch (result)
                {
                    case -1:
                        uiManager.PrintError("골드가 부족합니다.");
                        break;
                    case -2:
                        uiManager.PrintError("체력증가 스킬이 MAX레벨 입니다.");
                        break;
                    case 1:
                        uiManager.ChangeTMPText(nameof(SkillType.체력증가));
                        if (mainData.healthUpLevel >= mainData.healthUpMaxLevel)
                        {
                            uiManager.OFFButtons(nameof(SkillType.체력증가));
                        }
                        break;
                    default:
                        break;
                }
                break;
            case nameof(SkillType.체력재생증가):
                result = UpgradeHealthRecovery();
                switch (result)
                {
                    case -1:
                        uiManager.PrintError("골드가 부족합니다.");
                        break;
                    case -2:
                        uiManager.PrintError("체력재생증가 스킬이 MAX레벨 입니다.");
                        break;
                    case 1:
                        uiManager.ChangeTMPText(nameof(SkillType.체력재생증가));
                        if (mainData.healthRepairLevel >= mainData.healthRepairMaxLevel)
                        {
                            uiManager.OFFButtons(nameof(SkillType.체력재생증가));
                        }
                        break;
                    default:
                        break;
                }
                break;
            case nameof(SkillType.골드획득량증가):
                result = UpgradeAdditionalGold();
                switch (result)
                {
                    case -1:
                        uiManager.PrintError("골드가 부족합니다.");
                        break;
                    case -2:
                        uiManager.PrintError("골드획득량증가 스킬이 MAX레벨 입니다.");
                        break;
                    case 1:
                        uiManager.ChangeTMPText(nameof(SkillType.골드획득량증가));
                        if (mainData.goldUpLevel >= mainData.goldUpMaxLevel)
                        {
                            uiManager.OFFButtons(nameof(SkillType.골드획득량증가));
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