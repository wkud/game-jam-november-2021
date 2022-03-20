using System.Collections.Generic;
using System.Linq;

public class GameState : IGameState
{
    public List<Player> Allies { get; private set; }

    public MapNode CurrentNode { private get; set; }

    public EncounterType CurrentEncounterDifficulty => (CurrentNode as FightNode)?.EncounterData.EncounterType ?? EncounterType.Normal;

    public GameState(ResourceContainer resourceContainer)
    {
        Allies = resourceContainer.GetRandomCharacterPresets().Select(s => (Player)EntityFactory.CreateEntity(s)).ToList();
    }

    public List<Enemy> GetEnemiesForThisFight()
    {
        if (CurrentNode == null)
        {
            //return GameController.Instance.Resources.NormalEasyEncounters.First().Enemies.Select(s => (Enemy)EntityFactory.CreateEntity(s)).ToList();
            var snake = GameController.Instance.Resources.NormalMediumEncounters
                .FirstOrDefault(en => en.EnemyStats.Any(e => e.Identifier == EntityId.Snake))
                .GetEnemies().FirstOrDefault(e => e.Stats.Identifier == EntityId.Snake);
            snake.Stats.CurrentHp = 1;
            return new List<Enemy>() { snake };
        }

        EncounterData encounter = ((FightNode)CurrentNode).EncounterData;
        return encounter.GetEnemies();
    }

    
}
