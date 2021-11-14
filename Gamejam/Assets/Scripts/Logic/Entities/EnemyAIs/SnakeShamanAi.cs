using System;
using System.Linq;

public class SnakeShamanAi : EnemyAi
{
    private const string SPECIAL_SKILL_NAME = "SummonDoctor";
    private const int MAX_ENEMY_COUNT = 4;

    public override void MakeMove(Entity user, Entity[] allies, Entity[] enemies, Skill[] availableSkills)
    {
        Skill selectedSkill = enemies.Count() < MAX_ENEMY_COUNT
        ? availableSkills.FirstOrDefault(s => s.Data.Name == SPECIAL_SKILL_NAME)
        : this.SelectRandomSkill(availableSkills.Where(x => x.Data.Name != SPECIAL_SKILL_NAME).ToArray(), allies, enemies);

        Entity[] targets = this.GetkillTarget(selectedSkill, allies, enemies);

        selectedSkill.Use(user, targets);
    }

}
