using UnityEngine;

public class StateController
{
    [SerializeField] IState State { get; }

    [SerializeField] private int _turnsLeft;
    public int TurnsLeft { get => _turnsLeft; }


    public StateController(IState State, int turns)
    {
        this._turnsLeft = turns;
    }

    public void OnTurnStart(EntityStats stats)
    {
        this.State.OnTurnStart(stats);
    }

    public void OnTurnEnd(EntityStats stats)
    {
        this.State.OnTurnEnd(stats);
        this._turnsLeft--;
    }
}
