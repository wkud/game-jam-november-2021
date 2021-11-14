using UnityEngine;
using System.Linq;

public class Enemy : IEntity
{
    private IEnemyAi _ai;
    private EntityStats _stats;

    //TODO add states and 
    //TODO add cooldown counter


    public EntityStats Stats { get => _stats; }

    public Enemy(IEnemyAi ai, EntityStats stats)
    {
        _ai = ai;
        _stats = stats;
    }

    public void TakeDamage(int damage)
    {
        this._stats.CurrentHp -= damage;
    }

    public void MakeMove(IFightStateHolder fightState)
    {
        var availableSkills = _stats.Skills.Where(s => s.Data.CurrentCooldown <= 0).ToArray();
        _ai.MakeMove(fightState.Allies, fightState.Enemies, availableSkills);
    }

}
