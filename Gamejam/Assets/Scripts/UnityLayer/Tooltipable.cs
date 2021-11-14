using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltipable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData data)
    {
        Tooltip.ShowTooltip_Static(this.gameObject.name);
    }

    public void OnPointerExit(PointerEventData data)
    {
        Tooltip.HideTooltip_Static();
    }
}
