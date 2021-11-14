using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityFactory
{
    private static Dictionary<EntityId, IEnemyAi> _enemyAis = new Dictionary<EntityId, IEnemyAi>()
    {
        { EntityId.SpiritWarriorBoss, new SpiritWarriorAi() },
        { EntityId.JaguarWarrior, new JaguarWarriorAi() },
    };

    public static Entity CreateEntity(EntityStats entityData)
    {
        if (entityData.Bond == Bond.Ally && entityData.Identifier != EntityId.PlayerCharacter)
        {
            Debug.LogError("Entity stats with Bond=Ally must have Identifier=PlayerCharacter.");
        }
        if (entityData.Bond == Bond.Enemy && entityData.Identifier == EntityId.PlayerCharacter)
        {
            Debug.LogError("Entity stats with Bond=Enemy cannot have Identifier=PlayerCharacter.");
        }

        Entity entity = entityData.Bond == Bond.Ally
            ? new Player(entityData) as Entity
            : CreateEnemy(entityData) as Entity;
        return entity;
    }

    private static Enemy CreateEnemy(EntityStats entityData)
    {
        var enemyAi = _enemyAis[entityData.Identifier];
        return new Enemy(entityData, enemyAi);
    }

}
