public interface IState
{
    void OnTurnStart(EntityStats stats);
    void OnTurnEnd(EntityStats stats);
    StateData Data { get; }
}
