using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DegradingSprite))]
public class HiddenRoomController : MonoBehaviour, Hittable
{
	public float health;

	public List<GameObject> activateOnDeath = new List<GameObject>();
	public GameObject spawnedItemPrefab;
	public GameObject spawnedItemLocation;

	public int lowerLootLimit, upperLootLimit;
	public GameObject spawnedLootPrefab;
	public List<GameObject> spawnedLootLocations;

	private DegradingSprite degradingSprite;
	private bool isDead = false;

	void Start () 
	{
		degradingSprite = gameObject.GetComponent<DegradingSprite>();
		degradingSprite.SetMaxHealth(health);
	}
	
	void Update () 
	{
		
	}

	void Death()
	{
		if(!isDead)
        {
			foreach(GameObject go in activateOnDeath)
			{
				go.SetActive(true);
			}

			Instantiate(
				spawnedItemPrefab,
				spawnedItemLocation.transform.position,
				spawnedItemLocation.transform.rotation);

			int rLoot = Random.Range(lowerLootLimit, upperLootLimit);
			for(int i = 0; i < rLoot; i++)
			{	
				int rLocation = Random.Range(0, spawnedLootLocations.Count);
				Instantiate(
					spawnedLootPrefab,
					spawnedLootLocations[rLocation].transform.position,
					spawnedLootLocations[rLocation].transform.rotation);
			}

			isDead = true;
			gameObject.SetActive(false);
		}
	}

	void Hittable.OnHit(Projectile p)
    {
		health -= p.damage;
		degradingSprite.SetSprite(health);

		if(health <= 0)
			Death();
    }
}
