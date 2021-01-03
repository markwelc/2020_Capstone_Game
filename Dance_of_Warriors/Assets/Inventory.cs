using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool inventoryEnabled = false;
    public GameObject inventory;

    private int allSlots, enabledSlots;
    private GameObject[] slot;
    public GameObject slotHolder;
    PlayerControls controls;
    
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
        allSlots = 30;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
        }
    }

    void inventoryButton()
    {
        Debug.Log("Tab pressed");
        inventoryEnabled = !inventoryEnabled;
    }

    void Update()
    {
        // // if (Input.GetButtonDown(KeyCode.Tab))
        // if (Input.GetButtonDown("tab"))
        // {
        //     Debug.Log("Tab pressed");
        //     inventoryEnabled = !inventoryEnabled;
        // }

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
