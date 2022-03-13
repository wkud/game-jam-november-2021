using UnityEngine;

public abstract class Skill
{
    private const int CRITICAL_POWER_MULTIPLIER = 2;

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

    private bool IsCritical(int critChance)
    {
        return Random.Range(0, 100) < critChance; // the bigger the critChance the more propable it is to be critical
    }

    protected int GetMultipliedPowerOnCritical(int critChance, int basePower)
    {
        return IsCritical(critChance) ? basePower * CRITICAL_POWER_MULTIPLIER : basePower;
    }
}
