using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deal", menuName = "ScriptableObjects/Deal", order = 1)]
public class Deal : ScriptableObject
{
    [SerializeField] StatChange price;
    [SerializeField] Gain profit;
}