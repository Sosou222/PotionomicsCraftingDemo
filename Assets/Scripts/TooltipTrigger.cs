using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Item item = GetComponent<ItemHolder>().GetItem();
        Tooltip.ShowTooltip(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip();
    }
}
