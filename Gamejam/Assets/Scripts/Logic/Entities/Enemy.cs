public class Enemy : IEntity
{
  protected int _hp = 30;
  protected int _initiative = 10;

  public Enemy(int hp, int initiative)
  {
    this._hp = hp;
    this._initiative = initiative;
  }

  public void DealDamage(int damage)
  {
    this._hp -= damage;
  }

  public int Initiative { get => _initiative; set => _initiative = value; }
  public int Hp { get => _initiative; set => _initiative = value; }

}
