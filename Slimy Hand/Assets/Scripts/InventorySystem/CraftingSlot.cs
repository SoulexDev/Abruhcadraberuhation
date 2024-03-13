using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Recipe itemRecipe;

    private bool tryingShowTooltip;

    private void Awake()
    {
        itemIcon.sprite = itemRecipe.itemToCraft.icon;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        bool canCraft = true;
        for (int i = 0; i < itemRecipe.requiredItemCountPairs.Length; i++)
        {
            ItemCountPair itemCountPair = itemRecipe.requiredItemCountPairs[i];
            if (!Player.Instance.inventorySystem.HasItemQuantity(itemCountPair.item, itemCountPair.count))
            {
                canCraft = false;
                break;
            }
        }
        if (canCraft)
        {
            for (int i = 0; i < itemRecipe.requiredItemCountPairs.Length; i++)
            {
                ItemCountPair itemCountPair = itemRecipe.requiredItemCountPairs[i];
                Player.Instance.inventorySystem.RemoveItem(itemCountPair.item, itemCountPair.count);
            }
            Player.Instance.inventorySystem.AddItem(itemRecipe.itemToCraft, itemRecipe.quantity);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tryingShowTooltip = true;
        CheckToolTip();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tryingShowTooltip = false;
        CheckToolTip();
    }
    private void CheckToolTip()
    {
        if (tryingShowTooltip)
        {
            if (!InventoryInteraction.Instance.carryingItem)
            {
                Tooltip.Instance.LoadRecipeTooltip(itemRecipe);
            }
        }
        else
        {
            Tooltip.Instance.ClearTooltip();
        }
    }
}