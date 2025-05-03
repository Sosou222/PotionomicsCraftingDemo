using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotionRecipes", menuName = "ScriptableObjects/PotionRecipe")]
public class PotionRecipe : ScriptableObject
{
    public string Name;
    public int RatioA;
    public int RatioB;
    public int RatioC;
    public int RatioD;
    public int RatioE;

    public float GetRatioNormalized(Magimins mag)
    {
        int total = RatioA+ RatioB + RatioC + RatioD + RatioE;
        switch (mag)
        {
            case Magimins.A:
                return (float)RatioA / total;
            case Magimins.B:
                return (float)RatioB / total;
            case Magimins.C:
                return (float)RatioC / total;
            case Magimins.D:
                return (float)RatioD / total;
            case Magimins.E:
                return (float)RatioE / total;
        }
        return -1.0f;
    }
}
