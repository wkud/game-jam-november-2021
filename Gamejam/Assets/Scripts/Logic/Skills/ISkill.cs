public interface ISkill
{
    SkillData Data { get; }

    void Use(IEntity user, IEntity[] targets);
}
