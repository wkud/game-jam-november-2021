public interface ISkill
{
  SkillData Data { get; }

  SkillTargetCount TargetCount { get; }
  Bond TargetBound { get; }

  void Use(IEntity user, IEntity[] targets);

  string Description { get; }

  int MaxCooldown { get; }

  int CurrentCooldown { get; set; }

}
