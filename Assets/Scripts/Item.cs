using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public ItemCategory Category;
    public string Description;
    public string CategoryName => Category.ToString();
    public int MagiminsA;
    public int MagiminsB;
    public int MagiminsC;
    public int MagiminsD;
    public int MagiminsE;
    public int TotalMagimins => MagiminsA + MagiminsB + MagiminsC + MagiminsD + MagiminsE;
    public Sprite Image;
    public ItemHasTrait Taste;
    public ItemHasTrait Sensation;
    public ItemHasTrait Armora;
    public ItemHasTrait Visual;
    public ItemHasTrait Sound;

    public bool HasMagimins(Magimins magimins)
    {
        switch (magimins)
        {
            case Magimins.A:
                return MagiminsA > 0;
            case Magimins.B:
                return MagiminsB > 0;
            case Magimins.C:
                return MagiminsC > 0;
            case Magimins.D:
                return MagiminsD > 0;
            case Magimins.E:
                return MagiminsE > 0;
        }

        return false;
    }

    public bool HasGoodTrait(Trait trait)
    {
        switch (trait)
        {
            case Trait.Taste:
                return Taste == ItemHasTrait.Good;
            case Trait.Sensation:
                return Sensation == ItemHasTrait.Good;
            case Trait.Armora:
                return Armora == ItemHasTrait.Good;
            case Trait.Visual:
                return Visual == ItemHasTrait.Good;
            case Trait.Sound:
                return Sound == ItemHasTrait.Good;
        }
        return false;
    }

    public bool HasNegativeTrait(Trait trait)
    {
        switch (trait)
        {
            case Trait.Taste:
                return Taste == ItemHasTrait.Bad;
            case Trait.Sensation:
                return Sensation == ItemHasTrait.Bad;
            case Trait.Armora:
                return Armora == ItemHasTrait.Bad;
            case Trait.Visual:
                return Visual == ItemHasTrait.Bad;
            case Trait.Sound:
                return Sound == ItemHasTrait.Bad;
        }
        return false;
    }

    public int GetMagiminsCountOfType(Magimins magimins)
    {
        switch (magimins) 
        {
            case Magimins.A:
                return MagiminsA;
            case Magimins.B:
                return MagiminsB;
            case Magimins.C:
                return MagiminsC;
            case Magimins.D:
                return MagiminsD;
            case Magimins.E:
                return MagiminsE;
        }

        return -1;
    }
}

public enum ItemCategory
{
    Fruit,
    Veggie,
    Meat,
    Fluid,
    Miscelanous
}

public enum ItemHasTrait
{
    None,
    Good,
    Bad
}

public enum Magimins
{
    A, B, C, D, E
}

public enum Trait
{
    Taste, Sensation, Armora, Visual, Sound
}