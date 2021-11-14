using System;
using System.Linq;

// Warning, has state!
public class SpiritWarriorAi : EnemyAi
{
    private int lastEntityCount;

    public override void MakeMove(Entity user, Entity[] allies, Entity[] enemies, Skill[] availableSkills)
    {
        Skill selectedSkill = this.SelectRandomSkill(availableSkills, allies, enemies);

        int currentEntityCount = allies.Count() + enemies.Count();
        if (this.lastEntityCount > currentEntityCount)
        {
            // Logic that happens when someone was killed
            int power = user.Stats.Skills.FirstOrDefault(s => s.Data.Name == "SpiritWarriorBuffSkill").Data.Power;
            user.Stats.MaxHp += power * 3;
            user.Stats.CurrentHp += power * 3;
            user.Stats.AttackModifier += power;

            this.lastEntityCount = currentEntityCount;
        }

        if (enemies.Count() == 1)
        {
            selectedSkill = availableSkills.FirstOrDefault(s => s.Data.Name == "SummonDoctor") ?? selectedSkill;
        }

        Entity[] targets = this.GetkillTarget(selectedSkill, allies, enemies);

        selectedSkill.Use(user, targets);
    }

    public override void OnCreate(Entity user, Entity[] allies, Entity[] enemies, Skill[] availableSkills)
    {
        this.lastEntityCount = allies.Count() + enemies.Count();
    }

}
