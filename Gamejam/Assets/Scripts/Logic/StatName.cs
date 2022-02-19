using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum StatName
{
    [Description("Max Hp")] MaxHp,
    [Description("Current Hp")] CurrentHp,
    [Description("Initiative")] Initiative,
    [Description("Attack")] Attack,
    [Description("Defence")] Defence,
    [Description("Crit Chance")] CritChance,
    [Description("Threat")] Threat
}
