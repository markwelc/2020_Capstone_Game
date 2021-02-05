using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocBag : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Doctor's Bag";
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
        Debug.Log("Doctor's bag picked up");
        gameObject.SetActive(false);
    }

    public void OnUse()
    {
        limbHeal();
    }

    Character player;
    float playerHealth;
    [SerializeField] public string ourTarget = "Player";

    void Start()
    {
        //make an array of all the characters in the scene
        Character[] myCharacters = FindObjectsOfType(typeof(Character)) as Character[];
        //for each character in the array
        foreach(Character person in myCharacters) {
            //if the current person is the one we are after (the player)
            if(person.gameObject.name == ourTarget)
            {
                //set player to person
                player = person;
                //exit loop
                break;
            }
        }
        //get the player's health
        playerHealth = player.getHealth();
    }

    public float healVal;

    void limbHeal()
    {
        Debug.Log("Current health: " + player.playerHealthManager.getHealth());
        // add healVal to player's health
        player.playerHealthManager.healGeneralHealth(healVal);
        Debug.Log("Healing player for " + healVal + " health");
        Debug.Log("New health value: " + player.playerHealthManager.getHealth());
    }
}