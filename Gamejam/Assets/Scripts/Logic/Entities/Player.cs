using UnityEngine;
using System;
using UnityEngine.Events;

public class Player : Entity
{
    public UnityEvent<StatName> OnStatChanged = new UnityEvent<StatName>();

    public Player(EntityStats initialStats) : base(initialStats) { }

    public void SetSkill(int slotNumber, Skill skill)
    {
        _stats.Skills[slotNumber] = skill;
    }

    public string GetSkillDescription(int slotNumber)
    {
        var skillData = _stats.Skills[slotNumber];
        return skillData?.GetTooltipDescription();
    }

    public string GetStatDescription(StatName statName)
    {
        switch (statName)
        {
            case StatName.Attack:
                return "Attack: " + _stats.AttackModifier;
            case StatName.CritChance:
                return "Crit chance: " + _stats.CritChance;
            case StatName.Defence:
                return "Defence: " + _stats.Defence;
            case StatName.MaxHp:
                return $"Hp: {_stats.CurrentHp}/{_stats.MaxHp}";
            case StatName.Initiative:
                return "Initiative: " + _stats.Initiative;
            case StatName.Threat:
                return "Threat: " + _stats.Threat;
            default:
                return "";
        }
    }

    public void UseSkill(int slotNumber, Entity[] targets)
    {
        _stats.Skills[slotNumber].Use(this, targets);
    }

    public void AddStat(StatName statName, int statIncrease)
    {
        switch (statName)
        {
            case StatName.MaxHp:
                Stats.MaxHp = Math.Max(Stats.MaxHp + statIncrease, 0);
                break;
            case StatName.CurrentHp:
                Stats.CurrentHp = Math.Max(Stats.CurrentHp + statIncrease, 0);
                break;
            case StatName.Initiative:
                Stats.Initiative = Math.Max(Stats.Initiative + statIncrease, 0);
                break;
            case StatName.Defence:
                Stats.Defence = Math.Max(Stats.Defence + statIncrease, 0);
                break;
            case StatName.CritChance:
                Stats.CritChance = Math.Max(Stats.CritChance + statIncrease, 0);
                break;
            case StatName.Attack:
                Stats.AttackModifier = Math.Max(Stats.AttackModifier + statIncrease, 0);
                break;
            case StatName.Threat:
                Stats.Threat = Math.Max(Stats.Threat + statIncrease, 0);
                break;
        }
        OnStatChanged.Invoke(statName);
    }

    public int GetStat(StatName statName)
    {
        switch (statName)
        {
            case StatName.MaxHp:
                return Stats.MaxHp;
            case StatName.CurrentHp:
                return Stats.CurrentHp;
            case StatName.Initiative:
                return Stats.Initiative;
            case StatName.Defence:
                return Stats.Defence;
            case StatName.CritChance:
                return Stats.CritChance;
            case StatName.Attack:
                return Stats.AttackModifier;
            case StatName.Threat:
                return Stats.Threat;
            default:
                return -1;
        }
    }

    public Skill GetSkill(int slotNumber)
    {
        return _stats.Skills[slotNumber];
    }

    public bool IsSkillSingleTarget(int skillIndex)
    {
        var skill = _stats.Skills[skillIndex];
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
        return _stats.Skills[skillIndex].Data.TargetBond;
    }

    public void OnTurnStart()
    {
        foreach (StateController state in _stats.States)
        {
            state.OnTurnStart(_stats);
        }
    }

    public void OnTurnEnd()
    {
        foreach (StateController state in _stats.States)
        {
            state.OnTurnEnd(_stats);
            if (state.TurnsLeft <= 0) _stats.States.Remove(state);
        }
    }
}
