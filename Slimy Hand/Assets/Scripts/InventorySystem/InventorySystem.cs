using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<Slot> slots = new List<Slot>();
    public Slot armorSlot;

    public void ClearInventory()
    {
        slots.ForEach(s=>s.itemCount = 0);
    }
    public bool AddItem(Item item, int quantity)
    {
        if(slots.Any(s=>s.item == item))
        {
            Slot slot = slots.First(s => s.item == item);
            slot.itemCount += quantity;
            Player.Instance.notificationManager.QueueNotification($"Received {item.itemName}");

            return true;
        }
        if(slots.Any(s=>s.item == null))
        {
            Slot slot = slots.First(s => s.item == null);
            slot.item = item;
            slot.itemCount = quantity;

            Player.Instance.notificationManager.QueueNotification($"Received {item.itemName}");

            return true;
        }
        Player.Instance.notificationManager.QueueNotification("Inventory full");

        return false;
    }
    public bool RemoveItem(Item item, int quantity = 1)
    {
        if (!HasItemQuantity(item, quantity))
            return false;

        if(slots.Any(s=>s.item == item))
        {
            Slot slot = slots.Last(s => s.item == item);
            slot.itemCount -= quantity;

            Player.Instance.notificationManager.QueueNotification($"Removed {item.itemName}");
            return true;
        }
        return false;
    }
    public bool AddItemsToSlot(Slot slot, Item item, int quantity, bool showMessage)
    {
        if(slot.item == null)
        {
            slot.item = item;
            slot.itemCount = quantity;
        }
        else if (slot.item == item)
        {
            slot.itemCount += quantity;
        }
        else
        {
            return false;
        }
        
        string itemName = slot.item.itemName;
        if (showMessage)
            Player.Instance.notificationManager.QueueNotification("Added " + itemName);

        return true;
    }
    public void RemoveItemsFromSlot(Slot slot, int quantity, bool showMessage)
    {
        string itemName = slot.item.itemName;
        slot.itemCount -= quantity;

        if(showMessage)
            Player.Instance.notificationManager.QueueNotification("Removed " + itemName);
    }
    public bool HasItem(Item item)
    {
        if (slots.Any(s => s.item == item))
            return true;
        return false;
    }
    public bool HasItemQuantity(Item item, int quantity)
    {
        int totalQuantity = 0;
        if (!slots.Any(s => s.item == item))
            return false;

        foreach(Slot slot in slots.FindAll(s=>s.item == item))
        {
            totalQuantity += slot.itemCount;

            if(totalQuantity >= quantity)
            {
                return true;
            }
        }
        return false;
    }
}