using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoinsOnDeath : MonoBehaviour {

	public GameObject coinPrefab;
	public int lowerCoinLimit, upperCoinLimit;
	public float lowerForceLimit, upperForceLimit;

	private bool isQuitting;

	void OnDestroy()
	{
		if(!isQuitting)
		{
			int rCoinNum = Random.Range(lowerCoinLimit, upperCoinLimit);
			for(int i = 0; i < rCoinNum; i++)
			{
				SpawnCoin();
			}
		}
	}

	void SpawnCoin()
	{
		GameObject coin = Instantiate(
                coinPrefab,
                transform.position,
                transform.rotation);

		float r1 = UnityEngine.Random.Range(lowerForceLimit, upperForceLimit);
		float r2 = UnityEngine.Random.Range(lowerForceLimit, upperForceLimit);
		Vector2 force = new Vector2(r1, r2);
		coin.GetComponent<Rigidbody2D>().AddForce(force);
	}

	void OnApplicationQuit()
	{
		isQuitting = true;
	}
}
