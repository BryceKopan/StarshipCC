using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDebris : Debris {
	public GameObject coinPrefab;
	public int lowerCoinLimit, upperCoinLimit;
	protected override void Death()
	{
		int rCoinNum = Random.Range(lowerCoinLimit, upperCoinLimit);
		for(int i = 0; i < rCoinNum; i++)
		{
			SpawnCoin();
		}

		Instantiate(
                deathEffectPrefab,
                transform.position,
                transform.rotation);

		Destroy(gameObject);
	}

	void SpawnCoin()
	{
		GameObject coin = Instantiate(
                coinPrefab,
                transform.position,
                transform.rotation);

		float r1 = UnityEngine.Random.Range(-1000f,1000f);
		float r2 = UnityEngine.Random.Range(-1000f,1000f);
        Vector2 force = new Vector2(r1, r2);
        coin.GetComponent<Rigidbody2D>().AddForce(force);
	}
}
