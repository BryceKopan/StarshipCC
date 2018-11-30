using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject setActiveOnGameOver;

	private List<GameObject> players;

	private bool gameIsOver = false;

	// Use this for initialization
	void Start () {
		players = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.PLAYER));

		for(int i = 0; i < players.Count; i++)
		{
			if(players[i].name != "Ship")
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

		if(gameIsOver)
		{
			if (Input.GetKey("joystick button 7") || Input.GetKey(KeyCode.Space))
			{
				SceneManager.LoadScene(2);
			}
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
		gameIsOver = true;
		setActiveOnGameOver.SetActive(true);
	}
}
