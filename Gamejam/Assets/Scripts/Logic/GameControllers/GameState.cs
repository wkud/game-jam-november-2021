using System.Collections.Generic;
using System.Linq;

public class GameState : IGameState
{
    public List<Player> Allies { get; private set; }

    public MapNode CurrentNode { private get; set; }

    public GameState(ResourceContainer resourceContainer)
    {
        Allies = resourceContainer.GetRandomCharacterPresets().Select(s => (Player)EntityFactory.CreateEntity(s)).ToList();
    }

    public List<Enemy> GetEnemiesForThisFight()
    {
        if (CurrentNode == null)
        {
            //return GameController.Instance.Resources.NormalEasyEncounters.First().Enemies.Select(s => (Enemy)EntityFactory.CreateEntity(s)).ToList();
            return GameController.Instance.Resources.NormalMediumEncounters
                .FirstOrDefault(en => en.EnemyStats.Any(e => e.Identifier == EntityId.SnakeShaman))
                .GetEnemies();
        }

        EncounterData encounter = ((FightNode)CurrentNode).EncounterData;
        return encounter.GetEnemies();
    }

    
}
