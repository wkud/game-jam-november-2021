using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitPortraitButton : MonoBehaviour
{
    private Unit _unit;

    public void Initialize(Unit unit)
    {
        _unit = unit;

        var sprite = _unit.Entity.Stats.Sprite;
        if (sprite != null)
        {
            GetComponent<Image>().overrideSprite = sprite;
        }


        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick() => _unit.OnPortraitClick();
}
