using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretController : MonoBehaviour, Hittable {
    public float Health, RotationSpeed, numberOfShots, timeBetweenShots, shotScale;
    public GameObject AttackPrefab, ExplosionPrefab;

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

            if(moving && Quaternion.Angle(q, transform.rotation) < 5)
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
        for(int i = 0; i < numberOfShots; i++)
        {
            Invoke("Fire", i * timeBetweenShots);
        }
        Invoke("Move", numberOfShots * timeBetweenShots);
    }

    void Move()
    {
        moving = true;
    }

    void Fire()
    {
        var bullet = (GameObject) Instantiate(
                AttackPrefab,
                transform.position,
                transform.rotation);

        bullet.transform.localScale = new Vector3(1 * shotScale, 1 * shotScale, 1);

        bullet.tag = Tags.ENEMY_BULLET;
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        //Add velocity to the bullet
        bulletScript.moveVector = -transform.up * bulletScript.bulletMoveSpeed * Time.deltaTime;
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
