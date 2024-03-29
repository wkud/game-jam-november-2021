using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class ResourceContainer
{
    [SerializeField] List<EncounterData> _normalEasyEncounters;
    [SerializeField] List<EncounterData> _normalMediumEncounters;
    [SerializeField] List<EncounterData> _normalHardEncounters;
    [SerializeField] List<EncounterData> _eliteEncounters;
    [SerializeField] List<EncounterData> _bossEncounters;

    public List<EncounterData> NormalEasyEncounters { get => _normalEasyEncounters; }
    public List<EncounterData> NormalMediumEncounters { get => _normalMediumEncounters; }
    public List<EncounterData> NormalHardEncounters { get => _normalHardEncounters; }
    public List<EncounterData> EliteEncounters { get => _eliteEncounters; }
    public List<EncounterData> BossEncounters { get => _bossEncounters; }

    [SerializeField] private List<EntityStats> _characterStats;
    [SerializeField] private Sprite _deadCharacterPortrait;
    [SerializeField] private EntityStats _summonableSnakeStats;
    [SerializeField] private EntityStats _summonableWitchDoctorStats;

    public List<EntityStats> CharacterStats => _characterStats;
    public Sprite DeadCharacterPortrait => _deadCharacterPortrait;

    [SerializeField] private Sprite[] _statImages = new Sprite[6];

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

    public Sprite GetStatSprite(StatName statName)
    {
        return _statImages[(int)statName];
    }

    public EntityStats GetSummonableEntityStats(EntityId entityId)
    {
        switch(entityId)
        {
            case EntityId.Snake:
                return _summonableSnakeStats;
            case EntityId.WitchDoctor:
                return _summonableWitchDoctorStats;
            default:
                Debug.LogError($"Entity Id of: {entityId} was not designed to be summoned");
                return null;
        }
    }
}
