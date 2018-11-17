using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : Projectile, Hittable {
    void Hittable.OnHit(Projectile p)
    {
        if(p.tag != Tags.FRIENDLY_BULLET && p.tag != Tags.PLAYER)
        {
            p.Death();
            Death();
        }
    }
}
