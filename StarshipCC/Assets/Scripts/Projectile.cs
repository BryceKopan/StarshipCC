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
            (thisTag == Tags.ENEMY_BULLET && otherTag == Tags.ENEMY_BULLET) ||
            (thisTag == Tags.FRIENDLY_BULLET && otherTag == Tags.FRIENDLY_BULLET)))
        { 
            Hittable hittable = other.gameObject.GetComponent<Hittable>();

            if (hittable != null)
            {
                hittable.OnHit(this);
                Death();
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

        //Detach particles so they won't immediately disappear
        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem ps in particles)
        {
            ps.transform.parent = null;
            var emission = ps.emission;
            emission.enabled = false;

            float lifetime = ps.main.startLifetime.constantMax;

            Destroy(ps, lifetime);
        }

        Destroy(gameObject);
    }
}