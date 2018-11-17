using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileWeapon : Weapon {

    public override void Fire()
    {
        if (canFire)
        {
            foreach (Transform bulletSpawn in bulletSpawns)
            {
                var missile = (GameObject)Instantiate(
                        bulletPrefab,
                        bulletSpawn.position,
                        bulletSpawn.rotation);

                missile.tag = Tags.FRIENDLY_BULLET;

                Projectile missileScript = missile.GetComponent<Projectile>();
                missileScript.damage = damage;
                missileScript.speed = bulletSpeed;
                missileScript.lifeTime = bulletLife;

                //Add velocity to the bullet
                missileScript.moveVector = bulletSpawn.transform.up;
            }
            canFire = false;
            Invoke("EnableFire", fireCooldown);
        }
    }

    public override void OnEquip(PlayerController player)
    {

    }

    public override void OnUnequip(PlayerController player)
    {

    }
}
