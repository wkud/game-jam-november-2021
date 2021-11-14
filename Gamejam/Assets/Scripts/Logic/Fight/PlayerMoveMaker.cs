using UnityEngine.Events;

public class PlayerMoveMaker
{
    private Player _currentPlayer;

    private int _selectedSkillIndex;
    private IEntity[] _targets;

    public PlayerTurnState State { get; private set; } = PlayerTurnState.WaitingForPlayerTurn;
    public UnityEvent OnPlayerTurnEnd { get; } = new UnityEvent();


    private IFightStateHolder _fightStateHolder;
    public PlayerMoveMaker(IFightStateHolder fightStateHolder)
    {
        _fightStateHolder = fightStateHolder;
    }

    #region StateMachine
    public void OnPlayerStartTurn(Player currentPlayer) // state transition: WaitingForPlayerTurn -> WaitingForSkill
    {
        _currentPlayer = currentPlayer;
        State = PlayerTurnState.WaitingForSkill;
    }

    public void OnPlayerSelectSkill(int skillIndex) // state transition: WaitingForSkill -> WaitingForTarget or -> WaitingForPlayerTurn
    {
        if (State != PlayerTurnState.WaitingForSkill)
            return;

        SetSkillIndex(skillIndex);
        
        if (_currentPlayer.IsSkillSingleTarget(skillIndex))
        {
            State = PlayerTurnState.WaitingForTarget;
        }
        else
        {
            SetManyTargets();
            UseSkill();
        }
    }

    public void OnPlayerSelectTarget(IEntity target) // state transition: WaitingForTarget -> WaitingForPlayerTurn
    {
        if (State != PlayerTurnState.WaitingForTarget)
            return;

        SetSingleTarget(target);
        UseSkill();
    }

    private void UseSkill() // -> WaitingForPlayerTurn
    {
        _currentPlayer.UseSkill(_selectedSkillIndex, _targets);

        State = PlayerTurnState.WaitingForPlayerTurn;
        OnPlayerTurnEnd.Invoke();
    }
    #endregion


    private void SetSkillIndex(int skillIndex)
    {
        _selectedSkillIndex = skillIndex;
    }

    private void SetManyTargets()
    {
        var targetBond = _currentPlayer.GetSkillTargetBond(_selectedSkillIndex);
        _targets = targetBond == Bond.Ally ? _fightStateHolder.Allies : _fightStateHolder.Enemies;
    }

    private void SetSingleTarget(IEntity target)
    {
        _targets = new IEntity[] { target };
    }


}
