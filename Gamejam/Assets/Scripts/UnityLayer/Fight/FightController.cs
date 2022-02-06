using UnityEngine;
using System.Text;

public class FightController : MonoBehaviour  // class for main fight management logic 
{
    private PlayerMoveMaker _playerMoveMaker;
    private PlayerMoveUiUpdater _playerMoveUiUpdater;
    private InitiativeTracker _initiativeTracker;
    private InitiativeController _initiativeUiController;
    private FightUnitManager _unitManager;
    private FightOverResolver _fightOverResolver;

    private Entity _currentEntity => _initiativeTracker.GetCurrentEntity(); // an entity, which is currently making a move

    public PlayerTurnState PlayerTurnState => _playerMoveMaker.State; // this enum informs buttons whether they should respond to events

    public void Initialize(IGameState gameState)
    {
        // setup variables
        var units = FindObjectsOfType<Unit>();
        _unitManager = new FightUnitManager(gameState, units, this);

        _fightOverResolver = new FightOverResolver(_unitManager);

        // set intiative
        _initiativeTracker = new InitiativeTracker(_unitManager.ActiveEntities);

        _initiativeUiController = FindObjectOfType<InitiativeController>();
        _initiativeUiController.Initialize(_initiativeTracker);

        // setup player move input system
        _playerMoveUiUpdater = new PlayerMoveUiUpdater(_unitManager);

        _playerMoveMaker = new PlayerMoveMaker(_unitManager, _playerMoveUiUpdater);
        _playerMoveMaker.OnPlayerTurnEnd.AddListener(OnFinishedTurn); // TODO: add listener for playing animations

        //start turn
        StartTurn();

        // TODO remove this after debugging
        var stringBuilder = new StringBuilder("Initiative Queue:\n");
        var queue = _initiativeTracker.GetInitiativeQueue();
        foreach (var entity in queue)
        {
            stringBuilder.AppendLine($"{queue.IndexOf(entity) + 1}. {entity.Stats.Identifier} Stats.UniqueId: {entity.Stats.UniqueId}");
        }
        Debug.Log(stringBuilder.ToString());
    }

    private void StartTurn()
    {
        if (_currentEntity.Stats.Bond == Bond.Ally)
        {
            _playerMoveMaker.OnPlayerStartTurn(_currentEntity as Player);
            Debug.Log($"Player's turn starts. Stats.UniqueId: {_currentEntity.Stats.UniqueId}");
            // TODO: play animations 
        }
        else
        {
            Debug.Log("Enemy's turn starts");
            (_currentEntity as Enemy)?.MakeMove(_unitManager);
            // TODO: play animations 

            Debug.Log("Enemy's turn ends");
            OnFinishedTurn();
        }
    }

    private void OnFinishedTurn() // this function needs to be called when entity ends it's turn
    {
        if(_fightOverResolver.IsFightOver())
        {
            Debug.Log("Fight is over");
            _fightOverResolver.OnFightEnd();
            return;
        }

        _initiativeTracker.SetNextEntity();

        _initiativeUiController.OnFinishedTurn();
        StartTurn();
    }

    public void OnSelectSkill(int skillIndex) => _playerMoveMaker.OnPlayerSelectSkill(skillIndex);

    public void OnSelectTarget(Unit targetUnit) => _playerMoveMaker.OnPlayerSelectTarget(targetUnit.Entity);

    public void OnEntityDied(Entity entity)
    {
        _initiativeTracker.RemoveFromQueue(entity);
    }

}
