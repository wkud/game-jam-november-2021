public interface ISkill
{
  void Use(IEntity[] targets);

  string Description
  {
    get;
  }

  int Cooldown
  {
    get;
  }
}
