using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    void OnPickup();
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
    public IInventoryItem Item;
}

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 24;

    public bool inventoryEnabled = false;
    public GameObject inventory;

    private int allSlots, enabledSlots;
    private GameObject[] slot;
    public GameObject slotHolder;
    PlayerControls controls;

    private List<IInventoryItem> mItems = new List<IInventoryItem>();
    public event EventHandler<InventoryEventArgs> ItemAdded;

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

    void Start()
    {
        // allSlots = 24;
        slot = new GameObject[SLOTS];
        for (int i = 0; i < SLOTS; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
        }
    }

    void inventoryButton()
    {
        inventoryEnabled = !inventoryEnabled;
    }

    void Update()
    {
        if (inventoryEnabled == true)
        {
            inventory.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            inventory.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
