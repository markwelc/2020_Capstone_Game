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
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        inv.SetActive(true);
        Transform SlotHolder = transform.Find("InventoryPanel").GetChild(1);

        if (SlotHolder != null)
        {
            Debug.Log("SlotHolder not null");
        }
        else
        {
            Debug.Log("SlotHolder is null");
        }

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
        inv.SetActive(false);
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
