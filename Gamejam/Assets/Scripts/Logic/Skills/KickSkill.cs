using UnityEngine;

public class KickSkill : ISkill
{
    [SerializeField] private SkillData _data;

    public SkillData Data => _data;

    public KickSkill(SkillData data)
    {
        _data = data;
    }

    public void Use(Entity user, Entity[] targets)
    {
        targets[0]?.TakeDamage(this._data.Power);
    }

}
