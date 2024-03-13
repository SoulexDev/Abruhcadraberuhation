using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Interactable
{
    private bool open;
    public override void Interact()
    {
        if (open)
        {
            CloseTable();
            return;
        }
        
        Player.Instance.canMove = false;

        MenuManager.Instance.SetCraftingUIState(true);

        //isInteractable = false;
        open = true;

        StopAllCoroutines();
        StartCoroutine(WaitForClose());
    }
    IEnumerator WaitForClose()
    {
        while (open)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseTable();
            }
            yield return null;
        }
    }
    void CloseTable()
    {
        MenuManager.Instance.SetCraftingUIState(false);

        Player.Instance.canMove = true;
        open = false;

        if(!MenuManager.Instance.inventoryOpen)
            Tooltip.Instance.ClearTooltip();
        else if(!Tooltip.Instance.inventoryToolTipLoaded)
            Tooltip.Instance.ClearTooltip();
    }
}