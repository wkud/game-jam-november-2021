using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightOverUi : MonoBehaviour
{

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    
    [SerializeField] private TextMeshProUGUI _winStatMessage;
    [SerializeField] private TextMeshProUGUI _winHealMessage;

    [SerializeField] private Image _background;

    private void Awake()
    {
        _winPanel.SetActive(false); // hide them at the start of the fight to reveal them later
        _losePanel.SetActive(false);

        _winHealMessage.text = _winHealMessage.text.Replace("$", (FightOverResolver.HEAL_AFTER_FIGHT_MAX_HP_PERCENT * 100).ToString());
    }

    public void HideMainCanvas()
    {
        var fightCanvas = GameObject.FindGameObjectWithTag("FightCanvas");
        fightCanvas.SetActive(false); // hide main fight canvas
    }

    public void ShowWinPanel() => _winPanel.SetActive(true);

    public void ShowLosePanel() => _losePanel.SetActive(true);

    public void SetTextToStatMessage(string text) => _winStatMessage.text = text;

    public void OnClickContinue() => GameController.Instance.OpenScene(SceneId.Map);

    public void OnClickQuit() => Application.Quit();

    public void SetBackgroundToLight() => _background.color = Color.white;

}
