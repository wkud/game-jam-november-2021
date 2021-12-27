using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FightUnitManager : IFightStateHolder
{
    private List<Unit> _allAllies;
    private List<Unit> _allEnemies;

    private List<Unit> _units = new List<Unit>();
    private List<Unit> _activeEnemies => _allEnemies.Where(e => e.IsActive).ToList();

    public Entity[] Enemies => _activeEnemies.Select(u => u.Entity).ToArray();
    public Entity[] Allies => _allAllies.Select(u => u.Entity).ToArray();
    public Entity[] ActiveEntities => _units.Where(u => u.IsActive).Select(u => u.Entity).ToArray();

    public void Initialize(IGameState gameState, Unit[] units, FightController fightController)
    {
        // get references to units
        _units = units.ToList();
        _allAllies = units.Where(u => u.CompareTag("Player")).ToList();
        _allEnemies = units.Where(u => u.CompareTag("Enemy")).ToList();

        // initialize allies
        var allyPresets = gameState.GetCharacters().Select(e => (Entity)e).ToList();
        InitializeUnits(_allAllies, allyPresets, fightController);

        // initialize enemies
        var enemies = gameState.GetEnemiesForThisFight() ?? new List<Enemy>();
        var enemyPresets = enemies.Select(e => (Entity)e).ToList();
        HideUnusedUnits(_allEnemies, enemyPresets);
        InitializeUnits(_activeEnemies, enemyPresets, fightController);
    }

    private void InitializeUnits(List<Unit> units, List<Entity> presets, FightController fightController)
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].Initialize(fightController, presets[i]);
        }
    }

    private void HideUnusedUnits(List<Unit> units, List<Entity> presets)
    {
        if (units.Count > presets.Count)
        {
            var unusedUnitCount = units.Count - presets.Count;
            for (int i = 0; i < unusedUnitCount; i++)
            {
                units[i].Hide();
            }
        }
    }
}
