public class HealSkill : Skill
{
    public override void Use(Entity user, Entity[] targets)
    {
        var healingPower = GetMultipliedPowerOnCritical(user.Stats.CritChance, Data.Power);

        foreach (var target in targets)
        {
            target.Heal(healingPower);
        }
    }
}
