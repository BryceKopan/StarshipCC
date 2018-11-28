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

    void OnTriggerEnter2D(Collider2D other)
    {
        string thisTag = gameObject.tag;
        string otherTag = other.gameObject.tag;
        if(!((thisTag == Tags.ENEMY_BULLET && otherTag == Tags.ENEMY) || 
            (thisTag == Tags.FRIENDLY_BULLET && otherTag == Tags.PLAYER) || 
            (thisTag == Tags.ENEMY_BULLET && otherTag == Tags.ENEMY_BULLET)))
        { 
            Hittable hittable = other.gameObject.GetComponent<Hittable>();

            if (hittable != null)
            {
                hittable.OnHit(this);
            }
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