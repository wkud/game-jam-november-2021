using UnityEngine;
using System;

public class Player : IEntity
{
    [SerializeField] private EntityStats _initialStats;
    private EntityStats _stats;

    public EntityStats Stats { get => _stats; }

    public Player(EntityStats stats)
    {
        this._initialStats = stats;
        _stats = _initialStats.GetClone();
    }

    public void TakeDamage(int damage)
    {
        this._stats.CurrentHp -= damage;
    }


    public void SetBuff(int slotNumber, IBuff buff)
    {
        this._stats.Buffs[slotNumber]?.Deactivate(this);

        this._stats.Buffs[slotNumber] = buff;
        this._stats.Buffs[slotNumber].Activate(this);
    }

    public void SetDebuff(int slotNumber, IDebuff debuff)
    {
        this._stats.Debuffs[slotNumber]?.Deactivate(this);

        this._stats.Debuffs[slotNumber] = debuff;
        this._stats.Debuffs[slotNumber].Activate(this);

    }


    public void SetSkill(int slotNumber, ISkill skill)
    {
        this._stats.Skills[slotNumber] = skill;
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
            throw new ArgumentOutOfRangeException("There is no skill in that slot");
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
