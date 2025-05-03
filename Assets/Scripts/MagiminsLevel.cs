using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagiminsLevel : MonoBehaviour
{
    [SerializeField] private List<GameObject> magiminsLevelsList = new List<GameObject>();

    public void UpdateMagiminOfType(Magimins magimins,float procentaged,int count)
    {
        GameObject holder = magiminsLevelsList[(int)magimins];
        holder.transform.Find("Slider").GetComponent<Slider>().value = procentaged;
        holder.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = "" + count;
    }
}
