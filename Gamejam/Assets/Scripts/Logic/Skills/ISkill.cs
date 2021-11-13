public interface ISkill
{
    public SkillData Data { get; }

    public SkillTargetCount TargetCount { get; } 
    public Bond TargetBound { get; }

    void Use(IEntity user, IEntity[] targets);

    string Description { get; }

    int MaxCooldown { get; }

    int CurrentCooldown { get; set; }

}
