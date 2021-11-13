using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum StatType
{
    MaxHp,
    Hp,
    Initiative,
    AttackModifier,
    Defence,
    CritChance,
    Threat
}
public class Player : IEntity
{
    [SerializeField] private EntityStats _stats;

    public EntityStats Stats { get => _stats; }

    private int _maxHP; 
    private int _hp = 30;
    private int _initiative = 10;
    private int _attackModifier = 10;
    private int _defence = 10;
    private float _critChance = 10;
    private float _threat = 10;

    public int MaxHp { get => _maxHP; set => _maxHP = value; }
    public int Hp { get => _hp; set => _hp = value; }
    public int Initiative { get => _initiative; set => _initiative = value; }
    public int AttackModifier { get => _attackModifier; set => _attackModifier = value; }
    public int Defence { get => _defence; set => _defence = value; }
    public float CritChance { get => _critChance; set => _critChance = value; }
    public float Threat { get => _threat; set => _threat = value; }

    public Player(EntityStats stats)
    {
        this._stats = stats;
        _hp = stats.Hp;
        _initiative = stats.Initiative;
        _attackModifier = stats.AttackModifier;
        _defence = stats.Defence;
        _critChance = stats.CritChance;
        _threat = stats.Threat;

        _maxHP = _hp;
    }

    public void TakeDamage(int damage)
    {
        this._stats.Hp -= damage;
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
        return this._stats.Skills[skillIndex].Data.TargetCount == SkillTargetCount.One;
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
