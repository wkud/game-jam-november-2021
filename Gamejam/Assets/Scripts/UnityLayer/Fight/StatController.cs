using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour, IDescriptable
{
    [SerializeField] private StatName _statName;
    private Unit _unit;

    public string Description => "";

    public bool IsShowable => true;

    public string Title => (_unit?.Entity as Player)?.GetStatDescription(_statName);

    public void Initialize(Unit unit)
    {
        _unit = unit;
    }

}
