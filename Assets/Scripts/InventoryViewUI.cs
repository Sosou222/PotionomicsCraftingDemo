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
    void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        currentPage = 0;
        maxPage = Mathf.CeilToInt((float)inventoryManager.GetInventory().Count / ItemsPerPage);

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
        var inventory = inventoryManager.GetInventory();

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
}
