﻿using System.Linq;
using System.Collections.Generic;

public class FightUnitManager : IFightStateHolder, IUnitReferenceHolder
{
    private List<Unit> _allEnemies;
    private List<Unit> _allAllies;
    private  List<Unit> _units = new List<Unit>();

    public List<Unit> ActiveUnits => _units.Where(u => u.IsActive && u.Entity.IsAlive).ToList();
    public List<Unit> ActiveAllyUnits => _allAllies.Where(u => u.Entity.IsAlive).ToList();
    public List<Unit> ActiveEnemyUnits => _allEnemies.Where(u => u.IsActive).ToList();

    public Entity[] Enemies => ActiveEnemyUnits.Select(u => u.Entity).ToArray();
    public Entity[] Allies => ActiveAllyUnits.Select(u => u.Entity).ToArray();
    public Entity[] ActiveEntities => _units.Where(u => u.IsActive).Select(u => u.Entity).ToArray();

    public FightUnitManager(IGameState gameState, Unit[] units, FightController fightController)
    {
        // get references to units
        _units = units.ToList();
        _allAllies = units.Where(u => u.CompareTag("Player")).ToList();
        _allEnemies = units.Where(u => u.CompareTag("Enemy")).ToList();

        // initialize allies
        var allyPresets = gameState.Allies.Select(e => (Entity)e).ToList();
        InitializeUnits(_allAllies, allyPresets, fightController);

        // initialize enemies
        var enemies = gameState.GetEnemiesForThisFight() ?? new List<Enemy>();
        var enemyPresets = enemies.Select(e => (Entity)e).ToList();
        HideUnusedUnits(_allEnemies, enemyPresets);
        InitializeUnits(ActiveEnemyUnits, enemyPresets, fightController);
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

    public Unit GetUnitOfEntity(Entity entity) => _units.FirstOrDefault(u => u.Entity == entity);
}
