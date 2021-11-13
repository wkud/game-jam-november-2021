public interface IState
{
    void OnStateStart(EntityStats stats);
    void OnStateEnd(EntityStats stats);
    void OnTurnStart(EntityStats stats);
    void OnTurnEnd(EntityStats stats);
    StateData Data { get; }
}
