using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    List<EntityStats> GetEnemiesForThisFight();
    List<EntityStats> GetCharacters();
}
