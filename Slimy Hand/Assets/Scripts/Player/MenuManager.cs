using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject craftingMenu;
    public bool inventoryOpen;
    public bool craftingMenuOpen;

    private bool mouseableMenuOpen;

    public delegate void InventoryUpdate(bool inventoryOpen);
    public static event InventoryUpdate OnInventoryUpdate;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        PlayerInput();
    }
    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetInventoryState(!inventoryOpen);
        }
    }
    public void SetInventoryState(bool state)
    {
        inventoryOpen = state;

        inventoryMenu.SetActive(inventoryOpen);

        OnInventoryUpdate?.Invoke(inventoryOpen);

        UpdateMenuConditions();
    }
    public void SetCraftingUIState(bool state)
    {
        craftingMenuOpen = state;

        craftingMenu.SetActive(craftingMenuOpen);

        UpdateMenuConditions();
    }
    void UpdateMenuConditions()
    {
        mouseableMenuOpen = inventoryOpen || craftingMenuOpen;

        Player.Instance.canLook = !mouseableMenuOpen;

        Cursor.lockState = mouseableMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = mouseableMenuOpen;
    }
}