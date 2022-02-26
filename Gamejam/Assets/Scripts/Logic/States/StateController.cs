using UnityEngine;

public class StateController
{
    [SerializeField] IState State { get; }

    [SerializeField] private int _turnsLeft;
    public int TurnsLeft { get => _turnsLeft; }


    public StateController(EntityStats stats, IState state, int turns)
    {
        _turnsLeft = turns;
        State = state;
        State.OnStateStart(stats);
    }

    public void OnTurnStart(EntityStats stats)
    {
        State.OnTurnStart(stats);
    }

    public void OnTurnEnd(EntityStats stats)
    {
        State.OnTurnEnd(stats);
        _turnsLeft--;

        if (_turnsLeft <= 0) State.OnStateEnd(stats);
    }
}
