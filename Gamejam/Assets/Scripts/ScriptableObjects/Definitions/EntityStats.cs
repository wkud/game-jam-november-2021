using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EntityStats", menuName = "ScriptableObjects/EntityStats", order = 2)]
public class EntityStats : ScriptableObject
{
    public int UniqueId { get; private set; }
    private static int lastAssignedId = 0;

    [SerializeField] private EntityId _identifier;
    [SerializeField] private Bond _bond;

    [SerializeField] private List<StateController> _states = new List<StateController>();
    [SerializeField] private SkillData[] _skills = new SkillData[3];

    [SerializeField] private int _maxHp = 30;
    [SerializeField] private int _initiative = 10;
    [SerializeField] private int _attackModifier = 10;
    [SerializeField] private int _defence = 10;
    [SerializeField] private int _critChance = 10;
    [SerializeField] private int _threat = 10;
    [SerializeField] public Sprite _sprite; 

    public EntityId Identifier { get => _identifier; set => _identifier = value; }
    public Bond Bond { get => _bond; set => _bond = value; }

    public List<StateController> States { get => _states; set => _states = value; }
    public Skill[] Skills { get; set; } 

    public int MaxHp { get => _maxHp; set => _maxHp = value; }
    public int CurrentHp { get; set; }
    public int Initiative { get => _initiative; set => _initiative = value; }
    public int AttackModifier { get => _attackModifier; set => _attackModifier = value; }
    public int Defence { get => _defence; set => _defence = value; }
    public int CritChance { get => _critChance; set => _critChance = value; } // 0 = never crit, 100 = always crit
    public int Threat { get => _threat; set => _threat = value; }
    public Sprite Sprite { get => _sprite; }

    public void SetValues(
        List<StateController> states,
        SkillData[] skills,
        EntityId identifier,
        Bond bond,
        int maxHp,
        int initiative,
        int attackModifier,
        int defence,
        int critChance,
        int threat,
        Sprite sprite)
    {
        _states = new List<StateController>(states);
        Skills = skills.Select(d => SkillFactory.CreateSkill(d)).ToArray();
        _identifier = identifier;
        _bond = bond;
        _maxHp = maxHp;
        CurrentHp = maxHp;
        _initiative = initiative;
        _attackModifier = attackModifier;
        _defence = defence;
        _critChance = critChance;
        _threat = threat;
        _sprite = sprite;
        UniqueId = lastAssignedId;
        lastAssignedId++;
    }

    public EntityStats GetClone()
    {
        EntityStats stats = (EntityStats)ScriptableObject.CreateInstance(typeof(EntityStats));
        stats.SetValues(_states,
         _skills,
         _identifier,
         _bond,
         _maxHp,
         _initiative,
         _attackModifier,
         _defence,
         _critChance,
         _threat,
         _sprite);
        return stats;
    }
}


