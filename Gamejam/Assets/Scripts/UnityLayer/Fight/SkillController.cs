using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour, IDescriptable
{
    private Unit _unit;


    [SerializeField] private int _skillSlotNumber; // values from 0 to 2 (for 3 slots)
    public int SkillSlotNumber => _skillSlotNumber;

    private bool _isActive = true;
    public bool IsActive
    {
        get => _isActive;
        private set
        {
            _isActive = value;
            gameObject.SetActive(_isActive);
        }
    }

    public string Description => (_unit?.Entity as Player)?.GetSkillDescription(_skillSlotNumber);

    public void Initialize(Unit unit)
    {
        _unit = unit;
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick() => _unit.OnSkillClick(_skillSlotNumber);

    public void Hide() => IsActive = false;
    public void Show() => IsActive = true;

}
