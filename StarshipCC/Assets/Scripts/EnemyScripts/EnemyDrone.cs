using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : EnemyController
{
    protected override void OnStart()
    {
        EquipAllChildWeapons();
    }

    protected override void Attack()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.StartAttack();
        }

        Invoke("DoneAttacking", longestWeaponFireTime);
    }

    protected override SimpleTransform GetTransformToAttack(Vector3 attackPosition)
    {
        SimpleTransform deltaTransform = new SimpleTransform();

        Vector3 rotationVector = attackPosition - transform.position;
        moveVector = rotationVector.normalized;
        float angle = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        deltaTransform.rotation = q;

        return deltaTransform;
    }

    protected override bool IsInRange(Vector3 attackPosition)
    {
        Vector3 rotationVector = attackPosition - transform.position;

        // Check distance
        if (rotationVector.magnitude > longestWeaponRange)
        {
            return false;
        }

        // Check angle
        float angle = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Quaternion.Angle(q, transform.rotation) >= 5)
        {
            return false;
        }

        return true;
    }

    // Update is called once per frame
    protected override void OnFixedUpdate()
    {
        if (currentState == State.Moving)
        {
            MoveTowardsTarget();
        }
    }

    protected void MoveTowardsTarget()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(moveVector * moveSpeed);
    }
}
