using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject, IItem
{
    public string itemName;
    [TextArea(5, 20)] public string itemDescription;
    public string itemTag;
    public Sprite icon;
    public GameObject itemPrefab;
    public ItemType itemType;

    public virtual bool Initialize()
    {
        return false;
    }

    public virtual bool Use()
    {
        return false;
    }

    public virtual void DeInitialize()
    {
        
    }
}
public enum ItemType
{
    Resource,
    Food,
    Equipment,
    Armor,
    Component
}