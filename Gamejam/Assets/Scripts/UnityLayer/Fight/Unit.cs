using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    private FightController _fightController;

    private UnitSkillManager _skillManager;
    private UnitPortraitButton _portraitButton;

    public Entity Entity { get; private set; }

    public bool IsActive
    {
        get => gameObject.activeSelf;
        private set => gameObject.SetActive(value);
    }

    public bool IsPortraitInteractable
    {
        get => _portraitButton.IsInteractable;
        set => _portraitButton.IsInteractable = value;
    }

    public bool AreSkillsInteractable
    {
        get => _skillManager.AreSkillsInteractable;
        set => _skillManager.AreSkillsInteractable = value;
    }

    public void Initialize(FightController fightController, Entity entity)
    {
        _fightController = fightController;
        Entity = entity;

        Show();

        _portraitButton = GetComponentInChildren<UnitPortraitButton>();
        _portraitButton.Initialize(this);

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
            //Debug.Log("Portrait click " + this);
            _fightController.OnSelectTarget(this);
        }
    }

    public void OnSkillClick(int skillIndex)
    {
        if (_fightController.PlayerTurnState == PlayerTurnState.WaitingForSkill)
        {
            //Debug.Log("Skill click " + skillIndex);
            _fightController.OnSelectSkill(skillIndex);
        }
    }

    

    public void Hide() => IsActive = false;
    public void Show() => IsActive = true;

}
