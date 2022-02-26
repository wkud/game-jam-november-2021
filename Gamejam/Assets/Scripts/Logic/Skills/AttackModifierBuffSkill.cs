
public class AttackModifierBuffSkill : Skill
{
    public override void Use(Entity user, Entity[] targets)
    {
        foreach (var target in targets)
        {
            target.Stats.AttackModifier += Data.Power;
        }
    }
}