using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	public GameObject setActiveOnGameOver;
	public GameObject GameOverScoreText;

	private List<GameObject> players;
	private bool gameStarted = false;
	private bool gameIsOver = false;

	private int coins = 0;
	private UnityEngine.UI.Text coinCounter;

	private int score = 0;

	// Use this for initialization
	void Start () 
	{
		coinCounter = GameObject.Find("CoinCounter").GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!gameStarted)
		{
			players = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.PLAYER));

			for(int i = 0; i < players.Count; i++)
			{
				if(players[i].name != "Ship")
				{
					players.RemoveAt(i);
				}
			}

			if(players.Count > 0)
			{
				gameStarted = true;
			}
		}

		if(!isAPlayerAlive() && gameStarted)
		{
			GameOver();
		}

		if(gameIsOver && gameStarted)
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
		GameOverScoreText.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
	}

	public void AddScore(int newScore)
	{
		score += newScore;
	}

	public int GetScore()
	{
		return score;
	}

    public void AddCoins(int coin)
    {
        coins += coin;
        coinCounter.text = ": " + coins;
    }

    public int GetCoins()
    {
        return coins;
    }
}
