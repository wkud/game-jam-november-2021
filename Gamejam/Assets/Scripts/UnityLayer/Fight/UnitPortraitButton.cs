using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitPortraitButton : MonoBehaviour
{
    public void Initialize(Unit unit)
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(unit.OnClick);
    }

}
