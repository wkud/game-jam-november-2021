

public class Player : IEntity
{
  private ISkill[] _skills = new ISkill[3];
  private IBuff[] _buffs = new IBuff[2];
  private IDebuff[] _debuffs = new IDebuff[2];
  private int _hp = 30;
  private int _initiative = 10;
  private int _attackModifier = 10;
  private int _defence = 10;
  private float _critChance = 10;

  public int Initiative { get => _initiative; set => _initiative = value; }
  public int Hp { get => _initiative; set => _initiative = value; }

  public Player(int hp, int initiative)
  {
    this._hp = hp;
    this._initiative = initiative;
  }

  public void DealDamage(int damage)
  {
    this._hp -= damage;
  }

  public void SetSkill(int slotNumber, ISkill skill)
  {
    this._skills[slotNumber] = skill;
  }

  public void UseSkill(int slotNumber, IEntity target)
  {
    this._skills[slotNumber].Use(target);
  }

  public void SetBuff(int slotNumber, IBuff buff)
  {
    this._buffs[slotNumber]?.Deactivate(this);

    this._buffs[slotNumber] = buff;
    this._buffs[slotNumber].Activate(this);
  }

  public void SetDebuff(int slotNumber, IDebuff debuff)
  {
    this._debuffs[slotNumber]?.Deactivate(this);

    this._debuffs[slotNumber] = debuff;
    this._debuffs[slotNumber].Activate(this);

  }

}
