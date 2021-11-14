using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltarController : MonoBehaviour
{
    public static AltarController Instance = null;

    [SerializeField] Deal[] allDeals;
    public Deal[] availableDeals = new Deal[4];

    public int offeringID = -1;
    public bool dealIsSkill = false;

    public int skillSlotID;

    public Image[] sacrifices;
    public Sprite[] sacrificeImages = new Sprite[6];
    public Image[] gains;

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
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    public void TurnChildrenOn()
    {
        GetNewDeals();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    public void ProcessStatDeal(int playerId)
    {
        Transform firstChild = transform.GetChild(0);
        if (offeringID != -1 && firstChild.gameObject.activeSelf && dealIsSkill == false)
        {
            GameController.Instance.ChangeCharacterStat(availableDeals[offeringID].price.statName, availableDeals[offeringID].price.amount, playerId);
            StatChange profit = (StatChange)availableDeals[offeringID].profit;
            GameController.Instance.ChangeCharacterStat(profit.statName, profit.amount, playerId);
            TurnChildrenOff();
        }
    }

    public void ChangeSkillSlotID(int id)
    {
        skillSlotID = id;
    }
    public void ProcessSkillDeal(int playerId)
    {
        Transform firstChild = transform.GetChild(0);
        if (offeringID != -1 && firstChild.gameObject.activeSelf && dealIsSkill == true)
        {
            GameController.Instance.ChangeCharacterStat(availableDeals[offeringID].price.statName, availableDeals[offeringID].price.amount, playerId);
            SkillGain profit = (SkillGain)availableDeals[offeringID].profit;
            Skill skill = SkillFactory.CreateSkill(profit.skillData);
            GameController.Instance.ChangeCharacterSkill(playerId,skillSlotID,skill);
            TurnChildrenOff();
        }
    }

    public void GetNewDeals()
    {
        offeringID = -1;

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
            switch (availableDeals[i].price.statName)
            {
                case StatName.Hp:
                    sacrifices[i].sprite = sacrificeImages[0];
                    break;
                case StatName.CurrentHp:
                    sacrifices[i].sprite = sacrificeImages[0];
                    break;
                case StatName.Initiative:
                    sacrifices[i].sprite = sacrificeImages[1];
                    break;
                case StatName.AttackModifier:
                    sacrifices[i].sprite = sacrificeImages[2];
                    break;
                case StatName.Defence:
                    sacrifices[i].sprite = sacrificeImages[3];
                    break;
                case StatName.CritChance:
                    sacrifices[i].sprite = sacrificeImages[4];
                    break;
                case StatName.Threat:
                    sacrifices[i].sprite = sacrificeImages[5];
                    break;                
            }
            if (availableDeals[i].profit is StatChange)
            {
                switch (availableDeals[i].price.statName)
                {
                    case StatName.Hp:
                        gains[i].sprite = sacrificeImages[0];
                        break;
                    case StatName.CurrentHp:
                        gains[i].sprite = sacrificeImages[0];
                        break;
                    case StatName.Initiative:
                        gains[i].sprite = sacrificeImages[1];
                        break;
                    case StatName.AttackModifier:
                        gains[i].sprite = sacrificeImages[2];
                        break;
                    case StatName.Defence:
                        gains[i].sprite = sacrificeImages[3];
                        break;
                    case StatName.CritChance:
                        gains[i].sprite = sacrificeImages[4];
                        break;
                    case StatName.Threat:
                        gains[i].sprite = sacrificeImages[5];
                        break;
                }
            }
        }
    }

    public void RejectDeal()
    {
        if (dealIsSkill == false)
        {
            GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, -10, 0);
            GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, -10, 1);
            GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, -10, 2);
            GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, -10, 3);
        }
        TurnChildrenOff();
    }
}

