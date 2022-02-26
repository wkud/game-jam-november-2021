using System;
using System.Collections.Generic;
using System.Linq;

public struct EnemyAiMoveDecission
{
    public Skill Skill { get; set; }
    public Entity[] Targets { get; set; }
    
    public EnemyAiMoveDecission(Skill skill, Entity[] targets)
    {
        Skill = skill;
        Targets = targets;
    }
}

public class EnemyAi
{
    private Random _random = new Random();

    /// <summary>
    /// Enemy decides what to do in this method and the a specific skill is invoked. This method takes entities as parameters (enemy can decide what to do based on entities' state, such as hp)
    /// </summary>
    /// <param name="availableSkills">Collection of available spells that enemy can use</param>
    public virtual EnemyAiMoveDecission MakeMove(Entity[] allies, Entity[] enemies, Skill[] availableSkills)
    {
        Skill selectedSkill = SelectRandomSkill(availableSkills, allies, enemies);

        // if skill bond is ally, return Enemy's allies - enemies
        Entity[] targets = GetSkillTarget(selectedSkill, allies, enemies);

        return new EnemyAiMoveDecission(selectedSkill, targets);
    }

    protected Skill SelectRandomSkill(IEnumerable<Skill> availableSkills, Entity[] allies, Entity[] enemies)
    {
        bool isSingleMemberInTargetParty(Skill skill)
        {
            var targetParty = skill.Data.TargetBond == Bond.Ally ? allies : enemies;
            return targetParty.Count() == 1;
        }

        // Prefer single-target skills when target group has 1 entity
        Skill[] prioritySkills = availableSkills.Where(
            x => x.Data.TargetCount == SkillTargetCount.One 
                && isSingleMemberInTargetParty(x)
            ).ToArray();

        Skill[] skillsToPick = prioritySkills.Length > 0 ? prioritySkills : availableSkills.ToArray();

        int randomSkillId = _random.Next(0, skillsToPick.Length);
        return skillsToPick[randomSkillId];
    }

    protected Entity[] GetSkillTarget(Skill skill, Entity[] allies, Entity[] enemies)
    {
        Entity[] targetGroup = skill.Data.TargetBond == Bond.Ally ? enemies : allies;

        Entity[] targets = skill.Data.TargetCount == SkillTargetCount.One
        ? GetOneSkillTarget(targetGroup)
        : targetGroup;

        return targets;
    }

    private Entity[] GetOneSkillTarget(Entity[] targetGroup)
    {
        int randomTargetId = _random.Next(0, targetGroup.Length);
        Entity target = targetGroup[randomTargetId];


        int[] threats = targetGroup.Select(e => e.Stats.Threat).ToArray();
        float threatSum = threats.Sum();

        float[] threatChances = threats.Select(e => e * 100 / threatSum).ToArray();

        float randomChance = _random.Next(0, 100);
        int selectedId = 0;

        for (int i = 0; i < threatChances.Length && randomChance > 0; i++)
        {
            randomChance -= threatChances[i];
            selectedId = i;
        }

        return new Entity[] { targetGroup[selectedId] };
    }

}
