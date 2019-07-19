using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunProjectile : Projectile
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Hittable hittable = other.gameObject.GetComponent<Hittable>();

        if (hittable != null)
        {
            hittable.OnHit(this);
        }
    }
}
