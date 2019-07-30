using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon {

    public bool isPenetrating = false;

    public GameObject projectilePrefab;

    public float bulletSpeed = 40f;

    public override void OnEquip(EnemyController enemy)
    {

    }

    public override void OnUnequip(EnemyController player)
    {

    }

    public override void OnAttackStart()
    {
    }

    public override void OnAttackStop() { }

    public override void OnCanAttack() { }

    protected void LaunchProjectiles()
    {
        PlayAttackSound();

        foreach (Transform bulletSpawn in attackSpawns)
        {
            var projectile = (GameObject)Instantiate(
                    projectilePrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);

            Vector3 localScale = projectile.transform.localScale;

            projectile.transform.localScale = new Vector3(localScale.x * attackScaleModifier, localScale.y * attackScaleModifier, localScale.z);

            if (gameObject.layer == Layers.PLAYER)
            {
                projectile.layer = Layers.FRIENDLY_ATTACK;
            }
            else if(gameObject.layer == Layers.ENEMY)
            {
                projectile.layer = Layers.ENEMY_ATTACK;
            }

            else
            {
                projectile.layer = Layers.ENEMY_ATTACK;
            }

            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if(projectileScript)
            {
                projectileScript.damage = damage;
                projectileScript.speed = bulletSpeed;
                projectileScript.range = range;
                projectileScript.isPenetrating = isPenetrating;

                //Add velocity to the bullet
                projectileScript.moveVector = bulletSpawn.transform.up;
            }
            else
            {
                Debug.LogError("Projectile script does not exist!");
            }
        }

        ApplyRecoil();
    }

    protected virtual void ApplyRecoil()
    {
        Rigidbody2D rigidbody = null;
        if (player)
        {
            rigidbody = player.GetComponent<Rigidbody2D>();
        }

        if (rigidbody)
        {
            rigidbody.AddForce((Quaternion.Euler(avgAttackDirection.x, avgAttackDirection.y, avgAttackDirection.z) * -transform.up ) * recoilMagnitude, ForceMode2D.Impulse);
        }
    }

    public override void OnEquip(PlayerController player) { }

    public override void OnUnequip(PlayerController player) { }

    public override void OnAttack()
    {
        for(int i = 0; i < numAttacks; i++)
        {
            Invoke("LaunchProjectiles", i * delayBetweenAttacks);
        }

        canAttack = false;
        Invoke("EnableAttack", delayBetweenAttacks * numAttacks + cooldown);
    }
}
