public class ExposingStrikeSkill : Skill
{
    public override void Use(Entity user, Entity[] targets)
    {
        var baseDamage = Data.Power + user.Stats.AttackModifier;

        var damage = GetMultipliedPowerOnCritical(user.Stats.CritChance, baseDamage);
        var deffenceDebuff = GetMultipliedPowerOnCritical(user.Stats.CritChance, Data.Power);

        foreach (var target in targets)
        {
            target.TakeDamage(damage);
            target.Stats.Defence -= deffenceDebuff;
        }
    }
}
