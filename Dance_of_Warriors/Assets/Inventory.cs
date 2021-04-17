using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public CameraLook cam; //for disabling camera controls while inventory is open

    private const int SLOTS = 6;

    public bool inventoryEnabled = false;
    public GameObject inventory;

    public GameObject slotHolder;
    PlayerControls controls;

    private List<IInventoryItem> mItems = new List<IInventoryItem>();
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;

    public GameObject firstSelectedSlot;

    public NewPlayer playa;

    public void AddItem(IInventoryItem item)
    {
        if (mItems.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
                item.OnPickup();

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }

    public void UseItem(IInventoryItem item)
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnUse();
        }
        if (ItemRemoved != null)
        {
            ItemRemoved(this, new InventoryEventArgs(item));
        }
    }


    private void Awake()
    {
        controls = new PlayerControls();    // Initialize our controls object

        controls.Gameplay.enableInventory.performed += ctx => inventoryButton();
    }

    /**
     * Enable Gameplay controls
     */
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    /**
     * Disable Gameplay controls
     */
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void inventoryButton()
    {
        if (Time.timeScale > 0.0f) // This just checks if the game is paused
        {
            if (inventory.activeSelf) // if inventory is active when the button is pressed
            {
                Cursor.lockState = CursorLockMode.Locked;
                inventory.SetActive(false); //deactivate it in game
                cam.isCamMovementAllowed = true; // allow camera movement
                if(playa != null)
                    playa.menuEnabled(false);
            }
            else //if inventory is not active when button is pressed
            {
                Cursor.lockState = CursorLockMode.None;
                inventory.SetActive(true); //activate it in game
                cam.isCamMovementAllowed = false; //disallow camera movement
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(firstSelectedSlot);
                if(playa != null)
                    playa.menuEnabled(true);
            }
        }
    }
}
