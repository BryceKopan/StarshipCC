using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserController : MonoBehaviour {
    public float Health, Speed, ChargeTime, AttackTime, MoveDistance;
    public GameObject AttackPrefab, ChargePrefab;
    public Vector3 LeftBound, RightBound;

    bool moving = true;
    float currentHealth;
    Vector3 targetPosition;

	// Use this for initialization
	void Start () 
    {
        float startX = (((RightBound.x - LeftBound.x) / 2) + LeftBound.x);
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
		targetPosition = transform.position + new Vector3(MoveDistance, 0, 0);

        currentHealth = Health;
	}
	
	// Update is called once per frame
    void Update () 
    {
        if(moving)
        {
            if((transform.position.x > RightBound.x && MoveDistance > 0)
                || transform.position.x < LeftBound.x && MoveDistance < 0)
                MoveDistance = -MoveDistance;

            transform.position += (targetPosition - transform.position).normalized * Speed * Time.deltaTime;

            if(Math.Abs(targetPosition.x - transform.position.x) < .25)
            {
	            targetPosition = transform.position + new Vector3(MoveDistance, 0, 0);
                moving = false;
                ChargeAttack();
            }
        }
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

    void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
