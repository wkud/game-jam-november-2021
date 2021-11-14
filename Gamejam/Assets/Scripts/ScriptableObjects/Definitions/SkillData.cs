using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 1)]
public class SkillData : ScriptableObject
{
    [SerializeField] private SkillName _skillCode;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _power = 9001;
    [SerializeField] private int _maxCooldown = 5;
    [SerializeField] private SkillTargetCount _targetCount;
    [SerializeField] private Bond _targetBond;
    [SerializeField] private StateData _imposedState;
    [SerializeField] private Sprite _sprite;

    public string Name { get => _name; set => _name = value; }
    public int Power { get => _power; set => _power = value; }
    public int MaxCooldown { get => _maxCooldown; set => _maxCooldown = value; }
    public int CurrentCooldown { get; set; } // this property shouldn't be serializable, because there is no need to
    public string Description { get => _description; set => _description = value; }
    public SkillTargetCount TargetCount { get => _targetCount; set => _targetCount = value; }
    public Bond TargetBond { get => _targetBond; set => _targetBond = value; }
    public SkillName Identifier { get => _skillCode; set => _skillCode = value; }
    public StateData ImposedState { get => _imposedState; set => _imposedState = value; }
    public Sprite Sprite { get => _sprite; }
}
