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

	private DegradingSprite degradingSprite;

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
		foreach(GameObject go in activateOnDeath)
		{
			go.SetActive(true);
		}

		Instantiate(
            spawnedItemPrefab,
			spawnedItemLocation.transform.position,
			spawnedItemLocation.transform.rotation);

		gameObject.SetActive(false);
	}

	void Hittable.OnHit(Projectile p)
    {
		health -= p.damage;
		degradingSprite.SetSprite(health);

		if(health <= 0)
			Death();

        p.Death();
    }
}
