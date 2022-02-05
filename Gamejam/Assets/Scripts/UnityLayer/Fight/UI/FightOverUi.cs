using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightOverUi : MonoBehaviour
{

    public FightOverUi()
    {
        // _text = GetComponentInChildren<TMPText>();
    }

    public void SetMessage(string message)
    {
        // _text.text = message;
    }

    internal void HideMainCanvas()
    {
        var fightCanvas = GameObject.FindGameObjectWithTag("FightCanvas");
        fightCanvas.SetActive(false); // hide main fight canvas
    }
}
