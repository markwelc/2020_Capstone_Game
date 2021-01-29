using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Health Pack";
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
        Debug.Log("Health Pack picked up");
        gameObject.SetActive(false);
    }

    public void OnUse()
    {
        heal();
    }

    public PlayerHealthController PlayerHealth;
    public float healVal = 20f;

    void heal()
    {
        PlayerHealth.healGeneralHealth(healVal);
    }
}
