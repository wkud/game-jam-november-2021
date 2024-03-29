using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] [Range(0, 3)] int _id;
    [SerializeField] Image portrait;
    [SerializeField] Slider slider;

    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI initiativeText;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI defenceText;
    [SerializeField] TextMeshProUGUI critChanceText;
    [SerializeField] TextMeshProUGUI threatText;

    [SerializeField] Image skill1Sprite;
    [SerializeField] Image skill2Sprite;
    [SerializeField] Image skill3Sprite;

    [SerializeField] Image buff1Sprite;
    [SerializeField] Image buff2Sprite;
    [SerializeField] Image debuff1Sprite;
    [SerializeField] Image debuff2Sprite;
    // Start is called before the first frame update
    public void Initialize()
    {
        GameController.Instance.OnStatChanged -= UpdateUnitStatsUI;
        GameController.Instance.OnSkillChanged -= UpdateUnitSkillUI;
        GameController.Instance.OnStatChanged += UpdateUnitStatsUI;
        GameController.Instance.OnSkillChanged += UpdateUnitSkillUI;
    }

    void UpdateUnitStatsUI(int unitID, Player unit)
    {
        if (_id == unitID)
        {
            hpText.text=unit.Stats.MaxHp.ToString();
            initiativeText.text = unit.Stats.Initiative.ToString();
            attackText.text = unit.Stats.AttackModifier.ToString();
            defenceText.text = unit.Stats.Defence.ToString();
            critChanceText.text = unit.Stats.CritChance.ToString();
            threatText.text = unit.Stats.Threat.ToString();

            slider.value = (float)unit.Stats.CurrentHp / unit.Stats.MaxHp;

            portrait.sprite = unit.Stats.Sprite;
        }
    }
    void UpdateUnitSkillUI(int playerId, int skillSlotId, Sprite sprite)
    {
        if (_id == playerId)
        {
            if (skillSlotId == 0)
            {
                if (skill1Sprite!=null) skill1Sprite.sprite = sprite;
            }
            else if (skillSlotId == 1)
            {
                if (skill2Sprite != null) skill2Sprite.sprite = sprite;
            }
            else if (skillSlotId == 2)
            {
                if (skill3Sprite != null) skill3Sprite.sprite = sprite;
            }
        }
    }
}
