using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [HideInInspector]
    public float damage, speed, range;
    float distanceTraveled = 0;
    [HideInInspector]
    public Vector3 moveVector;

    public GameObject DeathEffectPrefab;

    public void Start()
    {
        OnStart();
    }

    public virtual void OnStart(){}

    public virtual void FixedUpdate()
    {
        if(distanceTraveled > range) 
        {
            Death();
        }
        else 
        {
            Vector3 movement = moveVector * speed * Time.fixedDeltaTime;
            distanceTraveled += movement.magnitude;
            transform.position += movement;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Hittable hittable = other.gameObject.GetComponent<Hittable>();

        if (hittable != null)
        {
            hittable.OnHit(this);
            Death();
        }
    }

    public virtual void Death()
    {
        if(DeathEffectPrefab)
        {
            Instantiate(
            DeathEffectPrefab,
            gameObject.transform.position,
            gameObject.transform.rotation);
        }

        Destroy(gameObject);
    }
}