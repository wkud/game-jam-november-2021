using System.Linq;

public class FightUiUpdater : IFightUiUpdater
{
    private IUnitReferenceHolder _unitManager;

    public FightUiUpdater(IUnitReferenceHolder unitManager)
    {
        _unitManager = unitManager;
    }

    public void LockAllSkills()
    {
        foreach (var playerUnit in _unitManager.ActiveAllyUnits)
        {
            playerUnit.AreSkillsInteractable = false;
        }
    }

    public void LockAllTargets()
    {
        foreach (var unit in _unitManager.ActiveUnits)
        {
            unit.IsPortraitInteractable = false;
        }
    }

    public void UnlockSkills(Player currentPlayer)
    {
        var unit = _unitManager.ActiveAllyUnits.FirstOrDefault(u => u.Entity == currentPlayer);
        unit.AreSkillsInteractable = true;
    }

    public void UnlockTargets(Bond skillTargetBond)
    {
        var targetGroup = skillTargetBond == Bond.Ally ? _unitManager.ActiveAllyUnits : _unitManager.ActiveEnemyUnits;
        foreach (var unit in targetGroup)
        {
            unit.IsPortraitInteractable = true;
        }
    }

    public void SetHighlightToCurrentUnit(Entity currentEntity, bool shoudlBeHighlighted)
    {
        var unit = _unitManager.GetUnitOfEntity(currentEntity);
        unit.IsCurrentTurnMaker = shoudlBeHighlighted;
    }

    public void SetHighlightToSelectedSkill(Player currentPlayer, int skillIndex, bool shouldBeHighlighted)
    {
        var unit = _unitManager.GetUnitOfEntity(currentPlayer);
        unit.HighlightSkill(skillIndex, shouldBeHighlighted);
    }

    
}