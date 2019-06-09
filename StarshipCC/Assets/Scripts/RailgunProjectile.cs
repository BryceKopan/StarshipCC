using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunProjectile : Projectile
{
    void OnTriggerEnter2D(Collider2D other)
    {
        string thisTag = gameObject.tag;
        string otherTag = other.gameObject.tag;
        if (!((thisTag == Tags.ENEMY_BULLET && otherTag == Tags.ENEMY) ||
            (thisTag == Tags.FRIENDLY_BULLET && otherTag == Tags.PLAYER) ||
            (thisTag == Tags.ENEMY_BULLET && otherTag == Tags.ENEMY_BULLET)))
        {
            Hittable hittable = other.gameObject.GetComponent<Hittable>();

            if (hittable != null)
            {
                hittable.OnHit(this);
            }
        }
    }
}
