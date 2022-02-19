public interface IFightUiUpdater
{
    void LockAllTargets();
    
    void LockAllSkills();

    /// <summary> This is only activated when player is a caster (when it is player's turn) </summary>
    void UnlockTargets(Bond skillTargetBond);

    /// <summary> This is only activated when player is a caster (when it is player's turn) </summary>
    void UnlockSkills(Player currentEntity);
}
