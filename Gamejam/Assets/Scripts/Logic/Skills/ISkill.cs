public interface ISkill
{
    SkillData Data { get; }

    void Use(Entity user, Entity[] targets);
}
