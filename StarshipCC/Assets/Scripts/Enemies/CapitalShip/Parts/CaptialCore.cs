using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptialCore : MonoBehaviour, Hittable
{
	public int phase = 0;

    public float health;
	private float maxHealth;

    public float timeBetweenLaser;
    public float timeToChargeLaser;

	public GameObject idlePrefab, chargePrefab, attackPrefab, explosionPrefab, pulsePrefab;

	public List<GameObject> deathPrefabList;

	protected bool isDead = false;

	[SerializeField] private List<GameObject> activeLaserSpawns = new List<GameObject>();
	[SerializeField] private List<GameObject> notActiveLaserSpawns = new List<GameObject>();

	private List<GameObject> activeParticles = new List<GameObject>();
	private int numberOfLasers = 3;

	// Use this for initialization
	protected void Start () 
	{
		maxHealth = health;

		foreach(GameObject spawn in activeLaserSpawns)
		{
			ActivateLaserSpawn(spawn);
		}

        StartCoroutine(Attack());
	}
	
	private IEnumerator Attack()
    {
        while(!isDead)
        {
			List<GameObject> attackPositions = new List<GameObject>();
			int curNumberOfLasers = numberOfLasers;

			for(int i = 0; i < curNumberOfLasers; i++)
			{
				int r;
				do{
					r = Random.Range(0, activeLaserSpawns.Count);
				}while(attackPositions.Contains(activeLaserSpawns[r]));

				attackPositions.Add(activeLaserSpawns[r]);

				activeParticles.Add(Instantiate(chargePrefab, attackPositions[i].transform.position, attackPositions[i].transform.rotation));

				ParticleSystem ps = chargePrefab.GetComponent<ParticleSystem>();
				ps.Stop();
 
				var main = ps.main;
				main.duration = timeToChargeLaser;
		
				ps.Play();
			}

            yield return new WaitForSeconds(timeToChargeLaser);

			for(int i = 0; i < curNumberOfLasers; i++)
			{
				Instantiate(attackPrefab, attackPositions[i].transform.position, attackPositions[i].transform.rotation);
			}

            yield return new WaitForSeconds(timeBetweenLaser);
        }
    }

	private void ActivateLaserSpawn(GameObject laserSpawn)
	{
		if(!activeLaserSpawns.Contains(laserSpawn))
		{
			activeLaserSpawns.Add(laserSpawn);

			Instantiate(explosionPrefab, laserSpawn.transform.position, laserSpawn.transform.rotation);
			Instantiate(pulsePrefab, laserSpawn.transform.position, laserSpawn.transform.rotation);
		}

		activeParticles.Add(Instantiate(idlePrefab, laserSpawn.transform.position, laserSpawn.transform.rotation));
	}

	protected virtual void Death()
	{
		if(!isDead)
		{
			SpawnDeathPrefabs();

			StopActiveParticles();

			isDead = true;
			Destroy(gameObject);
		}
	}

	void Hittable.OnHit(Projectile p)
    {
		health -= p.damage;

		if(health < maxHealth/3*2 && phase == 0)
		{
			ChangePhase();
		}
		else if(health < maxHealth/3 && phase == 1)
		{
			ChangePhase();
		}

		if(health <= 0)
			Death();
    }

	private void ChangePhase()
	{
		phase++;

		for(int i = 0; i < 2; i++)
		{
			int r = Random.Range(0,notActiveLaserSpawns.Count);

			ActivateLaserSpawn(notActiveLaserSpawns[r]);
			notActiveLaserSpawns.Remove(notActiveLaserSpawns[r]);
		}
		numberOfLasers++;
	}

	protected void StopActiveParticles()
	{
		for(int i = 0; i < activeParticles.Count; i++)
		{
			if(activeParticles[i] != null)
			{
				activeParticles[i].GetComponent<ParticleSystem>().Stop();
			}

			activeParticles.RemoveAt(i);
			i--;
		}
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
