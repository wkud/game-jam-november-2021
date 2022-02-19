using System.Linq;

public class UnitSkillManager 
{

    private Unit _unit;
    private SkillController[] _skillControllers;

    bool _areSkillsInteractable = true;
    public bool AreSkillsInteractable
    {
        get => _areSkillsInteractable;
        set
        {
            _areSkillsInteractable = value;
            SetInteractableToAllSkills(value);
        }
    }

    public UnitSkillManager(Unit unit, SkillController[] skillControllers)
    {
        _unit = unit;
        _skillControllers = skillControllers;

        for (int i = 0; i < skillControllers.Length; i++)
        {
            var controller = skillControllers.FirstOrDefault(c => c.SkillSlotNumber == i);
            var skill = (unit.Entity as Player).GetSkill(i);

            if (skill is null)
            {
                controller.IsInteractable = false; // turn of this button
            }
            else
            {
                controller.UpdateSprite();
            }
        }
    }

    private void SetInteractableToAllSkills(bool interactable)
    {
        foreach (var skillController in _skillControllers)
        {
            skillController.IsInteractable = interactable;
        }
    }

    public void HighlightSkill(int skillIndex, bool shouldBeHighlighted) => _skillControllers[skillIndex].IsSelected = shouldBeHighlighted;
}
