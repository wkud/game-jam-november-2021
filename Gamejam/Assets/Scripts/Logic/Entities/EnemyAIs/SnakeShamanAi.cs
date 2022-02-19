using System.Linq;

public class SnakeShamanAi : EnemyAi
{
    private const SkillName SPECIAL_SKILL_ID = SkillName.SummonSnake;
    private const int MAX_ENEMY_COUNT = 4;

    public override EnemyAiMoveDecission MakeMove(Entity[] allies, Entity[] enemies, Skill[] availableSkills)
    {
        var summonSkill = availableSkills.FirstOrDefault(s => s.Data.Identifier == SPECIAL_SKILL_ID);
        var regularSkills = availableSkills.Where(s => s != summonSkill);

        Skill selectedSkill = enemies.Count() < MAX_ENEMY_COUNT
        ? summonSkill
        : SelectRandomSkill(regularSkills, allies, enemies);

        Entity[] targets = GetSkillTarget(selectedSkill, allies, enemies);

        return new EnemyAiMoveDecission(selectedSkill, targets);
    }

}
