using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltarController : MonoBehaviour
{
    public static AltarController Instance = null;

    [SerializeField] Deal[] allDeals;//TODO przeniesc do resource containera i zrobic refke do GameControllera
    public Deal[] availableDeals = new Deal[4];

    public int offeringID = -1;
    public bool dealIsSkill = false;

    public int skillSlotID;

    public Image[] sacrifices;
    public Sprite[] _statImages = new Sprite[6];
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
        if (availableDeals[id].SkillToGain!=null)
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
            GameController.Instance.ChangeCharacterStat(availableDeals[offeringID].StatToLose, availableDeals[offeringID].LoseAmount, playerId);
            GameController.Instance.ChangeCharacterStat(availableDeals[offeringID].StatToGain, availableDeals[offeringID].GainAmount, playerId);
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
            GameController.Instance.ChangeCharacterStat(availableDeals[offeringID].StatToLose, availableDeals[offeringID].LoseAmount, playerId);
            Skill skill = SkillFactory.CreateSkill(availableDeals[offeringID].SkillToGain);
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
            switch (availableDeals[i].StatToLose)
            {
                case StatName.MaxHp:
                    sacrifices[i].sprite = _statImages[0];//te cyferki odpowiaaja kolejnosci w altar controllerze w insopektorze, handluj z tym
                    break;
                case StatName.CurrentHp:
                    sacrifices[i].sprite = _statImages[0];
                    break;
                case StatName.Initiative:
                    sacrifices[i].sprite = _statImages[1];
                    break;
                case StatName.AttackModifier:
                    sacrifices[i].sprite = _statImages[2];
                    break;
                case StatName.Defence:
                    sacrifices[i].sprite = _statImages[3];
                    break;
                case StatName.CritChance:
                    sacrifices[i].sprite = _statImages[4];
                    break;
                case StatName.Threat:
                    sacrifices[i].sprite = _statImages[5];
                    break;                
            }
            if (availableDeals[i].SkillToGain == null && availableDeals[i].StatToGain!=0)
            {
                switch (availableDeals[i].StatToGain)
                {
                    case StatName.MaxHp:
                        gains[i].sprite = _statImages[0];
                        break;
                    case StatName.CurrentHp:
                        gains[i].sprite = _statImages[0];
                        break;
                    case StatName.Initiative:
                        gains[i].sprite = _statImages[1];
                        break;
                    case StatName.AttackModifier:
                        gains[i].sprite = _statImages[2];
                        break;
                    case StatName.Defence:
                        gains[i].sprite = _statImages[3];
                        break;
                    case StatName.CritChance:
                        gains[i].sprite = _statImages[4];
                        break;
                    case StatName.Threat:
                        gains[i].sprite = _statImages[5];
                        break;
                }
            }
            else if (availableDeals[i].SkillToGain != null)
            {
                gains[i].sprite = availableDeals[i].SkillToGain.Sprite;
            }
            else 
            {
                Debug.LogError("Deal: " + availableDeals[i].name + " doesn't have reward");
            }
        }
    }

    public void RejectDeal()
    {
        GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, -10, 0);
        GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, -10, 1);
        GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, -10, 2);
        GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, -10, 3);
        //if (dealIsSkill == false)
        //{
        //    GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, -10, 0);
        //    GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, -10, 1);
        //    GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, -10, 2);
        //    GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, -10, 3);
        //}
        TurnChildrenOff();
    }
}

