using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltipable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData data)
    {
        var descriptable = GetComponent<IDescriptable>();
        string description = descriptable?.Description ?? "";
        string title = descriptable?.Title ?? "";
        
        if (descriptable?.IsShowable ?? false)
        {
            Tooltip.ShowTooltip_Static(title, description);
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        Tooltip.HideTooltip_Static();
    }
}
