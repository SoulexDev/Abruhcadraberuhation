using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance;
    [SerializeField] private RectTransform tooltipBox;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemTagText;

    public bool inventoryToolTipLoaded;

    public static Dictionary<ItemType, Color> itemTypeColors = new Dictionary<ItemType, Color>() 
    {
        { ItemType.Resource, new Color(0.5f, 0.5f, 0.9f) },
        { ItemType.Food, new Color(0.5f, 0.9f, 0.5f) },
        { ItemType.Equipment, new Color(0.9f, 0.5f, 0.5f) },
        { ItemType.Armor, new Color(0.9f, 0.9f, 0.5f) },
        { ItemType.Component, new Color(0.9f, 0.5f, 0.9f) }
    };
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipBox);
    }
    private void LateUpdate()
    {
        transform.position = Input.mousePosition;
    }
    public void LoadTooltip(Item item)
    {
        if (!MenuManager.Instance.inventoryOpen)
            return;
        tooltipBox.gameObject.SetActive(true);

        itemTypeText.text = $"{item.itemName} - {item.itemType}";

        itemDescriptionText.text = item.itemDescription;

        itemTagText.text = item.itemTag;
        background.color = itemTypeColors[item.itemType];

        inventoryToolTipLoaded = true;
    }
    public void LoadRecipeTooltip(Recipe recipe)
    {
        if (!MenuManager.Instance.craftingMenuOpen)
            return;

        tooltipBox.gameObject.SetActive(true);
        itemTypeText.text = $"{recipe.itemToCraft.itemName} - {recipe.itemToCraft.itemType}";

        itemDescriptionText.text = recipe.itemToCraft.itemDescription;

        for (int i = 0; i < recipe.requiredItemCountPairs.Length; i++)
        {
            if (i > 0)
                itemTagText.text += "\n";

            ItemCountPair pair = recipe.requiredItemCountPairs[i];

            string color = Player.Instance.inventorySystem.HasItemQuantity(pair.item, pair.count) ? "green" : "red";

            itemTagText.text += $"<color={color}>{pair.item.itemName} x{pair.count}</color>";
        }
        background.color = itemTypeColors[recipe.itemToCraft.itemType];
    }
    public void ClearTooltip()
    {
        tooltipBox.gameObject.SetActive(false);

        itemTypeText.text = "";

        itemDescriptionText.text = "";

        itemTagText.text = "";
        background.color = Color.white;

        inventoryToolTipLoaded = false;
    }
}