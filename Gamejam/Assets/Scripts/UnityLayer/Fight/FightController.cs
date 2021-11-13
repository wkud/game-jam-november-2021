using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{

    [SerializeField] private List<Unit> _enemies;
    [SerializeField] private List<Unit> _allies;

    private List<Unit> _units;

    void Start()
    {
        _units.AddRange(_enemies);
        _units.AddRange(_allies);

        foreach (var unit in _units)
        {
            unit.Initialize();
        }
    }

    void Update()
    {
        
    }
}
