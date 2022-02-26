using TMPro;
using UnityEngine;

public class DamageInfo : MonoBehaviour
{
    private const float _floatingTextSpeed = 0.2f;
    private const float _fadingTextSpeed = 0.1f;

    private float _initialAlpha;

    public bool IsActive
    {
        get => gameObject.activeSelf;
        set => gameObject.SetActive(value);
    }

    private TextMeshProUGUI _text;
    private RectTransform _rect;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _rect = GetComponent<RectTransform>();

        _initialAlpha = _text.alpha;
    }

    public void DisplayDamage(Unit unit, int damage)
    {
        var portraitCenter = unit.GetComponentInChildren<UnitPortrait>().GetComponent<RectTransform>().rect.center;
        _rect.position = portraitCenter;

        _text.text = damage.ToString();

        IsActive = true;
    }

    private void Update()
    {
        if (!IsActive)
            return;

        _rect.position += Vector3.up * _floatingTextSpeed;
        _text.alpha -= _fadingTextSpeed;

        if (_text.alpha < 0.1f)
        {
            IsActive = false;
            _text.alpha = _initialAlpha;
        }
    }

}
