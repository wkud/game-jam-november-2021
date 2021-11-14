using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    private FightController _fightController;

    private UnitSkillManager _skillManager;

    public Entity Entity { get; private set; }

    private bool _isActive = true;
    public bool IsActive
    {
        get => _isActive;
        private set
        {
            _isActive = value;
            gameObject.SetActive(_isActive);
        }
    }

    public void Initialize(FightController fightController, Entity entity)
    {
        _fightController = fightController;
        Entity = entity;

        Show();

        var portraitButton = GetComponentInChildren<UnitPortraitButton>();
        portraitButton.Initialize(this);

        var skillControllers = GetComponentsInChildren<SkillController>();
        foreach (var skillButton in skillControllers)
        {
            skillButton.Initialize(this);
        }

        var statControllers = GetComponentsInChildren<StatController>();
        foreach (var statController in statControllers)
        {
            statController.Initialize(this);
        }

        _skillManager = new UnitSkillManager(this, skillControllers);

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
