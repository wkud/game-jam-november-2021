using UnityEngine;

public class PoisonedState : IState
{
    [SerializeField] private StateData _data;

    public StateData Data => _data;

    public PoisonedState(StateData stateData)
    {
        this._data = stateData;
    }

    public void OnTurnStart(EntityStats stats)
    {
    }

    public void OnTurnEnd(EntityStats stats)
    {
        stats.Hp -= this._data.Power;
    }
}
