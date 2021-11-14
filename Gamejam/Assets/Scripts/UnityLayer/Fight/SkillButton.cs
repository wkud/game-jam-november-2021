using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour // TODO implement selecting skill
{
    [SerializeField] private int _skillSlotNumber; // values from 0 to 2 (for 3 slots)
    private Unit _unit;

    public void Initialize(Unit unit)
    {
        _unit = unit;
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick() => _unit.OnSkillClick(_skillSlotNumber);

}
