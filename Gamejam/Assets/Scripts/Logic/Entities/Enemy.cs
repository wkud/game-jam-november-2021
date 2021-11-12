public class Enemy : IEntity
{
  protected int _hp = 30;

  public void DealDamage(int damage)
  {
    _hp -= damage;
  }

}
