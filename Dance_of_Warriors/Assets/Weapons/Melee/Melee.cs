﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : WeaponController
{
    protected bool impactPlaying = false;
    protected float baseDamage; //the base amount of damage this weapon expects to do
    protected int[] phaseTimes; //the lenght of time each phase should last
    public Collider user; //the character using this weapon
    protected int selection; //integer that holds the random clip to play
    protected Character targetCharacter; //used to fiture out what to actually damage
    private bool canDamage = false;
    protected bool previousAttack;

    /**
     * sets the animation name and the lengths of the action states
     */
    public override void useWeapon(string weaponName, out string animation, out int[] states, int attackType, float characterDamageModifier)
    {
        if (attackType == 1)
        {
            animation = animationName;
            states = weaponStates;
            if (previousAttack == false)
			{
                StartCoroutine("FastSwing");
            }
        }
        else if(attackType == 2)
        {
            animation = animationNameSecondary;
            states = weaponStatesSecondary;
            if (previousAttack == false)
            {
                StartCoroutine("Swing");
            }
        }
        else if(attackType == 3)
        {
            animation = animationNameUniqueOne;
            states = weaponStatesUniqueOne;
            StartCoroutine("FastSwing");
        }
        else
        {
            animation = animationNameUniqueTwo;
            states = weaponStatesUniqueTwo;
            StartCoroutine("FastSwing");
        }
    }

    /**
     * runs whenever the weapon hits someone
     * deals mainly with damage and audio
     */
    protected void OnTriggerEnter(Collider other)
    {
        //Debug.Log("LayerMask.LayerToName(other.gameObject.layer) = " + LayerMask.LayerToName(other.gameObject.layer));
        if(LayerMask.LayerToName(other.gameObject.layer) == "enemy" || LayerMask.LayerToName(other.gameObject.layer) == "player")
        {
            if (canDamage && !checkSelf(other))
            {
                Debug.Log("hit a character");
                targetCharacter = getCharacterInParents(other.gameObject);
                targetCharacter.playerHealthManager.TakeDamage(other.gameObject.tag, 1);
                //sound doesn't work correctly due to the way the collider works right now
                if (impactPlaying == false)
                {
                    StartCoroutine("ImpactAudio");
                }
            }
        }
    }

    /*
     * Play a random sword impact sound each time the sword hits a character
     */
    private IEnumerator ImpactAudio()
	{
        //still playing at weird times. this happens because canDamage is enabled, and collisions happen during the build-up as well as the actual attack.
        impactPlaying = true;
        //random number generation in order to determine which sound to play
        selection = Random.Range(1, 7);
        switch (selection)
        {
            case 1:
                FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "sword impact 1");
                break;
            case 2:
                FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "sword impact 2");
                break;
            case 3:
                FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "sword impact 3");
                break;
            case 4:
                FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "sword impact 4");
                break;
            case 5:
                FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "sword impact 5");
                break;
            case 6:
                FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "sword impact 6");
                break;
            default:
                FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "sword impact 1");
                break;
        }
        
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        impactPlaying = false;
    }

    /*
     * Delay the sound of the heavy swing until appropriate
     */
    private IEnumerator Swing()
    {
        previousAttack = true;
        yield return new WaitForSeconds(0.85f);
        FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "swing");
        yield return new WaitForSeconds(0.95f);
        previousAttack = false;
    }

    /*
     * Delay the sound of the light (fast) swing until appropriate
     */
    private IEnumerator FastSwing()
    {
        previousAttack = true;
        yield return new WaitForSeconds(0.33f);
        FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "fastswing");
        yield return new WaitForSeconds(1.1f);
        previousAttack = false;
    }

    

    private Character getCharacterInParents(GameObject start)
    {
        Character res = null;
        res = start.GetComponent<Character>();

        while(!res)
        {
            start = start.transform.parent.gameObject;
            if (!start)
                break;
            res = start.GetComponent<Character>();
        }

        return res;
    }

    /*
     * Enables the sword to deal damage
     */
    public override void canDealDamage(string weaponN, bool canDamage)
    {
        
        this.canDamage = canDamage;
        
    }

    private bool checkSelf(Collider other)
    {
        Transform ourRoot = this.transform.root;
        int ourLayer = ourRoot.gameObject.layer;

        Transform otherRoot = other.transform.root;
        int otherLayer = otherRoot.gameObject.layer;

        return ourLayer == otherLayer;
    }
}
