using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image iconImage;
    [SerializeField] private TextMeshProUGUI countText;
    public Item exclusiveItem;

    private bool tryingShowTooltip;
    private bool showingTooltip;

    private Item _item;

    private int _itemCount;
    public int itemCount 
    { 
        get 
        { 
            return _itemCount; 
        } 
        set 
        { 
            _itemCount = value; countText.text = _itemCount == 0 ? "" : "x" + _itemCount.ToString();
            if (_itemCount == 0)
                item = null;
        } 
    }

    public Item item 
    { 
        get 
        { 
            return _item; 
        } 
        set 
        {
            _item = value;
            iconImage.sprite = _item == null ? null : _item.icon;
            iconImage.enabled = _item != null;

            if (_item == null && showingTooltip)
                Tooltip.Instance.ClearTooltip();
            CheckToolTip();
        } 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryInteraction.Instance.HandleSlot(eventData, this);
        CheckToolTip();
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
            if (item != null && !InventoryInteraction.Instance.carryingItem)
            {
                Tooltip.Instance.LoadTooltip(item);
                showingTooltip = true;
            }
        }
        else
        {
            Tooltip.Instance.ClearTooltip();
            showingTooltip = false;
        }
    }
}