using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltipable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData data)
    {
        string description = GetComponent<IDescriptable>()?.Description ?? "";
        Tooltip.ShowTooltip_Static(description);
    }

    public void OnPointerExit(PointerEventData data)
    {
        Tooltip.HideTooltip_Static();
    }
}
