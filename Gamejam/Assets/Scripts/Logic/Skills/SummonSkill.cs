using System;
using System.Collections.Generic;

public class SummonSkill<T> : Skill where T : Entity
{
    private Dictionary<Type, EntityId> _enemyIds = new Dictionary<Type, EntityId>()
    {
        { typeof(Snake), EntityId.Snake },
        { typeof(WitchDoctor), EntityId.WitchDoctor },
    };

    public override void Use(Entity user, Entity[] targets)
    {
        var entityId = _enemyIds[typeof(T)];
        var entityStats = GameController.Instance.ResourceContainer.GetSummonableEntityStats(entityId);
        var entity = EntityFactory.CreateEntity(entityStats);
        FightController.SpawnEntity();
    }
}