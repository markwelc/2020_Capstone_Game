using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageTester : MonoBehaviour
{
    // Start is called before the first frame update
    Character ourPerson;
    void Start()
    {
        Character[] myCharacters = FindObjectsOfType(typeof(Character)) as Character[];
        foreach (Character person in myCharacters)
        {
            if (person.gameObject.name == "Player")
            {
                ourPerson = person;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ourPerson != null)
            ourPerson.playerHealthManager.TakeDamage(collision.gameObject.tag, 1);
    }
}
