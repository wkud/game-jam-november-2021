using UnityEngine;
using System.Linq;

public class Enemy : Entity
{
    private EnemyAi _ai;
    private bool _hasStarted = false;

    //TODO add states and 
    //TODO add cooldown counter



    public Enemy(EntityStats initialStats, EnemyAi ai) : base(initialStats)
    {
        _ai = ai;
    }


    public void MakeMove(IFightStateHolder fightState)
    {
        var availableSkills = _stats.Skills.Where(s => s.Data.CurrentCooldown <= 0).ToArray();
        if (!this._hasStarted)
        {
            this._ai.OnCreate(this, fightState.Allies, fightState.Enemies, availableSkills);
            this._hasStarted = true;
        }
        _ai.MakeMove(this, fightState.Allies, fightState.Enemies, availableSkills);
    }
}
