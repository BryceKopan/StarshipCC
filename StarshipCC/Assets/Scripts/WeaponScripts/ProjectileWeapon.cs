using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon {

    public GameObject projectilePrefab;

    public float bulletSpeed = 40f;
    public float bulletLife = 10f;

    public override void Fire()
    {
        if (canFire)
        {
            foreach (Transform bulletSpawn in bulletSpawns)
            {
                var projectile = (GameObject)Instantiate(
                        projectilePrefab,
                        bulletSpawn.position,
                        bulletSpawn.rotation);

                projectile.tag = Tags.FRIENDLY_BULLET;

                Projectile projectileScript = projectile.GetComponent<Projectile>();
                projectileScript.damage = damage;
                projectileScript.speed = bulletSpeed;
                projectileScript.lifeTime = bulletLife;

                //Add velocity to the bullet
                projectileScript.moveVector = bulletSpawn.transform.up;
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

    public override void OnFireStart()
    {
    }

    public override void OnFireEnd()
    {
    }
}
