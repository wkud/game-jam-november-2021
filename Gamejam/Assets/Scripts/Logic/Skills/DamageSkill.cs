public class DamageSkill : Skill
{
    public override void Use(Entity user, Entity[] targets)
    {
        var baseDamage = Data.Power + user.Stats.AttackModifier;
        var damage = GetMultipliedPowerOnCritical(user.Stats.CritChance, baseDamage);

        foreach (var target in targets)
        {
            target.TakeDamage(damage);
        }
    }
}
