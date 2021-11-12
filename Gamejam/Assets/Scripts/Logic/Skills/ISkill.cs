public interface ISkill
{
  void Use(IEntity target);

  string Description
  {
    get;
  }
}
