public class ShieldBashSkill : Skill
{
    public override void Use(Entity user, Entity[] targets)
    {
        var baseDamage = Data.Power + user.Stats.Defence;
        var damage = GetMultipliedPowerOnCritical(user.Stats.CritChance, baseDamage);

        foreach (var target in targets)
        {
            target.TakeDamage(damage);
        }
    }
}
