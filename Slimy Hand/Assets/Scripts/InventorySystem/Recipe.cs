using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public Item itemToCraft;
    public int quantity;
    public ItemCountPair[] requiredItemCountPairs;
}

[System.Serializable]
public class ItemCountPair
{
    public Item item;
    public int count;
}