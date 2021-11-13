using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{

    [SerializeField] private List<Unit> _enemies;
    [SerializeField] private List<Unit> _allies;

    private List<Unit> _units;

    private Unit _currentUnit; // a unit, who is currently making a move
    //private InitiativeTracker _initiativeTracker = new InitiativeTracker(); // _initiativeTracker.GetNextUnit();


    //private ISkill _skill;
    private Unit[] _targets;

    public FightState State { get; private set; } = FightState.WaitingForSkill;

    void Start()
    {
        _units.AddRange(_enemies);
        _units.AddRange(_allies);

        foreach (var unit in _units)
        {
            unit.Initialize(this);
        }
    }

    void Update()
    {
        
    }

    public void SetTarget(Unit unit)
    {
        //if (_skill.TargetCount == SkillTargetCount.One)
        //{
        //    _targets = new Unit[] { unit };
        //    _currentUnit.Use(_skill, _targets);
        //}
    }
}
