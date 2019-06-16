using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class LobbySpawner : MonoBehaviour {

    bool[] playersJoined = new bool[] { false, false, false, false, false };
    bool[] playersReady = new bool[] { false, false, false, false, false };
    Vector3[] spawnPositions = new Vector3[5];

    public GameObject playerPrefab;

    XboxController[] controllers = new XboxController[4];

    public AudioClip playerJoinSound;
    public float soundVolume = 1.0f;

    private void Start()
    {
        // Init xbox controllers
        controllers[0] = XboxController.First;
        controllers[1] = XboxController.Second;
        controllers[2] = XboxController.Third;
        controllers[3] = XboxController.Fourth;

        // Init spawn positions
        spawnPositions[0] = new Vector3(-75f, 25f, 0f);
        spawnPositions[1] = new Vector3(75f, 25f, 0f);
        spawnPositions[2] = new Vector3(-75f, -25f, 0f);
        spawnPositions[3] = new Vector3(75f, 25f, 0f);
        spawnPositions[4] = new Vector3(0f, 0f, 0f);
    }

    void Update () {

        for(int i = 0; i < controllers.Length; i++)
        {
            // If A is pressed on a controller, add that player to the game
            if (XCI.GetButton(XboxButton.A, controllers[i]))
            {
                if(!playersJoined[i])
                {
                    SpawnPlayer(i);
                }
            }

            // If start is pressed on a controller, toggle ready for that player
            if (XCI.GetButton(XboxButton.Start, controllers[i]))
            {
                if (playersJoined[i])
                {
                    if(playersReady[i])
                    {
                        playersReady[i] = false;
                        //TODO remove indication that player is ready
                    }
                    else
                    {
                        playersReady[i] = true;
                        //TODO add indication that player is ready
                    }
                }
            }
        }

        // Check for keyboard player join
        if(Input.GetButton("KeyboardJoin"))
        {
            if(!playersJoined[4])
            {
                SpawnPlayer(4);
            }
        }

        // Check for keyboard player ready
        if(Input.GetButton("KeyboardReady"))
        {
            if (playersJoined[4])
            {
                if (playersReady[4])
                {
                    playersReady[4] = false;
                    //TODO remove indication that player is ready
                }
                else
                {
                    playersReady[4] = true;
                    //TODO add indication that player is ready
                }
            }
        }

        // If everyone is ready, start game
        if (CanStartGame())
        {
            Debug.Log("Starting game");
            SceneManager.LoadScene("ConstructedShip0");
        }
    }

    // Returns true if there is at least one player and all players are ready
    protected bool CanStartGame()
    {
        bool canStartGame = true;
        bool atLeastOnePlayer = false;
        for (int i = 0; i < playersJoined.Length; i++)
        {
            if(playersJoined[i])
            {
                atLeastOnePlayer = true;
                if(!playersReady[i])
                {
                    canStartGame = false;
                }
            }
        }

        if (!atLeastOnePlayer)
        {
            canStartGame = false;
        }

        return canStartGame;
    }

    protected void SpawnPlayer(int playerNum)
    {
        playersJoined[playerNum] = true;
        GameObject newPlayer = (GameObject)Instantiate(playerPrefab, spawnPositions[playerNum], Quaternion.identity);
        newPlayer.GetComponentInChildren<PlayerController>().PlayerNumber = playerNum + 1;

        DontDestroyOnLoad(newPlayer);

        // Play audio clip for player joining
        if (playerJoinSound)
        {
            AudioSource.PlayClipAtPoint(playerJoinSound, Camera.main.transform.position, soundVolume);
        }
    }
}
