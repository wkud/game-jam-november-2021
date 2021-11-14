using UnityEngine;

public class Unit : MonoBehaviour
{
    private FightController _fightController;

    public IEntity Entity { get; private set; }

    private bool _isActive = true;
    public bool IsActive { get => _isActive; 
        private set 
        {
            _isActive = value;
            gameObject.SetActive(_isActive);
        } 
    }

    public void Initialize(FightController fightController, IEntity entity)
    {
        _fightController = fightController;

        Show();

        Entity = entity;

        var portraitButton = GetComponentInChildren<UnitPortraitButton>();
        portraitButton.Initialize(this);

        var skillButtons = GetComponentsInChildren<SkillButton>();
        foreach (var skillButton in skillButtons)
        {
            skillButton.Initialize(this);
        }
    }

    public void OnPortraitClick()
    {
        if (_fightController.PlayerTurnState == PlayerTurnState.WaitingForTarget)
        {
            Debug.Log("Portrait click " + this);
            _fightController.OnSelectTarget(this);
        }
    }
    
    public void OnSkillClick(int skillIndex)
    {
        if (_fightController.PlayerTurnState == PlayerTurnState.WaitingForSkill)
        {
            Debug.Log("Skill click " + skillIndex);
            _fightController.OnSelectSkill(skillIndex);
        }
    }

    public void Hide() => IsActive = false;
    public void Show() => IsActive = true;

}
