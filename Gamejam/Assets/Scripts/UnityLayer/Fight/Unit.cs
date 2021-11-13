using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitPortraitButton _portraitButton;
    private FightController _fightController;

    [SerializeField] EntityStats _entityData;
    public IEntity Entity { get; private set; }

    public void Initialize(FightController fightController)
    {
        _fightController = fightController;

        Entity = EntityFactory.CreateEntity(_entityData);

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
