using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 1)]
public class SkillData : ScriptableObject
{


  [SerializeField] private string _name;
  [SerializeField] private string _description;
  [SerializeField] private int _power = 9001;
  [SerializeField] private int _cooldown = 5;
  [SerializeField] private SkillTargetCountType _targetCountType;

  public string Name { get => _name; set => _name = value; }
  public int Power { get => _power; set => _power = value; }
  public int Cooldown { get => _cooldown; set => _cooldown = value; }
  public string Description { get => _description; set => _description = value; }
  public SkillTargetCountType TargetCountType { get => _targetCountType; set => _targetCountType = value; }
}
