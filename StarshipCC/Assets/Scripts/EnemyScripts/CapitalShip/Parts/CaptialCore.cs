using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptialCore : MonoBehaviour, Hittable
{
    public float health;
	private float maxHealth;

    public float timeBetweenAttacks;
    public float attackDelay;

    public GameObject preAttackPrefab;
    public GameObject attackPrefab;

	public List<GameObject> deathPrefabList;

	protected bool isDead = false;

	[SerializeField] private List<GameObject> bulletSpawns = new List<GameObject>();
	private int numberOfLasers = 1;

	// Use this for initialization
	protected void Start () 
	{
		maxHealth = health;

        StartCoroutine(Attack());
	}
	
	private IEnumerator Attack()
    {
        while(!isDead)
        {
			List<Transform> attackPositions = new List<Transform>();
			int curNumberOfLasers = numberOfLasers;

			for(int i = 0; i < curNumberOfLasers; i++)
			{
				int r = Random.Range(0, bulletSpawns.Count);

				attackPositions.Add(bulletSpawns[r].transform);
				Instantiate(preAttackPrefab, attackPositions[i].position, attackPositions[i].rotation);
			}

            yield return new WaitForSeconds(attackDelay);

			for(int i = 0; i < curNumberOfLasers; i++)
			{
				Instantiate(attackPrefab, attackPositions[i].position, attackPositions[i].rotation);
			}

            yield return new WaitForSeconds(timeBetweenAttacks);
        }
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

		if(health < maxHealth/3*2 && numberOfLasers == 1)
			numberOfLasers++;
		else if(health < maxHealth/3 && numberOfLasers == 2)
			numberOfLasers++;

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
