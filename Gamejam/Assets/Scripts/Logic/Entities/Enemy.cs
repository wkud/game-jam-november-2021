using UnityEngine;
using System.Linq;

public class Enemy : Entity
{
    private IEnemyAi _ai;

    //TODO add states and 
    //TODO add cooldown counter



    public Enemy(IEnemyAi ai, EntityStats stats)
    {
        _ai = ai;
        _stats = stats;
    }


    public void MakeMove(IFightStateHolder fightState)
    {
        var availableSkills = _stats.Skills.Where(s => s.Data.CurrentCooldown <= 0).ToArray();
        _ai.MakeMove(fightState.Allies, fightState.Enemies, availableSkills);
    }

}
