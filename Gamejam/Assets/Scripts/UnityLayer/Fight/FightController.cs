using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class FightController : MonoBehaviour  // class for main fight management logic 
{
    private InitiativeTracker _initiativeTracker;
    private PlayerMoveMaker _playerMoveMaker;
    private InitiativeController _initiativeUiController;
    [SerializeField] private FightUnitManager _unitManager;

    private Entity _currentEntity => _initiativeTracker.GetCurrentEntity(); // an entity, which is currently making a move

    public PlayerTurnState PlayerTurnState => _playerMoveMaker.State; // this enum informs buttons whether they should respond to events

    public void Initialize(IGameState gameState)
    {
        // setup variables
        var units = FindObjectsOfType<Unit>();
        _unitManager.Initialize(gameState, units, this);

        // set intiative
        _initiativeTracker = new InitiativeTracker(_unitManager.ActiveEntities);

        _initiativeUiController = FindObjectOfType<InitiativeController>();
        _initiativeUiController.Initialize(_initiativeTracker);

        // setup player move input system
        _playerMoveMaker = new PlayerMoveMaker(_unitManager);
        _playerMoveMaker.OnPlayerTurnEnd.AddListener(OnFinishedTurn); // TODO: add listener for playing animations

        //start turn
        StartTurn();
    }

    private void StartTurn()
    {
        if (_currentEntity.Stats.Bond == Bond.Ally)
        {
            _playerMoveMaker.OnPlayerStartTurn(_currentEntity as Player);
            Debug.Log("Player's turn starts");
            // TODO: play animations 
        }
        else
        {
            Debug.Log("Enemy's turn starts");
            (_currentEntity as Enemy)?.MakeMove(_unitManager);
            // TODO: play animations 

            OnFinishedTurn();
            Debug.Log("Enemy's turn ends");
        }
    }

    private void OnFinishedTurn() // this function needs to be called when entity ends it's turn
    {
        _initiativeTracker.SetNextEntity();

        _initiativeUiController.OnFinishedTurn();
        StartTurn();
    }

    public void OnSelectSkill(int skillIndex) => _playerMoveMaker.OnPlayerSelectSkill(skillIndex);

    public void OnSelectTarget(Unit targetUnit) => _playerMoveMaker.OnPlayerSelectTarget(targetUnit.Entity);

}
