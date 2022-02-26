public class SpiritWarriorBuffSkill : Skill
{
    public override void Use(Entity user, Entity[] targets)
    {
        user.Stats.MaxHp += Data.Power * 3;
        user.Stats.CurrentHp += Data.Power * 3;
        user.Stats.AttackModifier += Data.Power;
    }
}
