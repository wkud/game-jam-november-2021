using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    List<Enemy> GetEnemiesForThisFight();
    List<Player> Allies { get; }
}
