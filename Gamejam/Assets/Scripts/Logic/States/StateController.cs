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

  public void onTurnStart(EntityStats stats)
  {
    this.State.onTurnStart(stats);
  }

  public void onTurnEnd(EntityStats stats)
  {
    this.State.onTurnEnd(stats);
    this._turnsLeft--;
  }
}
