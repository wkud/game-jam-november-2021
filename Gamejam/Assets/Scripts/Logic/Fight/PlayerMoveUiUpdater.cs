using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoveUiUpdater : IPlayerMoveUiUpdater
{
    private IUnitReferenceHolder _unitManager;

    public PlayerMoveUiUpdater(IUnitReferenceHolder unitManager)
    {
        _unitManager = unitManager;
    }

    public void LockAllSkills()
    {
        foreach (var playerUnit in _unitManager.AllAllyUnits)
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
        var unit = _unitManager.AllAllyUnits.FirstOrDefault(u => u.Entity == currentPlayer);
        unit.AreSkillsInteractable = true;
    }

    public void UnlockTargets(Bond skillTargetBond)
    {
        var targetGroup = skillTargetBond == Bond.Ally ? _unitManager.AllAllyUnits : _unitManager.ActiveEnemyUnits;
        foreach (var unit in targetGroup)
        {
            unit.IsPortraitInteractable = true;
        }
    }
}