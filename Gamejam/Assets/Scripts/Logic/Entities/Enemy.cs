public class Enemy : IEntity
{
  protected int _hp = 30;

  public void DealDamage(int damage)
  {
    _hp -= damage;
  }

  public int Initiative { get => _initiative; set => _initiative = value; }

}
