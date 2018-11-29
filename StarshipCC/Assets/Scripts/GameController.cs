using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject setActiveOnGameOver;

	private List<GameObject> players;

	// Use this for initialization
	void Start () {
		players = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.PLAYER));

		for(int i = 0; i < players.Count; i++)
		{
			if(players[i].name != "ship")
			{
				players.RemoveAt(i);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isAPlayerAlive())
		{
			GameOver();
		}
	}

	bool isAPlayerAlive()
	{
		int livingPlayerCount = players.Count;

		foreach(GameObject player in players)
		{
			if(!player.activeSelf)
			{
				livingPlayerCount--;
			}
		}

		if(livingPlayerCount <= 0)
		{
			return false;
		}

		return true;
	}

	void GameOver()
	{
		setActiveOnGameOver.SetActive(true);
	}
}
