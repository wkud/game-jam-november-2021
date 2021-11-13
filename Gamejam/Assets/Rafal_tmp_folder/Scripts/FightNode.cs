using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightNode : MapNode
{
    [SerializeField] private List<EntityStats> _enemies;
    public List<EntityStats> Enemies => _enemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
