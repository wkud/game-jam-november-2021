using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitPortraitButton _portraitButton;
    private FightController _fightController;

    public IEntity Entity { get; private set; }

    public void Initialize(FightController fightController, IEntity entity)
    {
        _fightController = fightController;


        Entity = entity;

        _portraitButton = GetComponentInChildren<UnitPortraitButton>();
        _portraitButton.Initialize(this);
    }

    public void OnClick()
    {
        if (_fightController.PlayerTurnState == PlayerTurnState.WaitingForTarget)
        {
            _fightController.OnSelectTarget(this);
        }
    }
    
}
