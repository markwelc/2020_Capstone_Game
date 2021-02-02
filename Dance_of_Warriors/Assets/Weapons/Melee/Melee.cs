using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : WeaponController
{
    protected float baseDamage; //the base amount of damage this weapon expects to do
    protected int[] phaseTimes; //the lenght of time each phase should last
    public Collider user; //the character using this weapon
                          // private Transform weaponTransform;
    protected Character targetCharacter; //used to figure out what to actually damage
                                //may or may not eventually move to WeaponController.cs


    //public override void useWeapon(string weaponName, out string animation, out float[] states)
    //{
    //    //weaponTransform = this.GetComponent<Transform>();

    //    animation = animationName;
    //    states = weaponStates;

    //    // weaponTransform.localScale += new Vector3(0, 0.5f, 0);
    //}

    public override void useWeapon(string weaponName, out string animation, out int[] states, int attackType)
    {
        if (attackType == 2)
            animation = animationNameSecondary;
        else
            animation = animationName;
        states = weaponStates;
    }

    protected void OnTriggerEnter(Collider other)
    {
        //Debug.Log("a melee weapon hit something");
        //Debug.Log("other.gameObject = " + other.gameObject);
        Debug.Log("LayerMask.LayerToName(other.gameObject.layer) = " + LayerMask.LayerToName(other.gameObject.layer));
        if (LayerMask.LayerToName(other.gameObject.layer) == "enemy" || LayerMask.LayerToName(other.gameObject.layer) == "player")
        {
            //Debug.Log("it hit a character");
            targetCharacter = getCharacterInParents(other.gameObject);
            targetCharacter.playerHealthManager.TakeDamage(other.gameObject.tag, 1);
        }

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
}
