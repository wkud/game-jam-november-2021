using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarController : MonoBehaviour
{
    public static AltarController Instance = null;

    [SerializeField] Deal[] allDeals;
    public Deal[] availableDeals = new Deal[4];

    public int offeringID = -1;
    public bool dealIsSkill = false;


    public int skillSlotID;
    // Start is called before the first frame update
    void Start()
    {
        GetNewDeals();
        if (AltarController.Instance != null) Destroy(gameObject);
        else AltarController.Instance = this;
    }

    public void ChangeOfferingID(int id)
    {
        offeringID = id;
        if (availableDeals[id].profit is SkillGain)
        {
            dealIsSkill = true;
        }
        else dealIsSkill = false;
        //switch buttons
    }

    public void TurnChildrenOff()
    { }
    public void TurnChildrenOn()
    { }
    public void DeactivatePlayerButtons()
    { }
    public void ActivatePlayerButtons()
    { }
    public void ProcessStatDeal(int playerId)
    {
        if (dealIsSkill == false)
        {
            GameController.Instance.ChangeCharacterStat(availableDeals[offeringID].price.statName, availableDeals[offeringID].price.amount, playerId);
            StatChange profit = (StatChange)availableDeals[offeringID].profit;
            GameController.Instance.ChangeCharacterStat(profit.statName, profit.amount, playerId);
        }
    }

    public void ChangeSkillSlotID(int id)
    {
        skillSlotID = id;
    }
    public void ProcessSkillDeal(int playerId)
    {
        if (dealIsSkill == true)
        {
            GameController.Instance.ChangeCharacterStat(availableDeals[offeringID].price.statName, availableDeals[offeringID].price.amount, playerId);
            SkillGain profit = (SkillGain)availableDeals[offeringID].profit;
            Skill skill = SkillFactory.CreateSkill(profit.skillData);
            GameController.Instance.ChangeCharacterSkill(playerId,skillSlotID,skill);
        }
    }

    public void GetNewDeals()
    {
        int dealId1 = -1;
        int dealId2 = -1;
        int dealId3 = -1;
        int dealId4 = -1;
        for (int i = 0; i < 4; i++)
        {
            int id;
            do
            {
                id = Random.Range(0, allDeals.Length);
            }
            while (id == dealId1 || id == dealId2 || id == dealId3 || id == dealId4);
            availableDeals[i] = allDeals[id];
            
        }
    }
}

