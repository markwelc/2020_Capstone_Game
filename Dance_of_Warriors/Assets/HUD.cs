using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject inv; // inventory as a GameObject
    public Inventory inventory; // the inventory itself
    public GameObject PickupMessagePanel; // prompts player to pick up an object
    public GameObject DeathMessagePanel; // shows on screen when player dies
    Transform SlotHolder; // the SlotHolder within the inventory object

    private List<Transform> buttons = new List<Transform>(); //List of buttons for each slot in the inventory
    public Button slotButton; //Used to keep track of the button component of a specific inventory slot

    // Start is called before the first frame update
    void Start()
    {
        SlotHolder = transform.Find("InventoryPanel").GetChild(1); // find SlotHolder in game
        inventory.ItemAdded += InventoryScript_ItemAdded;
        inventory.ItemRemoved += InventoryScript_ButtonPressed;
    }

    //Call this when an item is added to the inventory
    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        foreach(Transform slot in SlotHolder)
        {
            //get the position of the image within the slot
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            //Get the image component of the current slot
            Image image = imageTransform.GetComponent<Image>();
            //use the DragHandler to get a reference to the item in that slot
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
            //if the image component of the slot is not enabled
            if(!image.enabled)
            {
                //enable the image component of the slot
                image.enabled = true;
                //set the image component of the slot to show the item's image
                image.sprite = e.Item.Image;
                //get a reference to the item
                itemDragHandler.Item = e.Item;
                //item is added to inventory, exit loop
                break;
            }
        }
    }

    //Call this when a button in the inventory is selected
    public void InventoryScript_ButtonPressed(object sender, InventoryEventArgs e)
    {
        foreach(Transform slot in SlotHolder)
        {
            //get the position of the image within the slot
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            //Get the image component of the current slot
            Image image = imageTransform.GetComponent<Image>();
            //use the DragHandler to get a reference to the item in that slot
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
            //if the item in that slot is equal to e
            if(itemDragHandler.Item.Equals(e.Item))
            {
                //disable the image
                image.enabled = false;
                //remove the image from the component
                image.sprite = null;
                //unset the item
                itemDragHandler.Item = null;
                //item is removed, exit loop
                break;
            }
        }
    }

    //Open the pickupMessage panel
    public void OpenPickupMessagePanel(string text)
    {
        PickupMessagePanel.SetActive(true);
    }
    //Close the pickupMessage panel
    public void ClosePickupMessagePanel()
    {
        PickupMessagePanel.SetActive(false);
    }
    //Open the deathMessage panel
    public void OpenDeathMessagePanel()
    {
        DeathMessagePanel.SetActive(true);
    }
}
