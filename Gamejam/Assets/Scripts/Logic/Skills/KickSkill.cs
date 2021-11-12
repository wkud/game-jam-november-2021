public class KickSkill : ISkill
{
  private int _power;
  private int _cooldown;
  private const string _DESCRIPTION = "Kick him in the butt";

  public string Description
  {
    get => _DESCRIPTION;
  }

  public int Cooldown
  {
    get => _cooldown;
  }

  public KickSkill(int power, int cooldown)
  {
    this._power = power;
    this._cooldown = cooldown;
  }

  public void Use(IEntity[] targets)
  {
    targets[0]?.DealDamage(this._power);
  }

}