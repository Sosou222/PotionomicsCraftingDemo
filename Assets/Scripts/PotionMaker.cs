using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMaker : MonoBehaviour
{
    [SerializeField] private Cauldron cauldron;
    [SerializeField] private PotionInfo potionInfo;
    [SerializeField] private MagiminsLevel magiminsLevel;

    [SerializeField] private List<PotionRecipe> recipesList;

    private List<int> recipeLevelCaps = new List<int>() { 60,120,180,240,300,360};
    void Start()
    {
        cauldron.OnItemsChanged += (obj, e) => OnUpdate();
    }

    private void OnUpdate()
    {
        int total = cauldron.GetTotalMagimins();
        foreach (Magimins magimin in (Magimins[])Enum.GetValues(typeof(Magimins)))
        {
            int magCount = cauldron.GetTotalMagiminsOfType(magimin);
            float procentage = (float) magCount/total;

            magiminsLevel.UpdateMagiminOfType(magimin,procentage, magCount);
        }

        int totalIgridients = cauldron.GetIngredientCount();

        PotionRecipe recipe = null;
        if (total > 0)
            recipe = MatchClosestPotion();

        float starProcentage = GetStarProcentage();
        Debug.Log("Procentage:" + starProcentage);
        string potionLevel = GetPotionLevel();
        potionInfo.UpdateInfo(totalIgridients, total, potionLevel + recipe.Name, starProcentage);
    }

    private float GetStarProcentage()
    {
        int total = cauldron.GetTotalMagimins();
        int min = 0;
        for(int i=1;i< recipeLevelCaps.Count;i++)
        {
            if(total > recipeLevelCaps[i])
            {
                continue;
            }
            else
            {
                min = recipeLevelCaps[i-1];
            }
        }


        float procentage = (float)(min - total)/ min;

        return procentage;
    }

    public string GetPotionLevel()
    {
        string[] names = {"Basic","Normal","Advanced","Grand","Superior","Masterworks" };
        int index = 0;

        int total = cauldron.GetTotalMagimins();
        for(int i=0;i<recipeLevelCaps.Count-1;i++)
        {
            if (total > recipeLevelCaps[i])
            {
                index++;
            }
        }

        return names[index];
    }

    private PotionRecipe MatchClosestPotion()
    {
        int total = cauldron.GetTotalMagimins();

        PotionRecipe bestMatch = null;
        float lowestDistance = float.MaxValue;


        foreach(PotionRecipe recipe in recipesList)
        {
            float distance = ComputeDistance(recipe, total);
            if (distance < lowestDistance)
            {
                lowestDistance = distance;
                bestMatch = recipe;
            }
        }

        return bestMatch;
    }

    private float ComputeDistance(PotionRecipe recipe,int totalMagimins)
    {
        float sum = 0.0f;
        foreach (Magimins magimin in (Magimins[])Enum.GetValues(typeof(Magimins)))
        {
            float valInput = (float)cauldron.GetTotalMagiminsOfType(magimin) / totalMagimins;
            float valTarget = recipe.GetRatioNormalized(magimin);

            float diff = valInput - valTarget;
            sum += diff * diff;
        }
        return Mathf.Sqrt(sum);
    }
}
