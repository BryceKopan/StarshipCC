using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHazard : MonoBehaviour {
	public Vector3 lowerSpawnPoint, upperSpawnPoint;
	public float lowerSpawnInterval, upperSpawnInterval, lowerSize, upperSize, speed;
	public List<GameObject> debrisPrefabs;

	// Use this for initialization
	void Start () {
		float rTime  = Random.Range(lowerSpawnInterval, upperSpawnInterval);
		Invoke("SpawnDebris", rTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnDebris()
	{
		int rPrefabNum = Random.Range(0, debrisPrefabs.Count);
        GameObject debrisPrefab = debrisPrefabs[rPrefabNum];

		float rYPosition = Random.Range(lowerSpawnPoint.y, upperSpawnPoint.y);

		var debris = Instantiate(
                debrisPrefab,
                new Vector3(lowerSpawnPoint.x, rYPosition, lowerSpawnPoint.z),
                Random.rotation);

		float rSize = Random.Range(lowerSize, upperSize);
		debris.transform.localScale = new Vector3(rSize, rSize, 1);

		debris.GetComponent<Rigidbody2D>().AddForce(new Vector3 (-speed, 0, 0));

		float rTime  = Random.Range(lowerSpawnInterval, upperSpawnInterval);
		Invoke("SpawnDebris", rTime);
	}
}
