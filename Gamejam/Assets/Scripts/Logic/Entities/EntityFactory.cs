using System.Collections.Generic;
using UnityEngine;

public static class EntityFactory
{
    private static EnemyAi defaultEnemyAi = new EnemyAi();

    private static Dictionary<EntityId, EnemyAi> _enemyAis = new Dictionary<EntityId, EnemyAi>()
    {
        { EntityId.SpiritWarriorBoss, new SpiritWarriorBossAi() }, // Warning, has state!
        { EntityId.BloodShamanElite, defaultEnemyAi },
        { EntityId.JaguarWarrior, defaultEnemyAi },
        { EntityId.Snake, defaultEnemyAi },
        { EntityId.SnakeShaman, new SnakeShamanAi() },
        { EntityId.WitchDoctor, new WitchDoctorAi() },
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
        var id = entityData.Identifier;

        var enemyAi = _enemyAis[id];
        var enemy = id == EntityId.SpiritWarriorBoss
            ? new SpiritWarriorBoss(entityData, enemyAi) 
            : new Enemy(entityData, enemyAi);

        return enemy;
    }

}
