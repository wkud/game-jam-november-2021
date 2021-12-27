using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeAvatarController : MonoBehaviour
{
    public Entity Entity { get; private set; }

    private Image _image;
    public void Initialize(Entity entity)
    {
        Entity = entity;
        _image = GetComponent<Image>();
        //_image. = entity.Stats.Sprite;
    }
}
