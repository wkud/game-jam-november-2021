using UnityEngine;
using System;

public class Player : IEntity
{
    [SerializeField] private EntityStats _stats;

    public EntityStats Stats { get => _stats; }

    public Player(EntityStats stats)
    {
        this._stats = stats;
    }

    public void TakeDamage(int damage)
    {
        this._stats.Hp -= damage;
    }

    public void SetSkill(int slotNumber, Skill skill)
    {
        this._stats.Skills[slotNumber] = skill;
    }

    public string GetSkillDescription(int slotNumber)
    {
        return this._stats.Skills[slotNumber]?.Data?.Description;
    }

    public string GetStatDescription(StatName statName)
    {
        switch (statName)
        {
            case StatName.AttackModifier:
                return "AttackModifier: " + _stats.AttackModifier;
            case StatName.CritChance:
                return "CritChance: " + _stats.CritChance;
            case StatName.Defence:
                return "Defence: " + _stats.Defence;
            case StatName.Hp:
                return "Hp: " + _stats.Hp;
            case StatName.Initiative:
                return "Initiative: " + _stats.Initiative;
            case StatName.Threat:
                return "Threat: " + _stats.Threat;
            default:
                return "";
        }
    }

    public void UseSkill(int slotNumber, IEntity[] targets)
    {
        this._stats.Skills[slotNumber].Use(this, targets);
    }

    public bool IsSkillSingleTarget(int skillIndex)
    {
        var skill = this._stats.Skills[skillIndex];
        try
        {
            return skill.Data.TargetCount == SkillTargetCount.One;
        }
        catch (NullReferenceException)
        {
            Debug.LogError("There is no skill in that slot");
            return false;
        }
    }

    public Bond GetSkillTargetBond(int skillIndex)
    {
        return this._stats.Skills[skillIndex].Data.TargetBond;
    }

    public void OnTurnStart()
    {
        foreach (StateController state in this._stats.States)
        {
            state.OnTurnStart(this._stats);
        }
    }

    public void OnTurnEnd()
    {
        foreach (StateController state in this._stats.States)
        {
            state.OnTurnEnd(this._stats);
            if (state.TurnsLeft <= 0) this._stats.States.Remove(state);
        }
    }


}
