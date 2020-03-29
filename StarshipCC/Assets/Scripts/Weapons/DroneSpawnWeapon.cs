using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawnWeapon : Weapon
{
    public GameObject dronePrefab;

    public override void OnEquip(EnemyController enemy) { }

    public override void OnUnequip(EnemyController player) { }

    public override void OnEquip(PlayerController player) { }

    public override void OnUnequip(PlayerController player) { }

    public override void OnAttackStart() { }

    public override void OnAttackStop() { }

    public override void OnCanAttack() { }

    protected void LaunchDrones()
    {
        PlayAttackSound();

        foreach (Transform bulletSpawn in attackSpawns)
        {
            var drone = (GameObject)Instantiate(
                    dronePrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);

            Vector3 localScale = drone.transform.localScale;

            drone.transform.localScale = new Vector3(localScale.x * attackScaleModifier, localScale.y * attackScaleModifier, localScale.z);

            if (gameObject.layer == Layers.PLAYER)
            {
                drone.layer = Layers.FRIENDLY_ATTACK;
            }
            else if (gameObject.layer == Layers.ENEMY)
            {
                drone.layer = Layers.ENEMY_ATTACK;
            }
            else
            {
                drone.layer = Layers.ENEMY_ATTACK;
            }
        }
    }

    public override void OnAttack()
    {
        for (int i = 0; i < numAttacks; i++)
        {
            Invoke("LaunchDrones", i * delayBetweenAttacks);
        }

        canAttack = false;
        Invoke("EnableAttack", delayBetweenAttacks * numAttacks + cooldown);
    }
}
