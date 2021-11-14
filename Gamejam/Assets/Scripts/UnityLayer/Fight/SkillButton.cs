using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IDescriptable
{
    [SerializeField] private int _skillSlotNumber; // values from 0 to 2 (for 3 slots)
    private Unit _unit;

    public string Description => (_unit?.Entity as Player)?.GetSkillDescription(_skillSlotNumber);

    public void Initialize(Unit unit)
    {
        _unit = unit;
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick() => _unit.OnSkillClick(_skillSlotNumber);

}
