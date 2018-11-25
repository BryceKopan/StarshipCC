using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHazard : MonoBehaviour {
	public Vector3 lowerSpawnPoint, upperSpawnPoint;
	public float lowerSpawnInterval, upperSpawnInterval, lowerSize, upperSize, speed;
	public List<GameObject> debrisPrefabs;
	public float treasureChance;
	public List<GameObject> treasurePrefabs;
	public float coinChance;
	public List<GameObject> coinPrefabs;

	// Use this for initialization
	void Start () {
		SpawnMapHazard();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnMapHazard()
	{
		float rType = Random.value;
		float rYPosition = Random.Range(lowerSpawnPoint.y, upperSpawnPoint.y);
		Vector3 position = new Vector3(lowerSpawnPoint.x, rYPosition, lowerSpawnPoint.z);
		position += transform.position;
		position.z = 0;

		if(rType <= treasureChance)
		{
			SpawnTreasureDebris(position);
		}
		else if(rType <= treasureChance + coinChance)
		{
			SpawnCoinDebris(position);
		}
		else
		{
			SpawnDebris(position);
		}

		float rTime  = Random.Range(lowerSpawnInterval, upperSpawnInterval);
		Invoke("SpawnMapHazard", rTime);
	}

	void SpawnDebris(Vector3 position)
	{
		int rPrefabNum = Random.Range(0, debrisPrefabs.Count);
        GameObject debrisPrefab = debrisPrefabs[rPrefabNum];

		var debris = Instantiate(
			debrisPrefab,
			position,
			Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

		float rSize = Random.Range(lowerSize, upperSize);
		debris.transform.localScale = new Vector3(rSize, rSize, 1);

		debris.GetComponent<Rigidbody2D>().AddForce(new Vector3 (-speed, 0, 0));
	}

	void SpawnTreasureDebris(Vector3 position)
	{
		int rPrefabNum = Random.Range(0, treasurePrefabs.Count);
        GameObject debrisPrefab = treasurePrefabs[rPrefabNum];

		var debris = Instantiate(
			debrisPrefab,
			position,
			Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

		debris.GetComponent<Rigidbody2D>().AddForce(new Vector3 (-speed, 0, 0));
	}

	void SpawnCoinDebris(Vector3 position)
	{
		int rPrefabNum = Random.Range(0, coinPrefabs.Count);
        GameObject debrisPrefab = coinPrefabs[rPrefabNum];

		var debris = Instantiate(
			debrisPrefab,
			position,
			Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

		debris.GetComponent<Rigidbody2D>().AddForce(new Vector3 (-speed, 0, 0));
	}
}
