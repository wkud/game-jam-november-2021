using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "EntityStats", menuName = "ScriptableObjects/EntityStats", order = 2)]
public class EntityStats : ScriptableObject
{
    [SerializeField] private EntityId _identifier;
    [SerializeField] private Bond _bond;

    [SerializeField] private List<StateController> _states = new List<StateController>();
    [SerializeField] private SkillData[] _skills = new SkillData[3];

    [SerializeField] private int _maxHp = 30;
    [SerializeField] private int _initiative = 10;
    [SerializeField] private int _attackModifier = 10;
    [SerializeField] private int _defence = 10;
    [SerializeField] private float _critChance = 10;
    [SerializeField] private float _threat = 10;
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
    public float CritChance { get => _critChance; set => _critChance = value; }
    public float Threat { get => _threat; set => _threat = value; }
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
        float critChance,
        float threat)
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
         _threat);
        return stats;
    }
}


