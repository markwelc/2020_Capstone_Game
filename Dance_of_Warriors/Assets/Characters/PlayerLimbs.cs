using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimbs : Character
{
    private float attackDamage;
    protected override void Start()
    {
        attackDamage = 10f;     // For now just setting to an arbitary damage
        // Damage can be fed in from the weapon to determine it later
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        
        // First check if what we are hitting is an enemy attack
        if(collision.gameObject.tag == "enemyAttack")
        {
            Debug.Log("we got hit by something");
            // For this it isn't set up yet
            // I figure we have some database for the weapons like a .json file or something
            // Each weapon would have a name and attack damage

            string ourWeapon = collision.gameObject.name;   // You could get the game objects name for the weapon
            // call some function to get database 
            // Sort throough database for a match with the name
            // once a match is found get the damage for that and set it to attack damage

            // The above isn't set up yet to attackDamage is arbitary for now


            // So we don't have to have a seperate class for each limb lets just do it here

            health -= attackDamage; // Take off the damage from attack

            // Since there are usually 2 colliders that make up a limb
            // I set each of those to equal a tag for thata limb
            // So.. see if our tag matches what was hit
            switch(gameObject.tag)
            {
                case "playerRightArm":
                    rArmHealth -= attackDamage; // Take thhe damage off of that limbs health
                    if(rArmHealth <= 0)         // Is it still usable?
                    {
                        rArmUsability = false; // Set usability to false
                    }
                    break;

                // Continue with same style as prior
                case "playerLeftArm":
                    lArmHealth -= attackDamage;
                    if (lArmHealth <= 0)
                    {
                        lArmUsability = false;
                    }
                    break;

                case "playerRightLeg":
                    rLegHealth -= attackDamage;
                    if (rLegHealth <= 0)
                    {
                        rLegUsability = false;
                    }
                    break;

                case "playerLeftLeg":
                    lLegHealth -= attackDamage;
                    if (lLegHealth <= 0)
                    {
                        lLegUsability = false;
                    }
                    break;

                case "playerBody":
                    bodyHealth -= attackDamage;
                    if (bodyHealth <= 0)
                    {
                        bodyUsability = false;
                    }
                    break;

                case "playerHead":
                    headHealth -= attackDamage;
                    // Lets do extra damage if you got hit in head
                    // Just gonna do an arbitary number for now
                    health -= 5f;
                    if(headHealth <= 0)
                    {
                        headUsability = false;
                    }
                    break;

                default:
                    Debug.Log("Hit no limb somehow?");
                    // Weird ig take away attack damage
                    health += attackDamage;
                    break;
            }
        }
    }
}
