using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DealSkill", menuName = "ScriptableObjects/DealSkill", order = 1)]
public class SkillGain : Gain
{
    [SerializeField] SkillData skillData;
}
