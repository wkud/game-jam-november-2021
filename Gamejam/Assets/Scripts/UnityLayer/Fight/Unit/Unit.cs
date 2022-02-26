using UnityEngine;

public class Unit : MonoBehaviour
{
    private FightController _fightController;

    private UnitSkillManager _skillManager;
    private UnitPortrait _portraitButton;

    public Entity Entity { get; private set; }

    public bool IsActive
    {
        get => gameObject.activeSelf;
        private set => gameObject.SetActive(value);
    }

    public bool IsCurrentTurnMaker
    {
        get => _portraitButton.IsCurrentTurnMaker;
        set => _portraitButton.IsCurrentTurnMaker = value;
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

        var hpBar = GetComponentInChildren<HpBar>();
        hpBar.Initialize(Entity);

        SetupOnDeathEffects();

        _portraitButton = GetComponentInChildren<UnitPortrait>();
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

    public void HighlightSkill(int skillIndex, bool shouldBeHighlighted) => _skillManager.HighlightSkill(skillIndex, shouldBeHighlighted);

    private void SetupOnDeathEffects()
    {
        if (Entity is Enemy)
        {
            Entity.OnDeath.AddListener(() =>
            {
                _fightController.OnEntityDied(Entity);
                Hide();
            });

        }
        else if (Entity is Player)
        {
            Entity.OnDeath.AddListener(() =>
            {
                _fightController.OnEntityDied(Entity);
               
                Entity.Stats.Sprite = GameController.Instance.Resources.DeadCharacterPortrait;
                _portraitButton.UpdatePortraitImage();
            });
        }
    }

    public void OnPortraitClick()
    {
        if (_fightController.PlayerTurnState == PlayerTurnState.WaitingForTarget)
        {
            _fightController.OnSelectTarget(this);
        }
    }

    public void OnSkillClick(int skillIndex)
    {
        if (_fightController.PlayerTurnState == PlayerTurnState.WaitingForSkill)
        {
            _fightController.OnSelectSkill(skillIndex);
        }
    }


    public void Hide() => IsActive = false;
    public void Show() => IsActive = true;

}
