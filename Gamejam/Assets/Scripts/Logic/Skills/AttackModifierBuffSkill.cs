
public class AttackModifierBuffSkill : Skill
{
    public override void Use(Entity user, Entity[] targets)
    {
        var power = GetMultipliedPowerOnCritical(user.Stats.CritChance, Data.Power);

        foreach (var target in targets)
        {
            target.Stats.AttackModifier += power;
        }
    }
}