using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deal", menuName = "ScriptableObjects/Deal", order = 1)]
public class Deal : ScriptableObject
{
    [Header("Cena (co gracz straci)")]
    [SerializeField] private StatName statToLose;
    [SerializeField] private int loseAmount;
    
    [Header("***** Uzupe³nij tylko jedno, albo skilla albo statystyki *****",order = 0)]
    //Skill ma pierwszeñstwo nad statami, jeœli ustawione s¹ oba to bierze tylko skilla
    [Header("Skill",order = 1)]
    [SerializeField] private SkillData skillToGain = null;
    [Header("Statystyka")]
    [SerializeField] private StatName statToGain;
    [SerializeField] private int gainAmount;

    public StatName StatToLose { get => statToLose;}
    public int LoseAmount { get => loseAmount;}
    public SkillData SkillToGain { get => skillToGain;}
    public StatName StatToGain { get => statToGain;}
    public int GainAmount { get => gainAmount;}
}