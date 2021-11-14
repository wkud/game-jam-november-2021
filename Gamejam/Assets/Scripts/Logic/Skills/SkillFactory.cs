using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillFactory 
{
    private static Dictionary<SkillName, Type> _skillTypes = new Dictionary<SkillName, Type>()
    {
        { SkillName.DropKick, typeof(DamageSkill) },
        { SkillName.KickSkill, typeof(DamageSkill) },
        { SkillName.Punch, typeof(DamageSkill) },
        { SkillName.Uppercut, typeof(DamageSkill) },
        { SkillName.SwordStrike, typeof(DamageSkill) },
        { SkillName.ExposingStrike, typeof(ExposingStrikeSkill) },
        { SkillName.ShieldBash, typeof(ShieldBashSkill) },
        { SkillName.Bowshot, typeof(DamageSkill) },
        { SkillName.FireArrow, typeof(DamageSkill) },
        { SkillName.Multishot, typeof(DamageSkill) },
        { SkillName.FirstAid, typeof(HealSkill) },
        { SkillName.Heal, typeof(HealSkill) },
        { SkillName.GroupHeal, typeof(HealSkill) },
    };

    public static Skill CreateSkill(SkillData data)
    {
        var skillClass = _skillTypes[data.Identifier];
        var instance = Activator.CreateInstance(skillClass, data);
        return (Skill)instance;
    }
}
