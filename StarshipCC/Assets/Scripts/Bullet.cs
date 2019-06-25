using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {
    public override void OnStart() 
    {
        base.OnStart();
        damage = 1;
        speed = 80;
        range = 200;
    }
}
