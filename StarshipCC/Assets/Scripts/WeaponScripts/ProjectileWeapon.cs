using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon {

    public GameObject projectilePrefab;

    public float bulletSpeed = 40f;

    public override void Fire()
    {
        if (canFire)
        {
            for(int i = 0; i < numShots; i++) 
            {
                Invoke("LaunchProjectiles", i * delayBetweenShots);
            }
            
            Invoke("EnableFire", fireCooldown + delayBetweenShots * numShots);
            canFire = false;
        }
    }

    protected void LaunchProjectiles() 
    {
        PlayFireSound();

        foreach (Transform bulletSpawn in bulletSpawns) 
        {
            var projectile = (GameObject)Instantiate(
                    projectilePrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);

            if(gameObject.tag == Tags.PLAYER)
            {
                projectile.tag = Tags.FRIENDLY_BULLET;
            }
            else
            {
                projectile.tag = Tags.ENEMY_BULLET;
            }

            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.damage = damage;
            projectileScript.speed = bulletSpeed;
            projectileScript.range = range;

            //Add velocity to the bullet
            projectileScript.moveVector = bulletSpawn.transform.up;
        }
    }

    public override void OnEquip(PlayerController player)
    {
        base.OnEquip(player);
    }

    public override void OnUnequip(PlayerController player)
    {
        base.OnUnequip(player);
    }

    public override void OnFireStart()
    {
    }

    public override void OnFireEnd()
    {
    }
}
