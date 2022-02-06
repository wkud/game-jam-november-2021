using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightOverUi : MonoBehaviour
{

    private GameObject _winPanel;
    private GameObject _losePanel;

    private void Awake()
    {
        _winPanel = GameObject.FindGameObjectWithTag("WinPanel");
        _losePanel = GameObject.FindGameObjectWithTag("LosePanel");

        _winPanel.SetActive(false); // hide them at the start of the fight to reveal them later
        _losePanel.SetActive(false);
    }

    public void HideMainCanvas()
    {
        var fightCanvas = GameObject.FindGameObjectWithTag("FightCanvas");
        fightCanvas.SetActive(false); // hide main fight canvas
    }

    public void ShowWinPanel()
    {
        _winPanel.SetActive(true);
    }

    public void ShowLosePanel()
    {
        _losePanel.SetActive(true);
    }
}
