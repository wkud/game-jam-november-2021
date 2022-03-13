using UnityEngine;
using System;

[Serializable]
public class FightOverResolver
{
    private IUnitReferenceHolder _unitManager;
    private FightOverUi _fightOverUi;

    private bool _wasThisEliteEncounter => GameController.Instance.GameState.CurrentEncounterDifficulty == EncounterType.Elite;

    // stat increase range: 10-15 for elite, 5-10 for casual, end game for boss
    private int _maxStatIncrease => _wasThisEliteEncounter ? 8 : 4;
    private int _minStatIncrease => _wasThisEliteEncounter ? 6 : 2;

    public const float HEAL_AFTER_FIGHT_MAX_HP_PERCENT = 0.25f; // every character is healed by 25% of it's max hp

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

            HealPercentOfHpForEveryCharacter();
        }
        else if (HaveCharacterLose())
        {
            _fightOverUi.ShowLosePanel();
        }

        _fightOverUi.SetBackgroundToLight();
    }

    private void HealPercentOfHpForEveryCharacter()
    {
        foreach (var ally in _unitManager.ActiveAllyUnits)
        {
            var healingAmount = ally.Entity.Stats.MaxHp * HEAL_AFTER_FIGHT_MAX_HP_PERCENT;
            ally.Entity.TakeDamage((int)healingAmount * -1); // negative damage = healing
        }
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
