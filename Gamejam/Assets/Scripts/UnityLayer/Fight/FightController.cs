using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class FightController : MonoBehaviour, IFightStateHolder  // class for main fight management logic 
{

    [SerializeField] private List<Unit> _enemies;
    [SerializeField] private List<Unit> _allies;

    private List<Unit> _units = new List<Unit>();

    private IEntity _currentEntity; // an entity, which is currently making a move
    private InitiativeTracker _initiativeTracker;
    private PlayerMoveMaker _playerMoveMaker;

    private GameController _gameController;
    private IGameState _gameState;

    public PlayerTurnState PlayerTurnState => _playerMoveMaker.State; // this enum informs buttons whether they should respond to events

    public IEntity[] Enemies => _enemies.Select(u => u.Entity).ToArray();
    public IEntity[] Allies => _allies.Select(u => u.Entity).ToArray();

    public void Initialize(GameController gameController)
    {
        // setup variables
        _gameController = gameController;
        _gameState = _gameController.GameState;

        _units.AddRange(_enemies);
        _units.AddRange(_allies);

        // initialize units
        var allyPresets = _gameState.GetCharacters(); 
        InitializeUnits(_allies, allyPresets.Select(e => (IEntity)e).ToList());
        
        var enemyPresets = _gameState.GetEnemiesForThisFight() ?? new List<Enemy>();
        InitializeUnits(_enemies, enemyPresets.Select(e => (IEntity)e).ToList());

        // set intiative
        IEntity[] entities = Enemies.Concat(Allies).ToArray();
        _initiativeTracker = new InitiativeTracker(entities);

        // setup player move input system
        _playerMoveMaker = new PlayerMoveMaker(this);
        _playerMoveMaker.OnPlayerTurnEnd.AddListener(OnFinishedTurn); // TODO: add listener for playing animations

        //start turn
        _currentEntity = _initiativeTracker.GetStartEntity();
        StartTurn();
    }

    private void InitializeUnits(List<Unit> units, List<IEntity> presets)
    {
        for (int i = 0; i < presets.Count; i++)
        {
            units[i].Initialize(this, presets[i]);
        }
    }

    private void StartTurn()
    {
        if (_currentEntity.Stats.Bond == Bond.Ally)
        {
            _playerMoveMaker.OnPlayerStartTurn(_currentEntity as Player);
            // TODO: play animations 
        }
        else
        {
            (_currentEntity as Enemy)?.MakeMove(this);
            // TODO: play animations 
            OnFinishedTurn();
        }
    }

    private void OnFinishedTurn() // this function needs to be called when entity ends it's turn
    {
        _currentEntity = _initiativeTracker.GetNextEntity();
        StartTurn();
    }

    public void OnSelectSkill(int skillIndex) => _playerMoveMaker.OnPlayerSelectSkill(skillIndex);

    public void OnSelectTarget(Unit targetUnit) => _playerMoveMaker.OnPlayerSelectTarget(targetUnit.Entity);

}
