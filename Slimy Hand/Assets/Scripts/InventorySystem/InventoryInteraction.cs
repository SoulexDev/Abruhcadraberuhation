using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryInteraction : MonoBehaviour
{
    public static InventoryInteraction Instance;
    [SerializeField] private ItemDragVisual itemDragVisual;
    private TempSlot tempSlot = new TempSlot();
    private Slot grabbedSlot;

    public bool carryingItem;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        MenuManager.OnInventoryUpdate += MenuManager_OnInventoryUpdate;
    }
    private void MenuManager_OnInventoryUpdate(bool inventoryOpen)
    {
        if(!inventoryOpen && tempSlot.item != null)
        {
            Player.Instance.inventorySystem.AddItemsToSlot(grabbedSlot, tempSlot.item, tempSlot.itemCount, false);

            tempSlot.Clear();
            itemDragVisual.ClearVisual();

            grabbedSlot = null;
        }
        if (!inventoryOpen && Tooltip.Instance.inventoryToolTipLoaded)
        {
            Tooltip.Instance.ClearTooltip();
        }
    }
    private void OnDisable()
    {
        MenuManager.OnInventoryUpdate -= MenuManager_OnInventoryUpdate;
    }
    public void HandleSlot(PointerEventData eventData, Slot slot)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (slot.item == null && tempSlot.item != null)
            {
                if (slot.exclusiveItem != null && tempSlot.item != slot.exclusiveItem)
                    return;

                grabbedSlot = null;
                Player.Instance.inventorySystem.AddItemsToSlot(slot, tempSlot.item, tempSlot.itemCount, false);

                tempSlot.Clear();
                itemDragVisual.ClearVisual();

                carryingItem = false;
            }
            else if (slot.item != null && tempSlot.item == null)
            {
                grabbedSlot = slot;
                tempSlot.CopyFromSlot(slot);
                itemDragVisual.SetVisual(slot.item, slot.itemCount);

                Player.Instance.inventorySystem.RemoveItemsFromSlot(slot, slot.itemCount, false);

                carryingItem = true;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (slot.item != null && slot.item.Use())
                Player.Instance.inventorySystem.RemoveItemsFromSlot(slot, 1, true);
        }
    }
}

public class TempSlot
{
    public Item item;
    public int itemCount;

    public void CopyFromSlot(Slot slot)
    {
        item = slot.item;
        itemCount = slot.itemCount;
    }
    public void Clear()
    {
        item = null;
        itemCount = 0;
    }
}