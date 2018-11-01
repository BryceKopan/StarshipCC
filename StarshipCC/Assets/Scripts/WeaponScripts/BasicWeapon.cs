using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : Weapon
{
    public override void Fire()
    {
        if(canFire)
        {
            foreach (Transform bulletSpawn in bulletSpawns)
            {
                var bullet = (GameObject)Instantiate(
                        bulletPrefab,
                        bulletSpawn.position,
                        bulletSpawn.rotation);

                bullet.tag = Tags.FRIENDLY_BULLET;

                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.damage = damage;
                bulletScript.speed = bulletSpeed;
                bulletScript.lifeTime = bulletLife;

                //Add velocity to the bullet
                bulletScript.moveVector = bulletSpawn.transform.up;
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
