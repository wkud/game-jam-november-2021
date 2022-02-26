using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitPortrait : MonoBehaviour
{
    private Unit _unit;

    private Button _button;
    private Image _image;

    [SerializeField] private Image _isActiveFrame;
    [SerializeField] private Image _isTargetableFrame;

    public bool IsCurrentTurnMaker
    {
        get => _isActiveFrame.gameObject.activeSelf;
        set => _isActiveFrame.gameObject.SetActive(value);
    }

    private bool _isTargetable
    {
        //get => _isTargetableFrame.gameObject.activeSelf;
        set => _isTargetableFrame.gameObject.SetActive(value);
    }

    public bool IsInteractable
    {
        get => _button.interactable;
        set
        {
            _button.interactable = value;
            _isTargetable = value;
        }

    }

    public void Initialize(Unit unit)
    {
        _unit = unit;

        _image = GetComponent<Image>();

        var sprite = _unit.Entity.Stats.Sprite;
        if (sprite != null)
        {
            _image.sprite = sprite;
        }


        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void OnClick() => _unit.OnPortraitClick();

    public void UpdatePortraitImage()
    {
        _image.sprite = _unit.Entity.Stats.Sprite;
    }
}
