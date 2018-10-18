using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour {

    public GameObject ShipPrefab;
    public float TimeBetweenSpawns;
    public Vector3 LeftXSpawn, RightXSpawn, TopYSpawn, BotYSpawn;

	// Use this for initialization
	void Start () {
		Invoke("SpawnShip", TimeBetweenSpawns);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void SpawnShip()
    {
        float rand = Random.Range(0f, 1f);
        float xSpawn;
        if(rand < .5)
        {
            xSpawn = LeftXSpawn.x;
        }
        else
        {
            xSpawn = RightXSpawn.x;
        }

        rand = Random.Range(BotYSpawn.y, TopYSpawn.y);
        Vector3 spawnLoc = 
            new Vector3(xSpawn, rand, transform.position.z);
        
        Instantiate(
                ShipPrefab,
                spawnLoc,
                transform.rotation);

		Invoke("SpawnShip", TimeBetweenSpawns);
    }
}
