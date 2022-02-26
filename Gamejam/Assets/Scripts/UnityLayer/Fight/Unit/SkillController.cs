using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour, IDescriptable
{
    [SerializeField] private Image _isSelectedFrame;

    public bool IsSelected
    {
        get => _isSelectedFrame.gameObject.activeSelf;
        set => _isSelectedFrame.gameObject.SetActive(value);
    }

    private Unit _unit;
    private Image _image;
    private Button _button;

    [SerializeField] private int _skillSlotNumber; // values from 0 to 2 (for 3 slots)
    public int SkillSlotNumber => _skillSlotNumber;

    private Player _player => (_unit?.Entity as Player);

    private Skill _skill => _player?.GetSkill(_skillSlotNumber);

    private bool _isNotEmptySkillSlot => _skill != null;

    public bool IsInteractable
    {
        get => _button.interactable && _isNotEmptySkillSlot;
        set => _button.interactable = value && _isNotEmptySkillSlot;
    }

    public string Description => _skill?.GetTooltipDescription();

    public string Title => _skill?.GetTooltipTitle();

    public bool IsShowable => IsInteractable;


    public void Initialize(Unit unit)
    {
        _unit = unit;
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnClick);
    }

    public void OnClick() => _unit.OnSkillClick(_skillSlotNumber);

    public void UpdateSprite() => _image.sprite = _skill.Data.Sprite;
}
