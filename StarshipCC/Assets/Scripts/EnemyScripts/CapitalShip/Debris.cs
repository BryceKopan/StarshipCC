using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour, Hittable
{
	public float health;
	public List<GameObject> deathPrefabList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected virtual void Death()
	{
		SpawnDeathPrefabs();

		Destroy(gameObject);
	}

	void Hittable.OnHit(Projectile p)
    {
		health -= p.damage;
		if(health <= 0)
			Death();

        p.Death();
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