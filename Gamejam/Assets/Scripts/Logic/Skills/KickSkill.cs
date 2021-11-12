public class KickSkill : ISkill
{
  private int _power;
  private const string _DESCRIPTION = "Kick him in the butt";

  public KickSkill(int power)
  {
    this._power = power;
  }

  public void Use(IEntity target)
  {
    target.DealDamage(this._power);
  }

  public string Description
  {
    get => _DESCRIPTION;
  }
}