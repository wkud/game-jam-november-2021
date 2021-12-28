using UnityEngine;
using UnityEngine.UI;

public class UnitPortraitButton : MonoBehaviour
{
    private Unit _unit;

    private Button _button;

    public bool IsInteractable
    {
        get => _button.interactable;
        set => _button.interactable = value;
    }

    public void Initialize(Unit unit)
    {
        _unit = unit;

        var sprite = _unit.Entity.Stats.Sprite;
        if (sprite != null)
        {
            GetComponent<Image>().sprite = sprite;
        }


        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void OnClick() => _unit.OnPortraitClick();
}
