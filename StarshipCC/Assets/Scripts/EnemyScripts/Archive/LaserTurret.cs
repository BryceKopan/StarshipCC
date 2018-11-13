using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserTurret : EnemyController 
{
    public float chargeTime = .5f, attackTime = .25f;
    public GameObject chargePrefab, attackPrefab;

    private Vector3 leftBound, rightBound;

    protected override void OnStart()
    {
        leftBound = rightBound = transform.position;
        leftBound.x -= 33;
        rightBound.x += 33;
    }

    protected override List<GameObject> TrimTargets(List<GameObject> targets)
    {
        List<GameObject> viableTargets = new List<GameObject>();
        targets = base.TrimTargets(targets);

        foreach(GameObject target in targets)
        {
            if(target.transform.position.x > leftBound.x &&
                    target.transform.position.x < rightBound.x)
            {
                viableTargets.Add(target);
            }
        }

        return viableTargets;
    }

    protected override SimpleTransform GetTransformToAttack(Vector3 attackPosition)
    {
        SimpleTransform deltaTransform = new SimpleTransform();

        deltaTransform.position = attackPosition - transform.position;
        deltaTransform.position.y = 0;
        deltaTransform.position.z = 0;
        deltaTransform.position.Normalize();

        return deltaTransform; 
    }

    protected override bool IsInRange(Vector3 attackPosition)
    {
        return Math.Abs(attackPosition.x - transform.position.x) < .5;
    }

    protected override void Attack()
    {   
        Charge();
        Invoke("Fire", chargeTime);
        Invoke("DoneAttacking", chargeTime + attackTime); 
    }

    private void Charge()
    {
        GameObject charge = Instantiate (
                chargePrefab,
                transform.position,
                transform.rotation);
         
        Destroy(charge, chargeTime);
    }

    private void Fire()
    {
        GameObject attack = Instantiate (
                attackPrefab,
                transform.position,
                transform.rotation);

        Destroy(attack, attackTime);
    }
}
