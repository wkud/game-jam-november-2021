public class DamageSkill : Skill
{
    public override void Use(Entity user, Entity[] targets)
    {
        foreach (var target in targets)
        {
            target.TakeDamage(this.Data.Power + user.Stats.AttackModifier);
        }
    }
}
