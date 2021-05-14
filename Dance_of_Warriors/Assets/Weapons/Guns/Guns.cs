﻿//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Guns : WeaponController
{
    /*[SerializeField] private*/ public GameObject bulletPrefab;
    /*[SerializeField] private*/ public Transform UserFirepoint;
    private GameObject newBullet;
    protected int bulletsLeft = -1;
    protected bool reloading = false;
    private GameObject mine;
    protected CameraLook myC;


    //add limited magazines?


    /**
     * tries to fire the gun, or sets animation and action state lengths if swinging it.
     * It won't be able to fire the gun if there aren't bullets left or if we're reloading
     * parameters are for the weapon we are using, the name of the animation, the weapons states (time for various actions), 
     * which attack type is being performed (light/heavy), and the amount of damage the weapon does
     */
    public override void useWeapon(string weaponName, out string animation, out int[] states, int attackType, float characterDamageModifier)
    {
        if (attackType == 2)
        {
            animation = animationNameSecondary;
            states = weaponStatesSecondary;
        }
        else
        {
            animation = animationName;
            states = weaponStates;
        }

        if(animation == null)
        {
            if (bulletsLeft == -1)
            {
                //set the correct number of bullets in the magazine depending on which gun is being used
                bulletsLeft = ammo;
                shoot();
            }
            //you've run out of ammo! reload!
            else if (bulletsLeft == 0)
            {
                if (reloading == false)
                {
                    reloading = true;
                    StartCoroutine("Reload");
                    //may want to make a switch here if we want new guns to have other reload sounds
                    FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "reload");
                    
                }
            }
            //go ahead and shoot one bullet and remove it from the magazine
            else
            {
                shoot();
            }
        }
        
    }

    /*
     * Delays the addition of ammunition to the gun magazine until a full reload is done  
     */
    IEnumerator Reload()
	{
        yield return new WaitForSeconds(2.7f);
        bulletsLeft = ammo;
        reloading = false;
    }


    /**
     * spawns a bullet at the right spot and makes the appropriate sound
     */
    void shoot()
	{
        //play the weapon's sound from the sound manager
        FindObjectOfType<AudioManager>().Play(this.transform.gameObject, "handgun");
        bulletsLeft--;
        //Debug.Log("bullets left: " + bulletsLeft);
        Vector3 angle = UserFirepoint.rotation.eulerAngles; //figure out the angle

        newBullet = Instantiate(bulletPrefab, UserFirepoint.position, Quaternion.Euler(angle)); //make the bullet
                                                                                                //the bullet controller takes things from here
        newBullet.GetComponent<BulletController>().source = UserFirepoint.transform.parent.parent.GetComponent<Collider>(); //be sure to set the bullets parent
        // only add recoil when player shoots
       // if (this.gameObject.layer == 8)
        //{
            mine = GameObject.Find("Third Person Camera");
            myC = mine.GetComponent<CameraLook>();
            myC.AddRecoil();
        //}
    }


    /*
     * This function is called when the object that this script is attached to is deactivated
     * Thus, when switching from a gun this script is called
     */
    void OnDisable()
    {
        //if we were in the middle of reloading, just undo all our progress
        if (reloading)
            reloading = false;
    }
}
