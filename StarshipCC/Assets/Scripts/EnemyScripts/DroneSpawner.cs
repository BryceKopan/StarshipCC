using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : EnemyController
{
    public float spawnRange = 40;
    public int numLivingDrones = 0;
    public int maxDrones = 5;

    protected override void Attack()
    {
        if(numLivingDrones < maxDrones)
        {
            // Weapons should all be drone spawn weapons;
            foreach (Weapon weapon in weapons)
            {
                weapon.StartAttack();
            }

            Invoke("DoneAttacking", longestWeaponFireTime);
        }
        else
        {
            Invoke("DoneAttacking", 0);
        }
    }

    protected override SimpleTransform GetTransformToAttack(Vector3 attackPosition)
    {
        return new SimpleTransform();
    }

    protected override bool IsInRange(Vector3 attackPosition)
    {
        Vector3 distanceVector = attackPosition - transform.position;
        return distanceVector.magnitude < spawnRange;
    }

    protected override void OnStart()
    {
        EquipAllChildWeapons();
    }

    public void OnDroneSpawned()
    {
        numLivingDrones++;
    }

    public void OnDroneDestroyed()
    {
        numLivingDrones--;
    }
}
