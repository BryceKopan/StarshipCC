using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretController : MonoBehaviour, Hittable {
    public float Health, RotationSpeed, numberOfShots, timeBetweenShots, pauseAfterShooting, shotScale;
    public GameObject AttackPrefab, ExplosionPrefab;
    public List<Sprite> healthSprites;
    
    bool moving = true, aiming = true;
    float currentHealth;
    float bulletMoveSpeed = 40f;
    private GameObject[] targets;
    private SpriteRenderer sr;

	// Use this for initialization
	void Start () 
    {
        currentHealth = Health;

        targets = GameObject.FindGameObjectsWithTag("Player");

        sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
    void Update () 
    {
        if(moving)
        {
            Vector3 rotationVector, attackPosition;
            
            if(aiming)
            {
                attackPosition = GetAttackPosition();

                rotationVector = transform.position - attackPosition;
                rotationVector.Normalize();
            }
            else
            {
                rotationVector = new Vector3(0, 1, 0);
            }

            float angle = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotationSpeed);

            if(moving && Quaternion.Angle(q, transform.rotation) < 5)
            {
                moving = false;
                if(aiming)
                    Attack();
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

    void Attack()
    {
        for(int i = 0; i < numberOfShots; i++)
        {
            Invoke("Fire", i * timeBetweenShots);
        }
        Invoke("Move", (numberOfShots * timeBetweenShots) + pauseAfterShooting);
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
        bulletScript.moveVector = -transform.up * bulletMoveSpeed * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        CheckSprite();
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

    void CheckSprite()
    {
        float healthPercentage = currentHealth/Health;
        int numberOfHealthSprites = healthSprites.Count;
        float stepSize = 1f / numberOfHealthSprites;

        for(int i = 0; i < numberOfHealthSprites; i++)
        {
            if(healthPercentage < 1f - (i * stepSize) &&
                    healthPercentage > 1f - ((i + 1) * stepSize))
            {
                sr.sprite = healthSprites[i];
            }
        }
    }
}
