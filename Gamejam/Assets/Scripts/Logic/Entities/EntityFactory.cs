using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityFactory
{
    private static EnemyAi defaultEnemyAi = new EnemyAi();

    private static Dictionary<EntityId, EnemyAi> _enemyAis = new Dictionary<EntityId, EnemyAi>()
    {
        { EntityId.SpiritWarrior, new SpiritWarriorAi() }, // Warning, has state!
        { EntityId.Aboriginal, defaultEnemyAi },
        { EntityId.JaguarWarrior, defaultEnemyAi },
        { EntityId.Eagle, defaultEnemyAi },
        { EntityId.Snake, defaultEnemyAi },
        { EntityId.HighShaman, defaultEnemyAi },
        { EntityId.BloodShaman, defaultEnemyAi },
        { EntityId.BlessShaman, defaultEnemyAi },
        { EntityId.ClothArtist, defaultEnemyAi },
        { EntityId.CursedMummy, defaultEnemyAi },
        { EntityId.ToxicHunter, defaultEnemyAi },
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
