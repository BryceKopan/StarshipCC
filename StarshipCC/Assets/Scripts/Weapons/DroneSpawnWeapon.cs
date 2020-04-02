using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawnWeapon : Weapon
{
    public DroneSpawner spawner;

    public GameObject dronePrefab;

    public override void OnEquip(EnemyController enemy)
    {
        spawner = enemy.GetComponent<DroneSpawner>();
    }

    public override void OnUnequip(EnemyController player)
    {
        spawner = null;
    }

    public override void OnEquip(PlayerController player)
    {
        spawner = player.GetComponent<DroneSpawner>();
    }

    public override void OnUnequip(PlayerController player)
    {
        spawner = null;
    }

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
                Debug.Log("Obj is enemy, setting drone layer to enemy attack");
            }
            else
            {
                drone.layer = Layers.ENEMY_ATTACK;
                Debug.Log("Obj is unknnown, setting drone layer to enemy attack");
            }

            //TODO refactor to allow for player drones
            EnemyDrone enemyDrone = drone.GetComponent<EnemyDrone>();
            if(enemyDrone)
            {
                enemyDrone.spawner = spawner;
            }

            if(spawner)
            {
                spawner.OnDroneSpawned();
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
