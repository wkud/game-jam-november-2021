using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityFactory 
{
    private static Dictionary<EntityId, IEnemyAi> _enemyAis = new Dictionary<EntityId, IEnemyAi>()
    {
        { EntityId.SpiritWarrior, new SpiritWarriorAi() },
        { EntityId.JaguarWarrior, new JaguarWarriorAi() },
    };

    public static IEntity CreateEntity(EntityStats entityData)
    {
        IEntity entity = entityData.Bond == Bond.Ally 
            ? new Player(entityData) as IEntity
            : CreateEnemy(entityData) as IEntity;
        return entity;
    }

    private static Enemy CreateEnemy(EntityStats entityData)
    {
        var enemyAi = _enemyAis[entityData.Identifier];
        return new Enemy(enemyAi, entityData);
    }

}