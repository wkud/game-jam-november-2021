public abstract class Skill
{
    public SkillData Data { get; private set; }

    public Skill(SkillData data)
    {
        Data = data;
    }

    public Skill() // default constructor for SkillFactory instantiation
    {

    }

    public void SetData(SkillData data)
    {
        Data = data;
    }

    public abstract void Use(Entity user, Entity[] targets);

    public string GetTooltipDescription()
    {
        return Data.Description;
    }

    public string GetTooltipTitle()
    {
        return Data.Name;
    }
}
