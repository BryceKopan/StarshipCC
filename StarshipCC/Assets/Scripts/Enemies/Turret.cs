﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : EnemyController 
{
    protected override void OnStart()
    {
        EquipAllChildWeapons();
    }

    protected override SimpleTransform GetTransformToAttack(Vector3 attackPosition)
    {
        SimpleTransform deltaTransform = new SimpleTransform();

        Vector3 rotationVector = transform.position - attackPosition;
        float angle = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        deltaTransform.rotation = q;

        return deltaTransform;
    }

    protected override bool IsInRange(Vector3 attackPosition)
    {
        Vector3 rotationVector = transform.position - attackPosition;
        float angle = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        if(Quaternion.Angle(q, transform.rotation) < 5)
            return true;

        return false;
    }

    protected override void Attack()
    {
        foreach (Weapon weapon in weapons) 
        {
            weapon.StartAttack();
        }

        Invoke("DoneAttacking", longestWeaponFireTime);
    }
}
