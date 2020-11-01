using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : WeaponController
{
    /*[SerializeField] private*/ public GameObject bulletPrefab;
    /*[SerializeField] private*/ public Transform UserFirepoint;
    private GameObject newBullet;

    public override void useWeapon()
    {
        Vector3 angle = UserFirepoint.rotation.eulerAngles; //figure out the angle

        newBullet = Instantiate(bulletPrefab, UserFirepoint.position, Quaternion.Euler(angle)); //make the bullet
            //the bullet controller takes things from here
        newBullet.GetComponent<BulletController>().source = UserFirepoint.transform.parent.parent.GetComponent<Collider>(); //be sure to set the bullets parent
        //newBullet.lifespan = 3000;
    }
}
