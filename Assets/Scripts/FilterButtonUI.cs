using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterButtonUI : MonoBehaviour
{
    private List<Color> colors;
    private Image image;
    private int index;

    private void Awake()
    {
        image = GetComponent<Image>();
        colors = new List<Color> { Color.white, Color.green, Color.red };
        index = 0;
    }
    public void UpdateColor()
    {
        index++;
        if (index >= colors.Count) index = 0;
        image.color = colors[index];
    }
}
