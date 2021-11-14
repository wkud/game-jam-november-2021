public abstract class Skill
{
    public SkillData Data { get; private set; }

    public Skill(SkillData data)
    {
        Data = data;
    }

    public abstract void Use(IEntity user, IEntity[] targets);
}
