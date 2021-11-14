using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarController : MonoBehaviour
{
    public static AltarController Instance = null;
    
    public StatType[] sacrifices = new StatType[4];
    public int[] sacrificeAmount = new int[4] { -1,-1,-1,-1};

    public StatType[] statGifts = new StatType[4];
    public int[] statGiftAmount = new int[4] { -1, -1, -1, -1 };
    public ISkill[] skillGifts = new ISkill[4];
    [SerializeField] SkillData[] skills;
    [SerializeField,SerializeReference] ISkill[] sk;

    public int offeringID = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (AltarController.Instance != null) Destroy(gameObject);
        else AltarController.Instance = this;
    }

    public void ChangeOfferingID(int id)
    {
        offeringID = id;
        //switch buttons
    }

    public void ResetAltar()
    {
        offeringID = -1;

        sacrifices = new StatType[4];
        sacrificeAmount = new int[4] { -1, -1, -1, -1 };
        statGifts = new StatType[4];
        statGiftAmount = new int[4] { -1, -1, -1, -1 };
        skillGifts = new ISkill[4];
    }

    public void GenerateAltarOfferings()
    {
        //price
        for (int i = 0; i < 4; i++)
        {
            sacrifices[i] = (StatType)Random.Range(0, (int)6);
            sacrificeAmount[i] = Random.Range(1, (int)4);
        }

        for (int i = 0; i < 4; i++)
        {
            if (Random.Range(0, 1f) == 0)//0-stat, 1-skill
            {
                do
                {
                    statGifts[i] = (StatType)Random.Range(0, (int)6);
                }
                while (statGifts[i] == sacrifices[i]);

                statGiftAmount[i] = Random.Range(3, (int)6);
            }
            else
            {
                //skillGifts[i] = //skills[Random.Range(0,skills.Length)];
            }
        }
        
    }
}

