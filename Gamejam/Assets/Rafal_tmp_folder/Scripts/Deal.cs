using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deal", menuName = "ScriptableObjects/Deal", order = 1)]
public class Deal : ScriptableObject
{
    [SerializeField] public StatChange price;
    [SerializeField] public Gain profit;
}