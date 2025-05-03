using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionInfo : MonoBehaviour
{
    [SerializeField] private Slider potionSlider;
    [SerializeField] private List<Image> starList = new List<Image>();
    [SerializeField] private TextMeshProUGUI totalMagiminsText;
    [SerializeField] private TextMeshProUGUI totalIgridientsText;
    [SerializeField] private TextMeshProUGUI ratioText;
    [SerializeField] private TextMeshProUGUI potionText;
    
    public void UpdateInfo(int totalIngidients,int totalMagimins,PotionRecipe recipe)
    {
        totalMagiminsText.text = "Total Magimins:" + totalIngidients;
        totalIgridientsText.text = "Total Igridients:" + totalMagimins;
        ratioText.text = totalMagimins + "/60";
        if (recipe == null)
        {
            potionText.text = "None";
        }
        else
        {
            potionText.text = recipe.Name;
        }

    }
}
