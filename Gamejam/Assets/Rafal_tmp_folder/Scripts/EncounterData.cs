using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "EncounterData", menuName = "ScriptableObjects/EncounterData", order = 1)]
public class EncounterData : ScriptableObject
{
    [Header("Should be between 1 and 4 enemies")]
    [SerializeField] private EntityStats[] enemies;
    public EntityStats[] EnemyStats => enemies;

    [SerializeField] private EncounterType encounterType;
    public EncounterType EncounterType { get => encounterType; }

    public List<Enemy> GetEnemies() => enemies.Select(s => (Enemy)EntityFactory.CreateEntity(s)).ToList();

}