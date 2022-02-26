using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeAvatarController : MonoBehaviour
{
    private const float _enlargedSize = 150;
    private float _initialSize; // set on initialization

    private Vector2 _initialSizeVector => new Vector2(_initialSize, _initialSize);
    private Vector2 _enlargedSizeVector => new Vector2(_enlargedSize, _enlargedSize);

    private RectTransform _rect;


    [SerializeField] private Image _isCurrentFrame;

    public bool IsCurrentTurnMaker
    {
        get => _isCurrentFrame.gameObject.activeSelf;
        set
        {
            _isCurrentFrame.gameObject.SetActive(value);
            _rect.sizeDelta = value ? _enlargedSizeVector : _initialSizeVector;
        }
    }

    public Entity Entity { get; private set; }

    private Image _image;
    public void Initialize(Entity entity)
    {
        Entity = entity;
        _image = GetComponent<Image>();
        _image.sprite = entity.Stats.Sprite;
        _rect = GetComponent<RectTransform>();

        _initialSize = _rect.sizeDelta.x;
    }
}
