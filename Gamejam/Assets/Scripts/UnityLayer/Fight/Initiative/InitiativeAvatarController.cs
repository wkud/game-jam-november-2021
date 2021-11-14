using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeAvatarController : MonoBehaviour
{
    [SerializeField] private GameObject _border;
    public Entity Entity { get; private set; }

    private Image _image;
    public void Initialize(Entity entity, bool isSelected)
    {
        Entity = entity;
        _image = GetComponent<Image>();
        _image.sprite = entity.Stats.Sprite;
        if (!isSelected) _border.SetActive(false);
    }
}
