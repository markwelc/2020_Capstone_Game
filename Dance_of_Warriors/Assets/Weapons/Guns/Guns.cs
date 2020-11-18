using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : WeaponController
{
    /*[SerializeField] private*/ public GameObject bulletPrefab;
    /*[SerializeField] private*/ public Transform UserFirepoint;
    private GameObject newBullet;

    public override void useWeapon(string weaponName, out string animation, out float[] states)
    {
        Vector3 angle = UserFirepoint.rotation.eulerAngles; //figure out the angle

        newBullet = Instantiate(bulletPrefab, UserFirepoint.position, Quaternion.Euler(angle)); //make the bullet
            //the bullet controller takes things from here
        newBullet.GetComponent<BulletController>().source = UserFirepoint.transform.parent.parent.GetComponent<Collider>(); //be sure to set the bullets parent

        animation = animationName;
        states = gunStates;
        
    }
}
