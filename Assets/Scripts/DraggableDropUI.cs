using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableDropUI : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggable = eventData.pointerDrag.gameObject;
        Debug.Log("Dropped item:" + draggable.name);

        draggable.transform.SetParent(transform, true);

    }
}
