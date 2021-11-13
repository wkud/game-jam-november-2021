using UnityEngine;

public class Player : IEntity
{
    [SerializeField] private EntityStats _stats;

    public int Initiative { get => _stats.Initiative; set => _stats.Initiative = value; }

    public EntityStats Stats { get => _stats; }

    public Player(EntityStats stats)
    {
        this._stats = stats;
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
        return this._stats.Skills[skillIndex].TargetCount == SkillTargetCount.One;
    }

    public Bond GetSkillTargetBond(int skillIndex)
    {
        return this._stats.Skills[skillIndex].TargetBound;
    }
    

}
