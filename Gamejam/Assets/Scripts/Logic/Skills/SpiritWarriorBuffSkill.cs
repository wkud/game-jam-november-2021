public class SpiritWarriorBuffSkill : Skill
{
    public override void Use(Entity user, Entity[] targets)
    {
        var power = GetMultipliedPowerOnCritical(user.Stats.CritChance, Data.Power);

        user.Stats.MaxHp += power * 3;
        user.Stats.CurrentHp += power * 3;
        user.Stats.AttackModifier += power;
    }
}
