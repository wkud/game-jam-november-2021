using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightNode : MapNode
{
    [SerializeField] private List<EntityStats> _enemies;
    public List<EntityStats> Enemies => _enemies;

}
