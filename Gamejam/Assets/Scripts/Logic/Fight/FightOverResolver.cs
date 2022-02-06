using UnityEngine;
using System;

[Serializable]
public class FightOverResolver 
{
    private IUnitReferenceHolder _unitManager;
    private FightOverUi _fightOverUi;

    public FightOverResolver(IUnitReferenceHolder unitManager)
    {
        _unitManager = unitManager;

        _fightOverUi = GameObject.FindObjectOfType<FightOverUi>();
        _fightOverUi.gameObject.SetActive(false);
    }

    public void OnFightEnd()
    {
        _fightOverUi.gameObject.SetActive(true);
        _fightOverUi.HideMainCanvas();

        if (HaveCharacterWon())
        {
            _fightOverUi.ShowWinPanel();
        }
        else if (HaveCharacterLose())
        {
            _fightOverUi.ShowLosePanel();
        }

        // TODO add separated UI for "Game over" on lose + Quit / Restart button
        // TODO add separated UI for "Each character gain 5-10 points in random stat" // 5 for casual, 10 for elite
    }

    public bool IsFightOver() => HaveCharacterWon() || HaveCharacterLose();

    public bool HaveCharacterWon() => _unitManager.ActiveEnemyUnits.Count == 0;

    public bool HaveCharacterLose() => _unitManager.ActiveAllyUnits.Count == 0;

}
