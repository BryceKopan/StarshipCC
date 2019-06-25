using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon {

    public GameObject projectilePrefab;

    public float bulletSpeed = 40f;

    public override void OnEquip(EnemyController enemy)
    {
        throw new NotImplementedException();
    }

    public override void OnUnequip(EnemyController player)
    {
        throw new NotImplementedException();
    }

    public override void OnAttackStart()
    {
        if (canAttack)
        {
            for (int i = 0; i < numAttacks; i++)
            {
                Invoke("LaunchProjectiles", i * delayBetweenAttacks);
            }
        }
    }

    public override void OnAttackEnd() { }

    public override void OnCanAttack() { }

    protected void LaunchProjectiles()
    {
        PlayAttackSound();

        foreach (Transform bulletSpawn in bulletSpawns)
        {
            var projectile = (GameObject)Instantiate(
                    projectilePrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);

            if (gameObject.tag == Tags.PLAYER)
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

    public override void OnEquip(PlayerController player) { }

    public override void OnUnequip(PlayerController player) { }
}
