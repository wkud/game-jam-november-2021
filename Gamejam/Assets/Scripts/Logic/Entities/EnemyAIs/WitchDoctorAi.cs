using System.Linq;

public class WitchDoctorAi : EnemyAi
{
    private const SkillName SPECIAL_SKILL_ID = SkillName.WitchDoctorHeal;

    public override EnemyAiMoveDecission MakeMove(Entity[] allies, Entity[] enemies, Skill[] availableSkills)
    {
        var specialSkill = availableSkills.FirstOrDefault(s => s.Data.Identifier == SPECIAL_SKILL_ID);
        var regularSkills = availableSkills.Where(s => s != specialSkill);

        bool isHealNeeded = IsHealNeeded(enemies);
        Skill selectedSkill = isHealNeeded
        ? specialSkill
        : SelectRandomSkill(regularSkills, allies, enemies);
        // if skill bond is ally, return Enemy's allies - enemies

        Entity[] targets = isHealNeeded ? this.GetEnemyWithLowestHp(enemies) : GetSkillTarget(selectedSkill, allies, enemies);
        
        return new EnemyAiMoveDecission(selectedSkill, targets);
    }

    private Entity[] GetEnemyWithLowestHp(Entity[] enemies)
    {
        return new Entity[] { enemies.OrderBy(e => e.Stats.CurrentHp / 1.0 * e.Stats.MaxHp).First() };
    }

    private bool IsHealNeeded(Entity[] enemies)
    {
        int CurrentHpSum = enemies.Select(e => e.Stats.CurrentHp).Sum();
        int MaxHpSum = enemies.Select(e => e.Stats.MaxHp).Sum();

        return (CurrentHpSum / 1.0 * MaxHpSum) <= 0.5;
    }

}
