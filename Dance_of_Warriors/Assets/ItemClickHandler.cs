using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{
    public HUD hud;
    public void OnItemClicked()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();
        // hud.InventoryScript_ButtonPressed();
        Image image = gameObject.transform.Find("ItemImage").GetComponent<Image>();
        Debug.Log("Image Found");
        // if (image != null)
        // {
        //     image.enabled = false;
        //     image.sprite = null;
        // }
        IInventoryItem item = dragHandler.Item;
        Debug.Log("Item set");
        Debug.Log(item.Name);
        item.OnUse();
        if (image != null)
        {
            image.enabled = false;
            image.sprite = null;
        }
    }
}
