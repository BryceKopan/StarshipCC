using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedProjectileWeapon : ProjectileWeapon
{
    public bool charging = false;
    public float chargePercentage = 0f;
    public float chargeRate = 0.01f;
    public float sizeModifier = 2f;

    public ParticleSystem chargeParticles;

    public void Start()
    {
        if(chargeParticles)
        {
            chargeParticles.Stop();
        }
    }

    public override void OnAttackStart()
    {
        base.OnAttackStart();
        charging = true;
        if(chargeParticles)
        {
            chargeParticles.Play();
        }
    }

    public override void OnAttack()
    {
        chargePercentage = Mathf.Min(1, chargePercentage + chargeRate);
    }

    public override void OnAttackStop()
    {
        base.OnAttackStop();
        charging = false;
        LaunchChargedProjectiles();
        chargePercentage = 0;
        if(chargeParticles)
        {
            chargeParticles.Stop();
        }
    }

    public void LaunchChargedProjectiles()
    {
        Debug.Log("Charge percentage: " + chargePercentage);

        PlayAttackSound();

        foreach (Transform bulletSpawn in attackSpawns)
        {
            var projectile = (GameObject)Instantiate(
                    projectilePrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);

            Vector3 localScale = projectile.transform.localScale;

            projectile.transform.localScale = new Vector3((localScale.x * attackScaleModifier) * chargePercentage, (localScale.y * attackScaleModifier) * chargePercentage, localScale.z);

            if (gameObject.layer == Layers.PLAYER)
            {
                projectile.layer = Layers.FRIENDLY_ATTACK;
            }
            else if (gameObject.layer == Layers.ENEMY)
            {
                projectile.layer = Layers.ENEMY_ATTACK;
            }

            else
            {
                projectile.layer = Layers.ENEMY_ATTACK;
            }

            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.damage = damage * chargePercentage;
            projectileScript.speed = bulletSpeed;
            projectileScript.range = range;

            //Add velocity to the bullet
            projectileScript.moveVector = bulletSpawn.transform.up;
        }

        //Scale recoil temporarily based on charge percentage
        float maxRecoil = recoilMagnitude;
        recoilMagnitude *= chargePercentage;
        ApplyRecoil();
        recoilMagnitude = maxRecoil;
    }
}
