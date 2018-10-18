using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour {

    public float health = 20f;
    public float turnSpeed = 5f;
    public float speed = 40f;

    public GameObject ExplosionPrefab;

    Vector2 moveDirection;
    private GameObject[] targets;

    Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();

	    targets = GameObject.FindGameObjectsWithTag("Player");	
	}
	
	// Update is called once per frame
	void Update () {
	    	
	}

    void FixedUpdate()
    {
        Vector3 attackPosition = GetAttackPosition();
        float moveX = attackPosition.x - transform.position.x;
        float moveY = attackPosition.y - transform.position.y;
        moveDirection = new Vector2(moveX, moveY);
        moveDirection.Normalize();

        rigidbody.AddForce(moveDirection * speed);

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnSpeed);

        if(Vector3.Distance(transform.position, attackPosition) < 10)
        {
            Death();
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
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
}
