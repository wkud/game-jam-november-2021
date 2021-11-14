using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        List<EntityStats> test = ((FightNode)CurrentNode).Enemies; 
        //var fightNode = (FightNode)CurrentNode;
        //return fightNode.Enemies.Select(s => (Enemy)EntityFactory.CreateEntity(s)).ToList();
        return null;
    }

    public List<Player> GetCharacters()
    {
        return Allies;
    }
}
