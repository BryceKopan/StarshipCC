using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretController : MonoBehaviour {
    public float Health, RotationSpeed, ChargeTime, AttackTime;
    public GameObject AttackPrefab, ChargePrefab, ExplosionPrefab;

    bool moving = true;
    float currentHealth;
    private GameObject[] targets;

	// Use this for initialization
	void Start () 
    {
        currentHealth = Health;

        targets = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
    void Update () 
    {
        if(moving)
        {
            Vector3 rotationVector, attackPosition;

            attackPosition = GetAttackPosition();

            rotationVector = transform.position - attackPosition;
            rotationVector.Normalize();

            float angle = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotationSpeed);

            if(false)
            {
                moving = false;
                Attack();
            }
        }
    }

    Vector3 GetAttackPosition()
    {
        Vector3 targetPosition = targets[0].transform.position;
        float distanceToTargetPosition = 
            Vector3.Distance(targetPosition, transform.position);

        foreach(GameObject target in targets)
        {
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if(distanceToTarget < distanceToTargetPosition)
            {
                targetPosition = target.transform.position;
                distanceToTargetPosition = distanceToTarget;
            }
        }

        return targetPosition;
    }

    void Attack()
    {
        Debug.Log("Fire");
    }
}
