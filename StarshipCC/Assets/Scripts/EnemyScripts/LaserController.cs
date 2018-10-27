using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserController : MonoBehaviour, Hittable {
    public float Health, Speed, ChargeTime, AttackTime;
    public GameObject AttackPrefab, ChargePrefab, ExplosionPrefab;
    
    private Vector3 leftBound, rightBound;
    bool moving = true, aiming = true;
    float currentHealth;
    private GameObject[] targets;

	// Use this for initialization
	void Start () 
    {
        currentHealth = Health;

        leftBound = rightBound = transform.position;
        leftBound.x -= 33;
        rightBound.x += 33;

        targets = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
    void Update () 
    {
        if(moving)
        {
            Vector3 movementVector, attackPosition;

            attackPosition = GetAttackPosition();
            if(attackPosition.x > leftBound.x && attackPosition.x < rightBound.x)
            {
                movementVector = GetMovementVectorToAttack(attackPosition);    
            }
            else
            {
                movementVector = new Vector3(0, 0, 0);
            }

            transform.position += movementVector * Speed * Time.deltaTime;

            if(Math.Abs(attackPosition.x - transform.position.x) < .5)
            {
                moving = false;
                if(aiming)
                    ChargeAttack();
            }
        }
    }

    void LateUpdate()
    {
        if(GetActiveTargetCount() == 0)
            aiming = false;
        else
            aiming = true;
    }

    int GetActiveTargetCount()
    {
        int count = 0;
        for(int i = 0; i < targets.Length; i++)
        {
            if(targets[i].activeSelf)
                count++;
        }

        return count;
    }

    Vector3 GetMovementVectorToAttack(Vector3 attackPosition)
    {
        Vector3 movementVector;

        movementVector = attackPosition - transform.position;
        movementVector.y = 0;
        movementVector.z = 0;
        movementVector.Normalize();

        return movementVector;
    }

    Vector3 GetAttackPosition()
    {
        Vector3 targetPosition = targets[0].transform.position;
        float distanceToTargetPosition = 
            Vector3.Distance(targetPosition, transform.position);

        foreach(GameObject target in targets)
        {
            if(target.activeSelf)
            {
                float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
                if(distanceToTarget < distanceToTargetPosition)
                {
                    targetPosition = target.transform.position;
                    distanceToTargetPosition = distanceToTarget;
                }
            }
        }

        return targetPosition;
    }

    void ChargeAttack()
    {
        var charge = (GameObject)Instantiate (
                ChargePrefab,
                transform.position,
                transform.rotation);

        Destroy(charge, ChargeTime);
        Invoke("Attack", ChargeTime);
    }

    void Attack()
    {
        var attack = (GameObject)Instantiate (
                AttackPrefab,
                transform.position,
                transform.rotation);

        Destroy(attack, AttackTime);
        Invoke("Move", AttackTime);
    }

    void Move()
    {
        moving = true;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
            Death();
    }

    void Death()
    {
        Instantiate(
                ExplosionPrefab,
                transform.position,
                transform.rotation);

        Destroy(gameObject);
    }

    void Hittable.OnHit(Projectile p)
    {
        TakeDamage(p.damage);
        p.Death();
    }
}
