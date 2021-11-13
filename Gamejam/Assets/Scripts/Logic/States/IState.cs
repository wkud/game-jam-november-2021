public interface IState
{
  void onTurnStart(EntityStats stats);
  void onTurnEnd(EntityStats stats);
  StateData Data { get; }
}
