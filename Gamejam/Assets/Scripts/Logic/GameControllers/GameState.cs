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
        EncounterData encounter = ((FightNode)CurrentNode).EncounterData;
        //var enemies = new List<Enemy>();
        //for (int i = 0; i < encounter.Enemies.Length; i++)
        //{
        //    var enemy = (Enemy)EntityFactory.CreateEntity(encounter.Enemies[i]);
        //    enemies.Add(enemy);
        //}
        return encounter.Enemies.Select(s => (Enemy)EntityFactory.CreateEntity(s)).ToList();
    }

    
}
