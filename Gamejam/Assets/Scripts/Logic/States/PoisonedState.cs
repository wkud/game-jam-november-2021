using UnityEngine;

public class PoisonedState : IState
{
  [SerializeField] private StateData _data;

  public StateData Data => _data;

  public PoisonedState(StateData stateData)
  {
    this._data = stateData;
  }

  public void onTurnStart(EntityStats stats)
  {
  }

  public void onTurnEnd(EntityStats stats)
  {
    stats.Hp -= this._data.Power;
  }
}
