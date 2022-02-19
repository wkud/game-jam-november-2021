using TMPro;
using UnityEngine;

public class FightOverUi : MonoBehaviour
{

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    
    [SerializeField] private TextMeshProUGUI _winStatMessage;

    private void Awake()
    {
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

    public void SetTextToStatMessage(string text)
    {
        _winStatMessage.text = text;
    }

    public void OnClickContinue()
    {
        GameController.Instance.OpenScene(SceneId.Map);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
