using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : Interactable
{
    [SerializeField] private Item[] items;
    [SerializeField] private int quantityMin = 1, quantityMax = 1;
    [SerializeField] private bool destroyObject = false;

    private void Awake()
    {
        if(items.Length == 1)
            interactMessage = $"Pick Up {items[0].itemName}";
    }
    public override void Interact()
    {
        if (!isInteractable)
            return;

        int quantity = Random.Range(quantityMin, quantityMax + 1);
        int itemIndex = Random.Range(0, items.Length);

        if (Player.Instance.inventorySystem.AddItem(items[itemIndex], quantity))
        {
            interactEvent?.Invoke();
            if (destroyObject)
                Destroy(gameObject);
        }
    }
}