using System.Linq;

public class Enemy : Entity
{
    private EnemyAi _ai;

    //TODO add states
    //TODO add cooldown counter

    public Enemy(EntityStats initialStats, EnemyAi ai) : base(initialStats)
    {
        _ai = ai;
    }

    public void MakeMove(IFightStateHolder fightState)
    {
        var availableSkills = _stats.Skills.Where(s => (s?.Data?.CurrentCooldown ?? int.MaxValue) <= 0).ToArray();
        
        var decission =_ai.MakeMove(fightState.Allies, fightState.Enemies, availableSkills);
        decission.Skill.Use(this, decission.Targets);

    }
}
