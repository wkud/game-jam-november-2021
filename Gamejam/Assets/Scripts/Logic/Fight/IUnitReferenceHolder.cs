using System.Collections.Generic;

public interface IUnitReferenceHolder
{
    List<Unit> ActiveUnits { get; }

    List<Unit> AllAllyUnits { get; }

    List<Unit> ActiveEnemyUnits { get; }

}
