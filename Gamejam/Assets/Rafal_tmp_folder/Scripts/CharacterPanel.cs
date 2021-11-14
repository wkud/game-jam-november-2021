using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] [Range(0, 3)] int _id;

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
        GameController.Instance.OnStatChanged += UpdateUnitStatsUI;
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


            /*if (unit.Skills[0]!=null && unit.Skills[0].Data.Sprite!=null) skill1Sprite.sprite = unit.Skills[0].Data.Sprite;
            if (unit.Skills[0] != null && unit.Skills[1].Data.Sprite != null) skill2Sprite.sprite = unit.Skills[1].Data.Sprite;
            if (unit.Skills[0] != null && unit.Skills[2].Data.Sprite != null) skill3Sprite.sprite = unit.Skills[2].Data.Sprite;*/
        }
    }
}
