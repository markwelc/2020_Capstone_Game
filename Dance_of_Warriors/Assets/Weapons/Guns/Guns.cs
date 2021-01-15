using System;
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
    //add limited magazines?

    public override void useWeapon(string weaponName, out string animation, out int[] states)
    {
        if(bulletsLeft == -1)
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
            }
		}
        //go ahead and shoot one bullet and remove it from the magazine
        else
		{
            shoot();
        }
        //get the animation and states from the tool
        animation = animationName;
        states = gunStates;
    }
    
    IEnumerator Reload()
	{
        yield return new WaitForSeconds(4.0f);
        bulletsLeft = ammo;
        reloading = false;
    }

    void shoot()
	{
        bulletsLeft--;
        //Debug.Log("bullets left: " + bulletsLeft);
        Vector3 angle = UserFirepoint.rotation.eulerAngles; //figure out the angle

        newBullet = Instantiate(bulletPrefab, UserFirepoint.position, Quaternion.Euler(angle)); //make the bullet
                                                                                                //the bullet controller takes things from here
        newBullet.GetComponent<BulletController>().source = UserFirepoint.transform.parent.parent.GetComponent<Collider>(); //be sure to set the bullets parent
    }
}
