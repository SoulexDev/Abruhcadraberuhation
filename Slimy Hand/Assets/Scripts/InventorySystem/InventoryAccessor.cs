using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAccessor : MonoBehaviour
{
    public void GiveItem(Item item)
    {
        Player.Instance.inventorySystem.AddItem(item, 1);
    }
}