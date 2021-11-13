using UnityEngine;

public class KickSkill : ISkill
{
  [SerializeField] private SkillData _data;

  public string Description
  {
    get => _data.Description;
  }

  public int Cooldown
  {
    get => _data.Cooldown;
  }

  public KickSkill(int power, int cooldown)
  {
    this._data.Power = power;
    this._data.Cooldown = cooldown;
  }

  public void Use(IEntity[] targets)
  {
    targets[0]?.DealDamage(this._data.Power);
  }

}