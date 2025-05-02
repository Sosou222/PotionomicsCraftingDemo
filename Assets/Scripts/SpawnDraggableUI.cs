using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnDraggableUI : MonoBehaviour, IInitializePotentialDragHandler,IDragHandler
{
    [SerializeField] private GameObject draggablePrefab;

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        Debug.Log("Initalizaing Potenital Drag");
        GameObject draggable = Instantiate(draggablePrefab);
        draggable.transform.SetParent(transform.root, false); //Set parent to canvas

        Item item = GetComponent<ItemHolder>().GetItem();
        draggable.GetComponent<ItemHolder>().SetItem(item);
        draggable.GetComponent<Image>().sprite = item.Image;

        InventoryManager.Instance.RemoveItem(item, 1);

        eventData.pointerDrag = draggable;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}
