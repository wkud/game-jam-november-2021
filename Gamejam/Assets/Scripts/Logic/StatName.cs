using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum StatName
{
    [Description("Max hp")] MaxHp,
    [Description("Current hp")] CurrentHp,
    [Description("Initiative")] Initiative,
    [Description("Attack")] Attack,
    [Description("Defence")] Defence,
    [Description("Crit chance")] CritChance,
    [Description("Threat")] Threat
}
