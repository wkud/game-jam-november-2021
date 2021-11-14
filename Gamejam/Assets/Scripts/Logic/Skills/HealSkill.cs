public class HealSkill : Skill
{
    public override void Use(Entity user, Entity[] targets)
    {
        foreach (var target in targets)
        {
            target.TakeDamage(-1 * Data.Power);
        }
    }
}
