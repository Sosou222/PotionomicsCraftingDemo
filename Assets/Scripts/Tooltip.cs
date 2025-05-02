using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI categoryText;
    [SerializeField] private TextMeshProUGUI totalMagiminsText;
    [SerializeField] private Image image;
    [SerializeField] private List<GameObject> magiminsHolderList = new List<GameObject>();
    [SerializeField] private List<GameObject> traitHolderList = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI descriptionText;


    private static Tooltip instance;
    private void Awake()
    {
        instance = this;
        HideTooltip();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.position = Input.mousePosition;
    }

    public static void ShowTooltip(Item item)
    {
        instance.SetItemInfo(item);
        instance.gameObject.SetActive(true);
        instance.UpdatePosition();
    }

    public static void HideTooltip()
    {
        instance.gameObject.SetActive(false);;
    }

    private void SetItemInfo(Item item)
    {
        nameText.text = item.Name;
        categoryText.text = item.CategoryName;
        totalMagiminsText.text = "Total Magimins:" + item.TotalMagimins;
        image.sprite = item.Image;

        for(int i=0;i<magiminsHolderList.Count;i++)
        {
            GameObject magHold = magiminsHolderList[i];
            magHold.transform.Find("Numer").GetComponent<TextMeshProUGUI>().text = ""+item.GetMagiminsCountOfType((Magimins)i);
        }

        for(int i=0;i<traitHolderList.Count; i++)
        {
            GameObject traitHold = traitHolderList[i];
            Image img = traitHold.transform.Find("Color").GetComponent<Image>();
            if (item.HasGoodTrait((Trait)i)) img.color = Color.green;
            else if (item.HasNegativeTrait((Trait)i)) img.color = Color.red;
            else img.color = Color.white;
        }

        descriptionText.text = item.Description;
    }
}
