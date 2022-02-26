using System.Collections.Generic;

public interface IUnitReferenceHolder
{
    List<Unit> ActiveUnits { get; }

    List<Unit> ActiveAllyUnits { get; }

    List<Unit> ActiveEnemyUnits { get; }

    List<Unit> AllAllyUnits { get; }

    Unit GetUnitOfEntity(Entity entity);
}
