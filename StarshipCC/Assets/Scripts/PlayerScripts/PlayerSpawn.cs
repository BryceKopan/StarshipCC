using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    bool[] Players = new bool[] { false, false, false, false };

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator setPlayers(bool[] pA)
    {
        Players = pA;
        yield return new WaitForSeconds(1);
        spawnPlayers();
    }

    void spawnPlayers()
    {
        if (Players[0])
        {
            GameObject player01 = (GameObject)Instantiate(player1, new Vector3(-75.0f, -50.0f, 0), Quaternion.identity);
        }
        if (Players[1])
        {
            GameObject player02 = (GameObject)Instantiate(player2, new Vector3(-40.0f, -50.0f, 0), Quaternion.identity);
        }
        if (Players[2])
        {
            GameObject player03 = (GameObject)Instantiate(player3, new Vector3(40.0f, -50.0f, 0), Quaternion.identity);
        }
        if (Players[3])
        {
            GameObject player04 = (GameObject)Instantiate(player4, new Vector3(75.0f, -50.0f, 0), Quaternion.identity);
        }
       
        
        
    }
}
