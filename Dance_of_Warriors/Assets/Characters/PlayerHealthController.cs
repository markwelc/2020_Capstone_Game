using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    [SerializeField] private float initialHealth = 100f; //the amount of health that the character has can adjust
    private float currentHealth; // The players current health

    // Initialize limbHealth
    // Each limb has a health variable and bool to determine whether it is usable
    // Usability reflects what it should do when its been damaged all the way
    [SerializeField] private float initialLimbHealth = 40f;
    protected float rArmHealth;
    protected bool rArmUsability;
    protected float lArmHealth;
    protected bool lArmUsability;
    protected float rLegHealth;
    protected bool rLegUsability;
    protected float lLegHealth;
    protected bool lLegUsability;
    protected float bodyHealth;
    protected bool bodyUsability;
    protected float headHealth;
    protected bool headUsability;


    // Start is called before the first frame update
    // Changed to awake. when initialized in start the first variable was always zero
    private void Awake()
    {
        InItHealth();

    }

    // Update is called once per frame
    void Update()
    {
        // with healing involved we need to be sure that
        // it doesn't go beyond the max possible health
        KeepBelowMax();

        if(currentHealth <= 0.0f)
        {
            die();
        }
    }

    /**
     * Initializing our current values
     * this could be called on full start or like if they respawned
     */
    public void InItHealth()
    {
        // Set initial player health
        currentHealth = initialHealth;

        // Set Initial Limb health
        // All are the same for but you can add to if you want some limbs to be stronger
        // Like if player has armor or something
        rArmHealth = initialLimbHealth;
        lArmHealth = initialLimbHealth;
        rLegHealth = initialLimbHealth;
        lLegHealth = initialLimbHealth;
        bodyHealth = initialLimbHealth;
        headHealth = initialLimbHealth;

        // At the start you can always use initially
        rArmUsability = true;
        lArmUsability = true;
        rLegUsability = true;
        lLegUsability = true;
        bodyUsability = true;
        headUsability = true;

    }

    /**
     * Can be called from enemy collision like shown below
     * Basically just decreases from overall and limb specific health
     */
    public void TakeDamage(string collisionTag, float attackDamage)
    {
        // In oncollision enter with enemy object we can call this script
        // Like so....

        /* PlayerHealthController playerHealth = collision.gameObject.GetComponent<PlayerHealthConotroller>();
         * playerHealth.TakeDamage(amount for that attacks damage
         */

        // Subtract attackDamage from current health
        currentHealth -= attackDamage;

        // Since there are usually 2 colliders that make up a limb
        // I set each of those to equal a tag for thata limb
        // So.. see if our tag matches what was hit
        switch (collisionTag)
        {
            case "playerRightArm":
                rArmHealth -= attackDamage; // Take thhe damage off of that limbs health
                if (rArmHealth <= 0)         // Is it still usable?
                {
                    rArmUsability = false; // Set usability to false
                }
                else
                {
                    // Adding this here so if they heal that limb it can be usable again
                    rArmUsability = true;
                }
                break;

            // Continue with same style as prior
            case "playerLeftArm":
                lArmHealth -= attackDamage;
                if (lArmHealth <= 0)
                {
                    lArmUsability = false;
                }
                else
                {
                    // Adding this here so if they heal that limb it can be usable again
                    lArmUsability = true;
                }
                break;

            case "playerRightLeg":
                rLegHealth -= attackDamage;
                if (rLegHealth <= 0)
                {
                    rLegUsability = false;
                }
                else
                {
                    // Adding this here so if they heal that limb it can be usable again
                    rLegUsability = true;
                }
                break;

            case "playerLeftLeg":
                lLegHealth -= attackDamage;
                if (lLegHealth <= 0)
                {
                    lLegUsability = false;
                }
                else
                {
                    // Adding this here so if they heal that limb it can be usable again
                    lLegUsability = true;
                }
                break;

            case "playerBody":
                bodyHealth -= attackDamage;
                if (bodyHealth <= 0)
                {
                    bodyUsability = false;
                }
                else
                {
                    // Adding this here so if they heal that limb it can be usable again
                    bodyUsability = true;
                }
                break;

            case "playerHead":
                Debug.Log("haa bam headshot lil bish");
                headHealth -= attackDamage;
                // Lets do extra damage if you got hit in head
                // Just gonna do an arbitary number for now
                currentHealth -= 5f;
                if (headHealth <= 0)
                {
                    headUsability = false;
                }
                else
                {
                    // Adding this here so if they heal that limb it can be usable again
                    headUsability = true;
                }
                break;

            default:
                Debug.Log("They didnt hit our player");
                // Weird ig take away attack damage
                currentHealth += attackDamage;
                break;
        }

    }

    /**
     * Healing for our standard health variable
     * Could call this and heal over time like heal 1 per second
     * or could be like they ate something to heal them
     */
    public void healGeneralHealth(float healAmount)
    {
        currentHealth += healAmount;
    }

    /**
     * Similar to previous but can heal the limbs now
     * Not sure our exact implementation for this or even if we want to do it
     * but it is here if needed.
     */
    public void healLimb(string limbToBeHealed, float healAmount)
    {
        switch(limbToBeHealed)
        {
            case "playerRightArm":
                rArmHealth += healAmount;
                break;

            // Continue with same style as prior
            case "playerLeftArm":
                lArmHealth += healAmount;
                break;

            case "playerRightLeg":
                rLegHealth += healAmount;
                break;

            case "playerLeftLeg":
                lLegHealth += healAmount;
                break;

            case "playerBody":
                bodyHealth += healAmount;
                break;

            case "playerHead":
                headHealth += healAmount;
                break;

            default:
                Debug.Log("Invalid Limb Name");
                break;

        }
    }

    /**
     * With healing involved this is to ensure that the values don't go beyond the maximum
     */
    private void KeepBelowMax()
    {
        if (currentHealth > initialHealth)
            currentHealth = initialHealth;
        
        if (rArmHealth > initialLimbHealth)
            rArmHealth = initialLimbHealth;

        if (lArmHealth > initialLimbHealth)
            lArmHealth = initialLimbHealth;

        if (rLegHealth > initialLimbHealth)
            rLegHealth = initialLimbHealth;

        if (lLegHealth > initialLimbHealth)
            lLegHealth = initialLimbHealth;

        if (bodyHealth > initialLimbHealth)
            bodyHealth = initialLimbHealth;

        if (headHealth > initialLimbHealth)
            headHealth = initialLimbHealth;
    }

    /**
     * This class should only track limb usability
     * so lets store our unusable limbs in a list so we can call and take action when needed
     */
    public List<string> getUnusableLimb()
    {

        // Initialize a list containing the unusable limbs
        List<string> unusableLimbs = new List<string>();

        // If they can't use it then add their limb name to the list
        if (!rArmUsability)
            unusableLimbs.Add("playerRightArm");

        if (!lArmUsability)
            unusableLimbs.Add("playerLeftArm");

        if (!rLegUsability)
            unusableLimbs.Add("playerRightLeg");

        if (!lLegUsability)
            unusableLimbs.Add("playerLeftLeg");

        if (!bodyUsability)
            unusableLimbs.Add("playerBody");

        if (!headUsability)
            unusableLimbs.Add("playerHead");


        // Return our list of unusable limbs
        return unusableLimbs;
    }


    /**
     * Getter func to return our current health
     */
    public float getHealth()
    {
        return currentHealth;
    }


    //way to kill player. Basically they just turn lifeless 
    void RagDoll(bool value)
    {
        var bodyParts = GetComponentsInChildren<Rigidbody>();
        // For each body part make it kinematic so it can flop
        foreach (var bodyPart in bodyParts)
        {
            bodyPart.isKinematic = value;
        }
    }

    private void die()
    {
        // Disable our animator
        GetComponentInChildren<Animator>().enabled = false;

        // Make it flop
        RagDoll(false);

        // Dont destroy yet just keep visual body in but it can't do anything
        this.enabled = false;

        // But be sure to destroy it on respawn
    }
}
