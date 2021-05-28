using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{
    public HUD hud;
    public Inventory inv;
    public void OnItemClicked()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();
        Image image = gameObject.transform.Find("ItemImage").GetComponent<Image>();
        IInventoryItem item = dragHandler.Item;
        if (item != null)
        {
            Debug.Log(item.Name);
            inv.UseItem(item);
        }
    }
}
