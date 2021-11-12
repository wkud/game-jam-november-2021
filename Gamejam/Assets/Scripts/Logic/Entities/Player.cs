

public class Player : IEntity
{
  protected UseSpell[] _spells = new ISpell[3];
  protected IBuff[] _buffs = new IBuff[2];
  protected IDebuff[] _debuffs = new IDebuff[2];
  protected int _hp = 30;

  public void DealDamage(int damage)
  {
    this._hp -= damage;
  }

  public void UseSpell(int slotNumber, IEntity target)
  {
    this._spells[slotNumber].Use(target);
  }

  public void SetBuff(int slotNumber, IBuff buff)
  {
    if (this._buffs[slotNumber])
    {
      this._buffs[slotNumber].Deactivate(this);
    }
    this._buffs[slotNumber] = buff;
    this._buffs[slotNumber].Activate(this);
  }

  public void SetDebuff(int slotNumber, IDebuff debuff)
  {
    if (this._debuffs[slotNumber])
    {
      this._debuffs[slotNumber].Deactivate(this);
    }
    this._debuffs[slotNumber] = debuff;
    this._debuffs[slotNumber].Activate(this);

  }

}
