using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private Transform cam;

    public PlayerController controller;
    public InventorySystem inventorySystem;
    public NotificationManager notificationManager;
    public PlayerStats stats;

    public GameObject deathMenu;

    public bool paused;
    public bool canMove = true;
    public bool canLook = true;
    public bool canInteract = true;
    public bool dead;
    public bool canPause = true;
    public bool escapeLocked;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(cam.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            cam.SetParent(null);

            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(cam.gameObject);
        }

        Random.InitState(System.DateTime.Now.Millisecond);
    }
    public void Die()
    {
        deathMenu.SetActive(true);

        controller.transform.position = Vector3.down * 25;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        canMove = false;
        dead = true;
    }
    public void SetMoveState(bool state)
    {
        canMove = state;
    }
    public void SetLookState(bool state)
    {
        canLook = state;
    }
    public void SetInteractState(bool state)
    {
        canInteract = state;
    }
}