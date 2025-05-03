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

        int totalIgridients = cauldron.GetIngredientCount();

        PotionRecipe recipe = null;
        if (total > 0)
            recipe = MatchClosestPotion();
        potionInfo.UpdateInfo(totalIgridients, total,recipe);
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
