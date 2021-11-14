using System.Linq;

public class UnitSkillManager 
{

    private Unit _unit;
    private SkillController[] _skillControllers;

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
                controller.SetInteractable(false); // turn of this button
            }
            else
            {
                controller.UpdateSprite();
            }
        }
    }
    
}
