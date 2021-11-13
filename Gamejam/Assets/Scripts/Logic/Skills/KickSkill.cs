using UnityEngine;

public class KickSkill : ISkill
{
    [SerializeField] private SkillData _data;

    public SkillData Data => _data;

    public SkillTargetCount TargetCount { get => _data.TargetCount; }
    public Bond TargetBound { get => _data.TargetBond; }

    public string Description { get => _data.Description; }

    public int MaxCooldown { get => _data.MaxCooldown; }

    public int CurrentCooldown { get => _data.CurrentCooldown; set => _data.CurrentCooldown = value; }


    public KickSkill(SkillData data)
    {
        _data = data;
    }

    public void Use(IEntity user, IEntity[] targets)
    {
        targets[0]?.TakeDamage(this._data.Power);
    }

}
