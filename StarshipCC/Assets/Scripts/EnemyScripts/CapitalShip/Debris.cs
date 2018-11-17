using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour, Hittable
{
	public float health;
	public GameObject deathEffectPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Death()
	{
		Instantiate(
                deathEffectPrefab,
                transform.position,
                transform.rotation);

		Destroy(gameObject);
	}

	void Hittable.OnHit(Projectile p)
    {
		health -= p.damage;
		if(health <= 0)
			Death();

        p.Death();
    }
}