using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour, Hittable
{
	public float health;
	public List<GameObject> deathPrefabList;

	protected bool isDead = false;

	// Use this for initialization
	protected void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected virtual void Death()
	{
		if(!isDead)
		{
			SpawnDeathPrefabs();

			isDead = true;
			Destroy(gameObject);
		}
	}

	void Hittable.OnHit(Projectile p)
    {
		health -= p.damage;

		if(health <= 0)
			Death();
    }

	protected void SpawnDeathPrefabs()
	{
		foreach(GameObject deathPrefab in deathPrefabList)
		{
			Instantiate(
					deathPrefab,
					transform.position,
					transform.rotation);
		}
	}
}