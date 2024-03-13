using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDragVisual : MonoBehaviour
{
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TextMeshProUGUI itemCountText;

    public void SetVisual(Item item, int quantity)
    {
        itemIconImage.enabled = true;
        itemIconImage.sprite = item.icon;

        itemCountText.text = $"x{quantity}";
    }
    public void ClearVisual()
    {
        itemIconImage.enabled = false;
        itemIconImage.sprite = null;

        itemCountText.text = "";
    }
    private void LateUpdate()
    {
        transform.position = Input.mousePosition;
    }
}