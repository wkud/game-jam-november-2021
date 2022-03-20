using System;
using System.Collections.Generic;

public static class SkillFactory 
{
    private static Dictionary<SkillName, Type> _skillTypes = new Dictionary<SkillName, Type>()
    {
        // player skills
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

        // enemy skills
        { SkillName.BloodRain, typeof(DamageSkill) },
        { SkillName.BloodStrike, typeof(DamageSkill) },
        { SkillName.DeathMist, typeof(DamageSkill) },
        { SkillName.DeathStrike, typeof(DamageSkill) },
        { SkillName.JaguarStrike, typeof(DamageSkill) },
        { SkillName.SacrificialRite, typeof(AttackModifierBuffSkill) },
        { SkillName.SnakeBite, typeof(DamageSkill) },
        { SkillName.SnakeSlap, typeof(DamageSkill) },
        { SkillName.SpiritWarriorBuffSkill, typeof(SpiritWarriorBuffSkill) },
        { SkillName.SummonDoctor, typeof(SummonSkill<WitchDoctor>) },
        { SkillName.SummonSnake, typeof(SummonSkill<Snake>) },
        { SkillName.WitchDoctorBuff, typeof(AttackModifierBuffSkill) },
        { SkillName.WitchDoctorHeal, typeof(HealSkill) },
    };

    public static Skill CreateSkill(SkillData data)
    {
        if (data is null)
        {
            return null;
        }

        var skillClass = _skillTypes[data.Identifier];
        var instance = Activator.CreateInstance(skillClass);

        var skillInstance = (Skill)instance;
        skillInstance.SetData(data);
        
        return skillInstance;
    }
}
