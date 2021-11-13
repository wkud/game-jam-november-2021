using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FightController : MonoBehaviour, IFightStateHolder  // class for main fight management logic 
{

    [SerializeField] private List<Unit> _enemies;
    [SerializeField] private List<Unit> _allies;

    private List<Unit> _units = new List<Unit>();

    private IEntity _currentEntity; // an entity, which is currently making a move
    private InitiativeTracker _initiativeTracker;
    private PlayerMoveMaker _playerMoveMaker;


    public PlayerTurnState PlayerTurnState => _playerMoveMaker.State; // this enum informs buttons whether they should respond to events

    public IEntity[] Enemies => _enemies.Select(u => u.Entity).ToArray();
    public IEntity[] Allies => _allies.Select(u => u.Entity).ToArray();


    void Start()
    {
        _units.AddRange(_enemies);
        _units.AddRange(_allies);

        foreach (var unit in _units)
        {
            unit.Initialize(this); // TODO: Rafał, dlaczego tu jest NullReferenceException przy uruchomieniu i jak to naprawić
        }

        _initiativeTracker = new InitiativeTracker();

        _playerMoveMaker = new PlayerMoveMaker(this);
        _playerMoveMaker.OnPlayerTurnEnd.AddListener(OnFinishedTurn); // TODO: add listener for playing animations
    }

    public void OnFinishedTurn() // this function needs to be called when entity ends it's turn
    {
        _currentEntity = _initiativeTracker.GetNextEntity();
        if (_currentEntity.Stats.Bond == Bond.Ally)
        {
            _playerMoveMaker.OnPlayerStartTurn();
            // TODO: play animations 
        }
        else
        {
            (_currentEntity as Enemy)?.MakeMove(this);
            // TODO: play animations 
            OnFinishedTurn();
        }
    }

    public void OnSelectSkill(int skillIndex) => _playerMoveMaker.OnPlayerSelectSkill(skillIndex);

    public void OnSelectTarget(Unit targetUnit) => _playerMoveMaker.OnPlayerSelectTarget(targetUnit.Entity);

}
