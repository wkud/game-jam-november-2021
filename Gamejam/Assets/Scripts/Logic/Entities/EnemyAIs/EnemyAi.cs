using System;
using System.Linq;

public class EnemyAi
{
    Random random = new Random();

    /// <summary>
    /// Enemy decides what to do in this method and the a specific skill is invoked. This method takes entities as parameters (enemy can decide what to do based on entities' state, such as hp)
    /// </summary>
    /// <param name="players"></param>
    /// <param name="enemies"></param>
    /// <param name="availableSkills">Collection of available spells that enemy can use</param>
    public void MakeMove(Entity user, Entity[] allies, Entity[] enemies, Skill[] availableSkills)
    {
        Skill selectedSkill = this.SelectRandomSkill(availableSkills);
        // if skill bond is ally, return Enemy's allies - enemies

        Entity[] targets = GetkillTarget(selectedSkill, allies, enemies);

        selectedSkill.Use(user, targets);
    }

    private Skill SelectRandomSkill(Skill[] availableSkills)
    {
        int randomSkillId = random.Next(0, availableSkills.Length);
        return availableSkills[randomSkillId];
    }

    private Entity[] GetkillTarget(Skill skill, Entity[] allies, Entity[] enemies)
    {
        Entity[] targetGroup = skill.Data.TargetBond == Bond.Ally ? enemies : allies;

        Entity[] targets = skill.Data.TargetCount == SkillTargetCount.One
        ? this.GetOneSkillTarget(targetGroup)
        : targetGroup;

        return targets;
    }

    private Entity[] GetOneSkillTarget(Entity[] targetGroup)
    {
        int randomSkillId = random.Next(0, targetGroup.Length);
        Entity target = targetGroup[randomSkillId];

        float randomChance = random.Next(0, 100);

        float[] threats = targetGroup.Select(e => e.Stats.Threat).ToArray();
        float threatSum = threats.Sum();

        float[] threatChances = threats.Select(e => e * 100 / threatSum).ToArray();

        int selectedId = 0;

        for (int i = 0; i < threatChances.Length && randomChance > 0; i++)
        {
            randomChance -= threatChances[i];
            selectedId = i;
        }

        return new Entity[] { targetGroup[selectedId] };
    }

}
