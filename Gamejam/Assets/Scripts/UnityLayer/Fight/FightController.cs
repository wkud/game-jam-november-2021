using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class FightController : MonoBehaviour, IFightStateHolder  // class for main fight management logic 
{

    [SerializeField] private List<Unit> _allAllies;
    [SerializeField] private List<Unit> _allEnemies;
    private List<Unit> _activeEnemies => _allEnemies.Where(e => e.IsActive).ToList();


    private List<Unit> _units = new List<Unit>();

    private IEntity _currentEntity; // an entity, which is currently making a move
    private InitiativeTracker _initiativeTracker;
    private PlayerMoveMaker _playerMoveMaker;

    private GameController _gameController;
    private IGameState _gameState;

    public PlayerTurnState PlayerTurnState => _playerMoveMaker.State; // this enum informs buttons whether they should respond to events

    public IEntity[] Enemies => _activeEnemies.Select(u => u.Entity).ToArray();
    public IEntity[] Allies => _allAllies.Select(u => u.Entity).ToArray();

    public void Initialize(GameController gameController)
    {
        // setup variables
        _gameController = gameController;
        _gameState = _gameController.GameState;

        _units.AddRange(_allEnemies);
        _units.AddRange(_allAllies);

        // initialize units
        var allyPresets = _gameState.GetCharacters().Select(e => (IEntity)e).ToList(); 
        InitializeUnits(_allAllies, allyPresets);
        
        var enemies = _gameState.GetEnemiesForThisFight() ?? new List<Enemy>();
        var enemyPresets = enemies.Select(e => (IEntity)e).ToList();
        HideUnusedUnits(_allEnemies, enemyPresets);
        InitializeUnits(_activeEnemies, enemyPresets);

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
        for (int i = 0; i < units.Count; i++)
        {
            units[i].Initialize(this, presets[i]);
        }
    }
    private void HideUnusedUnits(List<Unit> units, List<IEntity> presets)
    {
        if (units.Count > presets.Count)
        {
            var unusedUnitCount = units.Count - presets.Count;
            for (int i = 0; i < unusedUnitCount; i++)
            {
                units[i].Hide();
            }
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
