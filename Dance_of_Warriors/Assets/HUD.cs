using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject inv; // inventory as a GameObject
    public Inventory inventory; // the inventory itself
    public GameObject MessagePanel; // prompts player
    Transform SlotHolder; // the SlotHolder within the inventory object

    private List<Button> buttons = new List<Button>(); //List of buttons for each slot in the inventory
    public Button slotButton; //Used to keep track of the button component of a specific inventory slot

    // Start is called before the first frame update
    void Start()
    {
        SlotHolder = transform.Find("InventoryPanel").GetChild(1); // find SlotHolder in game
        foreach(Transform slot in SlotHolder)
        {
            slotButton = slot.GetChild(0).GetComponent<Button>(); // get the button component of the slot
            buttons.Add(slotButton); // add it to the buttons array
            slotButton.onClick.AddListener(delegate {InventoryScript_ButtonPressed(slot);}); // add event listener for when a button is pressed
        }
        inventory.ItemAdded += InventoryScript_ItemAdded;
    }

    //Call this when an item is added to the inventory
    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        foreach(Transform slot in SlotHolder)
        {
            //Get the image component of the current slot
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            //if the image component of the slot is not enabled
            if(!image.enabled)
            {
                //enable the image component of the slot
                image.enabled = true;
                //set the image component of the slot to show the item's image
                image.sprite = e.Item.Image;
                //item is added to inventory, exit loop
                break;
            }
        }
    }

    //Call this when a button in the inventory is selected
    public void InventoryScript_ButtonPressed(Transform slot)
    {
        Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
        if (image.enabled)
        {
            image.enabled = false;
            image.sprite = null;
        }
        Debug.Log("Removed");
    }

    //Open the pickupMessage panel
    public void OpenMessagePanel(string text)
    {
        MessagePanel.SetActive(true);
    }
    //Close the pickupMessage panel
    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }
}
