using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitPortraitButton : MonoBehaviour
{
    private Unit _parentUnit;

    public void Initialize(Unit unit)
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(unit.OnSelect);
    }

    void Update()
    {
        
    }
}
