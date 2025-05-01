using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    

    private Dictionary<Item,int> inventory = new Dictionary<Item, int>();
    public void AddItem(Item item,int count)
    {
        if(!inventory.ContainsKey(item))
        {
            inventory.Add(item, 0);
        }
        inventory[item] += count;
    }

    public void RemoveItem(Item item,int count) {
        inventory[item] -= count;
        if (inventory[item] <= 0)
        {
            inventory.Remove(item);
        }
    }

    public Dictionary<Item, int> GetInventory()
    {
        return inventory;
    }
}
