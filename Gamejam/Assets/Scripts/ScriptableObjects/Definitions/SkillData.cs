using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 1)]
public class SkillData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _power = 9001;
    [SerializeField] private int _maxCooldown = 5;
    [SerializeField] private SkillTargetCount _targetCount;
    [SerializeField] private Bond _targetBond;

    public string Name { get => _name; set => _name = value; }
    public int Power { get => _power; set => _power = value; }
    public int MaxCooldown { get => _maxCooldown; set => _maxCooldown = value; }
    public int CurrentCooldown { get; set; } // this property shouln't be serializable, becouse there is no need to
    public string Description { get => _description; set => _description = value; }
    public SkillTargetCount TargetCount { get => _targetCount; set => _targetCount = value; }
    public Bond TargetBond { get => _targetBond; set => _targetBond = value; }
}
