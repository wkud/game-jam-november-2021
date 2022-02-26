using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour, IDescriptable
{
    [SerializeField] private StatName _statName;
    private Unit _unit;
    
    private TextMeshProUGUI _text;
    private Image _image;

    public string Description => "";

    public bool IsShowable => true;

    public string Title => _player.GetStatDescription(_statName);

    private Player _player => _unit.Entity as Player;

    public void Initialize(Unit unit)
    {
        _unit = unit;
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _image = GetComponent<Image>();

        _player.OnStatChanged.AddListener(UpdateStatUI);
        UpdateStatUI(_statName);
        SetIcon();
    }

    private void UpdateStatUI(StatName statName)
    {
        if (_statName == statName)
        {
            var newStatValue = _player.GetStat(statName);
            _text.text = newStatValue.ToString();
        }
    }
    private void SetIcon()
    {
        _image.sprite = GameController.Instance.Resources.GetStatSprite(_statName);
    }
}
