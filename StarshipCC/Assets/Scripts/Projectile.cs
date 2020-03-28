using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public bool isPenetrating = false;

    [HideInInspector]
    public float damage, speed, range;
    float distanceTraveled = 0;
    [HideInInspector]
    public Vector3 moveVector;

    public GameObject DeathEffectPrefab;

    public bool orphanParticlesOnDeath = true; 

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
            if(!isPenetrating)
            {
                Death();
            }
        }
    }

    public virtual void Death()
    {
        if(DeathEffectPrefab)
        {
            GameObject effectInstance = Instantiate(DeathEffectPrefab);
            effectInstance.transform.position = transform.position;
            effectInstance.transform.rotation = transform.rotation;
        }

        if(orphanParticlesOnDeath)
        {
            ParticleSystem[] childParticles = GetComponentsInChildren<ParticleSystem>(); 
            foreach(ParticleSystem particles in childParticles)
            {
                particles.transform.SetParent(null);
                particles.gameObject.AddComponent<DestroyWhenNoParticles>();
                particles.Stop();
            }
        }

        Destroy(gameObject);
    }
}