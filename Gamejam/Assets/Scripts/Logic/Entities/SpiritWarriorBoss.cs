using System.Linq;

public class SpiritWarriorBoss : Enemy
{
    public SpiritWarriorBoss(EntityStats initialStats, EnemyAi ai) : base(initialStats, ai)
    {

    }

    public void UsePassiveSkill()
    {
        var buffSkill = _stats.Skills.FirstOrDefault(s => s.Data.Identifier == SkillName.SpiritWarriorBuffSkill);

        var targets = new Entity[] { this };
        buffSkill.Use(this, targets);
    }
}