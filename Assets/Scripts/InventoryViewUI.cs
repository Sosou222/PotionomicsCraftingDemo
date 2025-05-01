using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject itemView;
    [SerializeField] private GameObject pageView;
    [SerializeField] private GameObject filtersView;
    [Header("Settings")]
    [SerializeField] private int ItemsPerPage;
    [Header("Prefabs")]
    [SerializeField] private GameObject itemUIPrefab;
    [SerializeField] private GameObject buttonPagePrefab;


    private InventoryManager inventoryManager;
    private int currentPage;
    private int maxPage;
    private CheckForFilter checkForFilter;


    void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        currentPage = 0;
        maxPage = Mathf.CeilToInt((float)inventoryManager.GetInventory().Count / ItemsPerPage);
        checkForFilter = new CheckForFilter();

        UpdatePage();

        PopulatePageView();
    }

    private void PopulatePageView()
    {
        for(int i=0;i<maxPage;i++)
        {
            GameObject button = Instantiate(buttonPagePrefab);
            button.transform.SetParent(pageView.transform.GetChild(0), false);
            int page = i;
            button.GetComponent<Button>().onClick.AddListener(
                () => SetPage(page)
                );
        }
    }

    public void NextPage()
    {
        SetPage(currentPage+1);
    }

    public void PreviousPage()
    {
        SetPage(currentPage-1);
    }

    public void SetPage(int page)
    {
        currentPage = page;
        Debug.Log("Current Page:" + page);
        CheckPage();
        UpdatePage();
    }

    public void UpdatePage()
    {
        var inventory = GetFilteredItems();

        while (itemView.transform.childCount > 0)
        {
            DestroyImmediate(itemView.transform.GetChild(0).gameObject);
        }

        for (int i=0;i<ItemsPerPage;i++)
        {
            int itemIndex = i + (currentPage * ItemsPerPage);
            if (itemIndex >= inventory.Count)
                break;

            KeyValuePair<Item,int> pair = inventory.ElementAt(itemIndex);

            GameObject itemUI = Instantiate(itemUIPrefab);
            itemUI.transform.SetParent(itemView.transform, false);
            itemUI.GetComponent<ItemHolder>().SetItem(pair.Key);
            itemUI.GetComponent<Image>().sprite = pair.Key.Image;
            itemUI.GetComponentInChildren<TextMeshProUGUI>().text = pair.Value.ToString();
        }
    }

    public void ToggleMagimins(int enumNum)
    {
        Magimins magimins = (Magimins)enumNum;
        State state = checkForFilter.magiminsState[magimins];
        int lenght = Enum.GetValues(state.GetType()).Length;
        int index = (int)state;
        checkForFilter.magiminsState[magimins] = index+1 < lenght? (State)(index+1) : (State)(0);

        UpdatePage();
    }

    public void ToggleTrait(int enumNum)
    {
        Trait trait = (Trait)enumNum;
        State state = checkForFilter.traitState[trait];
        int lenght = Enum.GetValues(state.GetType()).Length;
        int index = (int)state;
        checkForFilter.traitState[trait] = index + 1 < lenght ? (State)(index + 1) : (State)(0);

        UpdatePage();
    }

    private Dictionary<Item,int> GetFilteredItems()
    {
        Dictionary<Item, int> inventory = inventoryManager.GetInventory();

        foreach(var maginimState in checkForFilter.magiminsState)
        {
            switch (maginimState.Value) 
            {
                case State.Any:
                    continue;
                case State.Has:
                    inventory = inventory.
                                Where(item => item.Key.HasMagimins(maginimState.Key))
                                .ToDictionary(item => item.Key,item=> item.Value);
                    break;
                case State.NotHave:
                    inventory = inventory.
                                Where(item => !item.Key.HasMagimins(maginimState.Key))
                                .ToDictionary(item => item.Key, item => item.Value);
                    break;
            }
        }

        foreach (var traitState in checkForFilter.traitState)
        {
            switch (traitState.Value)
            {
                case State.Any:
                    continue;
                case State.Has:
                    inventory = inventory.
                                Where(item => item.Key.HasGoodTrait(traitState.Key))
                                .ToDictionary(item => item.Key, item => item.Value);
                    break;
                case State.NotHave:
                    inventory = inventory.
                                Where(item => !item.Key.HasGoodTrait(traitState.Key))
                                .ToDictionary(item => item.Key, item => item.Value);
                    break;
            }
        }

        return inventory;
    }

    private void CheckPage()
    {
        if (currentPage > maxPage)
        {
            currentPage = maxPage;
        }
        if (currentPage < 0)
        {
            currentPage = 0;
        }
    }

    private enum State
    {
        Any,
        Has,
        NotHave
    }

    private class CheckForFilter
    {
        public Dictionary<Magimins, State> magiminsState = new Dictionary<Magimins, State>()
        {
            { Magimins.A,State.Any},
            { Magimins.B,State.Any},
            { Magimins.C,State.Any},
            { Magimins.D,State.Any},
            { Magimins.E,State.Any},

        };

        public Dictionary<Trait, State> traitState = new Dictionary<Trait, State>()
        {
            { Trait.Sensation,State.Any },
            { Trait.Armora,State.Any },
            { Trait.Taste,State.Any },
            { Trait.Visual,State.Any },
            { Trait.Sound,State.Any },
        };

    }
}