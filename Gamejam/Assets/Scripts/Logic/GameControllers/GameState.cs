using System.Collections.Generic;
using System.Linq;

public class GameState : IGameState
{
    public List<Player> Allies { get; private set; }

    public MapNode CurrentNode { private get; set; }

    private ResourceContainer _resourceContainer;
    public GameState(ResourceContainer resourceContainer)
    {
        _resourceContainer = resourceContainer;
        Allies = resourceContainer.GetRandomCharacterPresets().Select(s => (Player)EntityFactory.CreateEntity(s)).ToList();
    }

    public List<Enemy> GetEnemiesForThisFight()
    {
        var enemyStats = _resourceContainer.EnemyStats.First();
        
        var enemies = new List<Enemy>(); // TODO replace this code with actual getting enemies assigned to map node
        for (int i = 0; i < 2; i++)
        {
            var enemy = (Enemy)EntityFactory.CreateEntity(enemyStats);
            enemies.Add(enemy);
        }

        return enemies;
    }

    public List<Player> GetCharacters()
    {
        return Allies;
    }
}
