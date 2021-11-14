using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DealStat", menuName = "ScriptableObjects/DealStat", order = 1)]
public class StatChange : Gain
{
    [SerializeField] public StatName statName;
    [SerializeField] public int amount;
}
