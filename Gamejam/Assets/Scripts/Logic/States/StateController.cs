using UnityEngine;

public class StateController
{
    [SerializeField] IState State { get; }

    [SerializeField] private int _turnsLeft;
    public int TurnsLeft { get => _turnsLeft; }


    public StateController(EntityStats stats, IState state, int turns)
    {
        this._turnsLeft = turns;
        this.State = state;
        this.State.OnStateStart(stats);
    }

    public void OnTurnStart(EntityStats stats)
    {
        this.State.OnTurnStart(stats);
    }

    public void OnTurnEnd(EntityStats stats)
    {
        this.State.OnTurnEnd(stats);
        this._turnsLeft--;

        if (this._turnsLeft <= 0) this.State.OnStateEnd(stats);
    }
}
