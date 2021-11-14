public class ShieldBashSkill : Skill
{
    public override void Use(Entity user, Entity[] targets)
    {
        foreach (var target in targets)
        {
            target.TakeDamage(Data.Power + user.Stats.Defence);
        }
    }
}
