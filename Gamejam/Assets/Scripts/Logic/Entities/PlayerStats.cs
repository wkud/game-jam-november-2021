using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 2)]
public class PlayerStats : ScriptableObject
{
  [SerializeField] private ISkill[] _skills = new ISkill[3];
  [SerializeField] private IBuff[] _buffs = new IBuff[2];
  [SerializeField] private IDebuff[] _debuffs = new IDebuff[2];
  [SerializeField] private int _hp = 30;
  [SerializeField] private int _initiative = 10;
  [SerializeField] private int _attackModifier = 10;
  [SerializeField] private int _defence = 10;
  [SerializeField] private float _critChance = 10;

  public int Hp { get => _hp; set => _hp = value; }
  public ISkill[] Skills { get => _skills; set => _skills = value; }
  public IBuff[] Buffs { get => _buffs; set => _buffs = value; }
  public IDebuff[] Debuffs { get => _debuffs; set => _debuffs = value; }
  public int Initiative { get => _initiative; set => _initiative = value; }
  public int AttackModifier { get => _attackModifier; set => _attackModifier = value; }
  public int Defence { get => _defence; set => _defence = value; }
  public float CritChance { get => _critChance; set => _critChance = value; }
}
