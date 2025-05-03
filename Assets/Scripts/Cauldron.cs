using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private int lastChildCount;
    private List<ItemHolder> itemHolderList;

    public event EventHandler OnItemsChanged;
    private void Start()
    {
        lastChildCount = transform.childCount;
        itemHolderList = new List<ItemHolder>();
    }
    void Update()
    {
        if(lastChildCount != transform.childCount)
        {
            lastChildCount = transform.childCount;
            itemHolderList = GetComponentsInChildren<ItemHolder>().ToList();

            OnItemsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetTotalMagimins()
    {
        int total = 0;
        foreach(ItemHolder itemHolder in itemHolderList)
        {
            total += itemHolder.GetItem().TotalMagimins;
        }
        return total;
    }

    public int GetTotalMagiminsOfType(Magimins magimins)
    {
        int total = 0;
        foreach (ItemHolder itemHolder in itemHolderList)
        {
            total += itemHolder.GetItem().GetMagiminsCountOfType(magimins);
        }
        return total;
    }
    
    public int GetIngredientCount()
    {
        return transform.childCount;
    }
}
