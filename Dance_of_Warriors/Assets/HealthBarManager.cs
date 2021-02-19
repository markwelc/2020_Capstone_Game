using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Need UI to handle this

public class HealthBarManager : MonoBehaviour
{

    private Image healthBar;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] public string ourTarget = "Player";
    private float currentHealth;
    Character ourPerson;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        Character[] myCharacters = FindObjectsOfType(typeof(Character)) as Character[];
        foreach(Character person in myCharacters) {
            if(person.gameObject.name == ourTarget)
            {
                ourPerson = person;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (ourPerson == null)
            currentHealth = 100f;
        else
            currentHealth = ourPerson.getHealth();
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
