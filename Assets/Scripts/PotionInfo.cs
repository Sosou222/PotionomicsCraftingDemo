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
    
    public void UpdateInfo(int totalIngidients,int totalMagimins,string potionName,float starProcentage)
    {
        totalMagiminsText.text = "Total Magimins:" + totalIngidients;
        totalIgridientsText.text = "Total Igridients:" + totalMagimins;
        ratioText.text = totalMagimins + "/60";
        potionText.text = potionName;
        potionSlider.value = starProcentage;
        for(int i=0;i< starList.Count; i++)
        {
            float baseP = 1.0f/7 * (i+1);
            if(starProcentage > baseP)
            {
                starList[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                starList[i].GetComponent<Image>().color = Color.gray;
            }
        }

    }
}
