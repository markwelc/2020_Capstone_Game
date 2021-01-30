﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 6;

    public bool inventoryEnabled = false;
    public GameObject inventory;

    public GameObject slotHolder;
    PlayerControls controls;

    private List<IInventoryItem> mItems = new List<IInventoryItem>();
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;

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
            Debug.Log("Item found in inventory");
            mItems.Remove(item);
        }
        if (ItemRemoved != null)
        {
            ItemRemoved(this, new InventoryEventArgs(item));
        }
        item.OnUse();
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
        if(inventory.activeSelf)
        {
            inventory.SetActive(false);
            // controls.Gameplay.Disable();
            Time.timeScale = 1f;
        }
        else
        {
            inventory.SetActive(true);
            // controls.Gameplay.Enable();
            Time.timeScale = 0f;
        }
    }
}
