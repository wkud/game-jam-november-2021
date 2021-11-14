using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterData", menuName = "ScriptableObjects/EncounterData", order = 1)]
public class EncounterData : ScriptableObject
{
    [SerializeField] public List<EntityStats> _enemies;
}
