using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnDraggableUI : MonoBehaviour, IInitializePotentialDragHandler,IDragHandler
{
    [SerializeField] private GameObject draggablePrefab;

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        Debug.Log("Initalizaing Potenital Drag");
        GameObject draggable = Instantiate(draggablePrefab);
        draggable.transform.SetParent(transform.root, false); //Set parent to canvas

        eventData.pointerDrag = draggable;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}
