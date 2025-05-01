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


    private InventoryManager inventoryManager;
    private int currentPage;
    private int maxPage;
    void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        currentPage = 0;
        maxPage = Mathf.CeilToInt((float)(inventoryManager.GetInventory().Count / ItemsPerPage));

        UpdatePage();
    }

    public void NextPage()
    {
        currentPage++;
        CheckPage();
    }

    public void PreviousPage()
    {
        currentPage--;
        CheckPage();
    }

    public void SetPage(int page)
    {
        currentPage = page;
        CheckPage();
    }

    public void UpdatePage()
    {
        var inventory = inventoryManager.GetInventory();

        Debug.Log(itemView == null);

        while (itemView.transform.childCount > 0)
        {
            Destroy(itemView.transform.GetChild(0).gameObject);
        }

        Debug.Log("Inventory size: "+inventory.Count);

        for(int i=0;i<ItemsPerPage;i++)
        {
            int itemIndex = i + (currentPage * ItemsPerPage);
            if (itemIndex >= inventory.Count)
                break;

            KeyValuePair<Item,int> pair = inventory.ElementAt(itemIndex);

            Debug.Log("Item:" + pair.Key.Name);

            GameObject itemUI = Instantiate(itemUIPrefab);
            itemUI.transform.SetParent(itemView.transform, false);
            itemUI.GetComponent<ItemHolder>().SetItem(pair.Key);
            itemUI.GetComponent<Image>().sprite = pair.Key.Image;
            itemUI.GetComponentInChildren<TextMeshProUGUI>().text = pair.Value.ToString();

            Debug.Log(i);
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
