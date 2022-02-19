using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightNode : MapNode
{
    [SerializeField] List<Sprite> nodeSprites;
    [SerializeField] SpriteRenderer spriteRenderer;
    public EncounterData EncounterData { get; set; }    
    
    public void SetEncounterSprite(EncounterType encounterType)
    {
        if (encounterType == EncounterType.Normal)
        {
            if (Random.Range(0, 100) < 80) spriteRenderer.sprite = nodeSprites[0];
            else spriteRenderer.sprite = nodeSprites[1];
        }
        else if (encounterType == EncounterType.Elite)
        {
            spriteRenderer.sprite = nodeSprites[2];
        }
        else if (encounterType == EncounterType.Boss)
        {
            spriteRenderer.sprite = nodeSprites[3];
        }
    }

}
public enum EncounterType
{
    Normal,
    Elite,
    Boss
}
