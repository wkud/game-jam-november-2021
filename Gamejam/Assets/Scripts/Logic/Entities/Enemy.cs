using UnityEngine;
using System.Linq;

public class Enemy : Entity
{
    private IEnemyAi _ai;

    //TODO add states and 
    //TODO add cooldown counter



    public Enemy(EntityStats initialStats, IEnemyAi ai) : base(initialStats)
    {
        _ai = ai;
    }


    public void MakeMove(IFightStateHolder fightState)
    {
        var availableSkills = _stats.Skills.Where(s => s.Data.CurrentCooldown <= 0).ToArray();
        _ai.MakeMove(fightState.Allies, fightState.Enemies, availableSkills);
    }

}
