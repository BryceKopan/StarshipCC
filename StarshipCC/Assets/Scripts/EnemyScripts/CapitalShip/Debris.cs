using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour, Hittable
{
	public GameObject deathEffectPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Death()
	{
		Destroy(gameObject);
	}

	void Hittable.OnHit(Projectile p)
    {
		Instantiate(
                deathEffectPrefab,
                transform.position,
                transform.rotation);

        Death();

        p.Death();
    }
}