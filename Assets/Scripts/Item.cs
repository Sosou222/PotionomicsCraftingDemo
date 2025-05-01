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
}

public enum ItemCategory
{
    Fruit,
    Veggie
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