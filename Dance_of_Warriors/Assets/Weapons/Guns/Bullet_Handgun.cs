using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Handgun : BulletController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 1000;
        damage = 1;
        base.Start();
    }
}
