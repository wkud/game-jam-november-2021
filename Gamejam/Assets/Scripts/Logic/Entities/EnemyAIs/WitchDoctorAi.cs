using System;
using System.Linq;

public class WitchDoctorAi : EnemyAi
{
    private const string SPECIAL_SKILL_NAME = "Heal";

    public override void MakeMove(Entity user, Entity[] allies, Entity[] enemies, Skill[] availableSkills)
    {
        bool isHealNeeded = CheckIfHealIsNeeded(enemies);
        Skill selectedSkill = isHealNeeded
        ? availableSkills.FirstOrDefault(s => s.Data.Name == SPECIAL_SKILL_NAME)
        : this.SelectRandomSkill(availableSkills.Where(x => x.Data.Name != SPECIAL_SKILL_NAME).ToArray(), allies, enemies);
        // if skill bond is ally, return Enemy's allies - enemies

        Entity[] targets = isHealNeeded ? this.GetEnemyWithLowestHp(enemies) : this.GetkillTarget(selectedSkill, allies, enemies);

        selectedSkill.Use(user, targets);
    }

    private Entity[] GetEnemyWithLowestHp(Entity[] enemies)
    {
        return new Entity[] { enemies.OrderBy(e => (float)e.Stats.CurrentHp / (float)e.Stats.MaxHp).First() };
    }

    private bool CheckIfHealIsNeeded(Entity[] enemies)
    {
        int CurrentHpSum = enemies.Select(e => e.Stats.CurrentHp).Sum();
        int MaxHpSum = enemies.Select(e => e.Stats.MaxHp).Sum();

        return ((float)CurrentHpSum / (float)MaxHpSum) <= 0.5;
    }

}
