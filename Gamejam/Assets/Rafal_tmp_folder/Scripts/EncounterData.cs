using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterData", menuName = "ScriptableObjects/EncounterData", order = 1)]
public class EncounterData : ScriptableObject
{
    [Header("Should be between 1 and 4 enemies")]
    [SerializeField] private EntityStats[] enemies;
    public EntityStats[] Enemies { get => enemies;}
    [SerializeField] private EncounterType encounterType;
    public EncounterType EncounterType { get => encounterType; }
}