using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public List<Item> itemList;

    void Start()
    {
        InventoryManager inventoryManager = FindAnyObjectByType<InventoryManager>();
        foreach(Item item in itemList)
        {
            inventoryManager.AddItem(item, Random.Range(1, 5));
        }
    }

    public void Say()
    {
        Debug.Log("Say");
    }
}
