using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMaker : MonoBehaviour
{
    [SerializeField] private Cauldron cauldron;
    [SerializeField] private PotionInfo potionInfo;
    [SerializeField] private MagiminsLevel magiminsLevel;
    void Start()
    {
        cauldron.OnItemsChanged += (obj, e) => OnUpdate();
    }

    private void OnUpdate()
    {
        int total = cauldron.GetTotalMagimins();
        Debug.Log("Total:" + total);
        foreach (Magimins magimin in (Magimins[])Enum.GetValues(typeof(Magimins)))
        {
            int magCount = cauldron.GetTotalMagiminsOfType(magimin);
            float procentage = (float) magCount/total;

            Debug.Log("Procentage:" + procentage);

            magiminsLevel.UpdateMagiminOfType(magimin,procentage, magCount);
        }
    }
}
