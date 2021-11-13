using UnityEngine;

public class Player : IEntity
{
  [SerializeField] private PlayerStats _stats;

  public int Initiative { get => _stats.Initiative; set => _stats.Initiative = value; }
  public int Hp { get => _stats.Hp; set => _stats.Hp = value; }

  public Player(PlayerStats stats)
  {
    this._stats = stats;
  }

  public void DealDamage(int damage)
  {
    this.Hp -= damage;
  }

  public void SetSkill(int slotNumber, ISkill skill)
  {
    this._stats.Skills[slotNumber] = skill;
  }

  public void UseSkill(int slotNumber, IEntity[] targets)
  {
    this._stats.Skills[slotNumber].Use(targets);
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

}
