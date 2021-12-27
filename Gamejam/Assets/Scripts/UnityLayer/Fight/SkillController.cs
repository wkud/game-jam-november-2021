using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour, IDescriptable
{
    private Unit _unit;
    private Image _image;
    private Button _button;

    [SerializeField] private int _skillSlotNumber; // values from 0 to 2 (for 3 slots)
    public int SkillSlotNumber => _skillSlotNumber;

    private Player _player => (_unit?.Entity as Player);


    private bool _isInteractable = true;
    public bool IsInteractable
    {
        get => _isInteractable;
        private set
        {
            _isInteractable = value;
            _button.interactable = _isInteractable;
        }
    }

    public string Description => _player?.GetSkill(_skillSlotNumber)?.GetTooltipDescription();

    public string Title => _player?.GetSkill(_skillSlotNumber)?.GetTooltipTitle();

    public bool IsShowable => _isInteractable;


    public void Initialize(Unit unit)
    {
        _unit = unit;
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnClick);
    }

    public void OnClick() => _unit.OnSkillClick(_skillSlotNumber);

    public void SetInteractable(bool interactable)
    {
        IsInteractable = interactable;
    }

    public void UpdateSprite()
    {
        _image.sprite = _player.GetSkill(_skillSlotNumber).Data.Sprite;
    }
}
