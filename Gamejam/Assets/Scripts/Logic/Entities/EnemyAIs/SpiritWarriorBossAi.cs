using System.Linq;

public class SpiritWarriorBossAi : EnemyAi
{
    // Spirit Warrior uses his passive buff whenever any entity (player on monster) dies. See SpiritWarrior class

    public override EnemyAiMoveDecission MakeMove(Entity[] allies, Entity[] enemies, Skill[] availableSkills)
    {
        Skill selectedSkill = SelectRandomSkill(availableSkills, allies, enemies);

        int currentEntityCount = allies.Count() + enemies.Count();

        if (enemies.Count() == 1)
        {
            selectedSkill = availableSkills.FirstOrDefault(s => s.Data.Name == "SummonDoctor") ?? selectedSkill;
        }

        Entity[] targets = GetSkillTarget(selectedSkill, allies, enemies);

        return new EnemyAiMoveDecission(selectedSkill, targets);
    }

}
