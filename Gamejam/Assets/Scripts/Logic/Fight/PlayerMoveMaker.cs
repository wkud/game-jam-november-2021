using UnityEngine.Events;

public class PlayerMoveMaker
{
    private Player _currentPlayer;

    private int _selectedSkillIndex;
    private Entity[] _targets;

    public PlayerTurnState State { get; private set; } = PlayerTurnState.WaitingForPlayerTurn;

    public UnityEvent OnPlayerTurnEnd { get; } = new UnityEvent();

    private IFightUiUpdater _uiUpdater;
    private IFightStateHolder _fightStateHolder;

    public PlayerMoveMaker(IFightStateHolder fightStateHolder, IFightUiUpdater uiUpdater)
    {
        _fightStateHolder = fightStateHolder;
        _uiUpdater = uiUpdater;

        _uiUpdater.LockAllSkills();
        _uiUpdater.LockAllTargets();
    }

    #region StateMachine
    public void OnPlayerStartTurn(Player currentPlayer) // state transition: WaitingForPlayerTurn -> WaitingForSkill
    {
        _currentPlayer = currentPlayer;

        State = PlayerTurnState.WaitingForSkill;

        _uiUpdater.UnlockSkills(_currentPlayer);
    }

    public void OnPlayerSelectSkill(int skillIndex) // state transition: WaitingForSkill -> WaitingForTarget or -> WaitingForPlayerTurn
    {
        if (State != PlayerTurnState.WaitingForSkill)
            return;

        _uiUpdater.LockAllSkills(); // UI: end waiting for skill

        SetSkillIndex(skillIndex);

        if (_currentPlayer.IsSkillSingleTarget(skillIndex))
        {
            State = PlayerTurnState.WaitingForTarget;

            var skillTargetBond = _currentPlayer.GetSkillTargetBond(skillIndex);
            _uiUpdater.UnlockTargets(skillTargetBond); // UI: unlock valid targets for this skill
        }
        else
        {
            SetManyTargets();
            UseSkill();
        }
    }

    public void OnPlayerSelectTarget(Entity target) // state transition: WaitingForTarget -> WaitingForPlayerTurn
    {
        if (State != PlayerTurnState.WaitingForTarget)
            return;

        _uiUpdater.LockAllTargets(); // UI: end waiting for target

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

    private void SetSingleTarget(Entity target)
    {
        _targets = new Entity[] { target };
    }


}
