using UnityEngine;
using System;

[Serializable]
public class FightOverResolver
{
    private IUnitReferenceHolder _unitManager;
    private FightOverUi _fightOverUi;

    private bool _wasThisEliteEncounter => true; // TODO change this

    // stat increase range: 10-15 for elite, 5-10 for casual, end game for boss
    private int _maxStatIncrease => _wasThisEliteEncounter ? 8 : 4;
    private int _minStatIncrease => _wasThisEliteEncounter ? 6 : 2;


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

            string statMessage = IncreaseRandomStatForeachAlly();
            _fightOverUi.SetTextToStatMessage(statMessage);
        }
        else if (HaveCharacterLose())
        {
            _fightOverUi.ShowLosePanel();
        }

        _fightOverUi.SetBackgroundToLight();
    }

    private string IncreaseRandomStatForeachAlly()
    {
        var randomStat = RandomUtility.GetRandomEnumValueOf<StatName>(StatName.CurrentHp, StatName.Threat); // exclude CurrentHp and Threat from possible stats to increase
        var statIncrease = UnityEngine.Random.Range(_minStatIncrease, _maxStatIncrease + 1);

        foreach (var unit in _unitManager.ActiveAllyUnits)
        {
            (unit.Entity as Player).AddStat(randomStat, statIncrease);
        }

        var statMessage = $"Each character gains {statIncrease} points of {randomStat.GetDescription()}";
        return statMessage;
    }

    public bool IsFightOver() => HaveCharacterWon() || HaveCharacterLose();

    public bool HaveCharacterWon() => _unitManager.ActiveEnemyUnits.Count == 0;

    public bool HaveCharacterLose() => _unitManager.ActiveAllyUnits.Count == 0;

}
