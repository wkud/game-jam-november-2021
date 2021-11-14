using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightNode : MapNode
{
    private List<EntityStats> _enemies;
    public List<EntityStats> Enemies { get => _enemies; set => _enemies = value; }
    

}
