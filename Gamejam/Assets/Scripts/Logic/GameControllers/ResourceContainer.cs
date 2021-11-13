using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class ResourceContainer
{
    [SerializeField] private List<EntityStats> _characterStats;
    [SerializeField] private List<EntityStats> _enemyStats;

    public List<EntityStats> CharacterStats => _characterStats;
    public List<EntityStats> EnemyStats => _enemyStats;

    public List<EntityStats> GetRandomCharacterPresets(int partySize = 4)
    {
        var randomCharacters = new List<EntityStats>();
        var randomizer = new System.Random();

        var characters = CharacterStats.ToList();
        for (int i = 0; i < partySize; i++)
        {
            var index = randomizer.Next(0, characters.Count);
            randomCharacters.Add(characters[index]);
            characters.RemoveAt(index);
        }

        return randomCharacters;
    }
}
