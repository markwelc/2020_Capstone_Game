using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject inv;
    public Inventory inventory;
    public GameObject MessagePanel;

    // Start is called before the first frame update
    void Start()
    {
        inventory.ItemAdded += InventoryScript_ItemAdded;
        inventory.ItemRemoved += InventoryScript_ItemRemoved;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        // inv.SetActive(true);
        Transform SlotHolder = transform.Find("InventoryPanel").GetChild(1);

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
        Transform SlotHolder = transform.Find("InventoryPanel").GetChild(1);

        foreach(Transform slot in SlotHolder)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            if(image.enabled)
            {
                image.enabled = false;
                image.sprite = null;

                break;
            }
        }
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
