using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    private UnitPortraitButton _portraitButton;
    private FightController _fightController;

    public void Initialize(FightController fightController)
    {
        _fightController = fightController;

        _portraitButton = GetComponentInChildren<UnitPortraitButton>();
        _portraitButton.Initialize(this);
    }

    public void OnSelect()
    {
        if (_fightController.State == FightState.WaitingForTarget)
        {
            _fightController.SetTarget(this);
        }
    }

    void Update()
    {
        
    }


}
