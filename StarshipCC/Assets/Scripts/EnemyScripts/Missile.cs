using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile, Hittable {
    public GameObject explosionPrefab;

    void Hittable.OnHit(Projectile p)
    {  
        p.Death();
        Death();
    }

    public override void Death()
    {
        Instantiate(
                    DeathEffectPrefab,
                    gameObject.transform.position,
                    gameObject.transform.rotation);
        Instantiate(
                    explosionPrefab,
                    gameObject.transform.position,
                    gameObject.transform.rotation);

        Destroy(gameObject);
    }
}
