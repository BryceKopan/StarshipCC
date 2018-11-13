using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile, Hittable {
    void Hittable.OnHit(Projectile p)
    {  
        p.Death();
        Death();
    }
}
