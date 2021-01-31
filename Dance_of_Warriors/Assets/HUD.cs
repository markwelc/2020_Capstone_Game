using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject inv; // inventory as a GameObject
    public Inventory inventory; // the inventory itself
    public GameObject MessagePanel; // prompts player
    Transform SlotHolder;

    private Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        SlotHolder = transform.Find("InventoryPanel").GetChild(1);

        // for (int i = 0; i < SlotHolder.transform.childCount; i++)
        // {
        //     buttons[i].onClick.AddListener(InventoryScript_ItemRemoved);
        // }

        inventory.ItemAdded += InventoryScript_ItemAdded;
        inventory.ItemRemoved += InventoryScript_ItemRemoved;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        // inv.SetActive(true);
        // Transform SlotHolder = transform.Find("InventoryPanel").GetChild(1);
        // buttons = SlotHolder.GetComponentsInChildren<Button>();
        foreach(Transform slot in SlotHolder)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            if(!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                break;
            }
        }
        // inv.SetActive(false);
    }

    private void InventoryScript_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Debug.Log("Removed");

        // foreach(Transform slot in SlotHolder)
        // {
        //     if (slot.GetComponent(Button).)
            // Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            // if(image.enabled)
            // {
            //     image.enabled = false;
            //     image.sprite = null;

            //     break;
            // }
        // }
    }

    public void OpenMessagePanel(string text)
    {
        MessagePanel.SetActive(true);
    }

    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }
}
