public class ExposingStrikeSkill : Skill
{
    public override void Use(Entity user, Entity[] targets)
    {
        foreach (var target in targets)
        {
            target.TakeDamage(Data.Power);
            target.Stats.Defence -= Data.Power;
        }
    }
}
