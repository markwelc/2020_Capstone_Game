using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Cube";
        }
    }

    public Sprite _Image = null;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void OnPickup()
    {
        Debug.Log("Cube picked up");
        gameObject.SetActive(false);
    }

    public void OnUse()
    {
        Debug.Log("Cube Used");
    }
}
