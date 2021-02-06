using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
    {
        string Name { get; } // name of the object
        Sprite Image { get; } // the object's sprite/image, set in Unity
        void OnPickup(); // adds the object to the player's inventory
    }

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
    public IInventoryItem Item;
}
