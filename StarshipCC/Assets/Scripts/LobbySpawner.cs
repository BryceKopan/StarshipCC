using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class LobbySpawner : MonoBehaviour {

    GameObject[] players = new GameObject[5];
    bool[] playersReady = new bool[] { false, false, false, false, false };
    Vector3[] spawnPositions = new Vector3[5];

    int[] colorIndices = new int[] {0, 0, 0, 0, 0};
    [SerializeField] Color[] possibleColors;

    public GameObject playerPrefab;

    XboxController[] controllers = new XboxController[4];

    public AudioClip playerJoinSound;
    public float soundVolume = 1.0f;

    private void Start()
    {
        if(possibleColors.Length < 5)
        {
            Debug.LogError("Error: Not enough player colors for every player to be distinct!");
        }

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

    void Update() {

        for (int i = 0; i < controllers.Length; i++)
        {
            // If A is pressed on a controller, add that player to the game
            if (XCI.GetButtonDown(XboxButton.A, controllers[i]))
            {
                if (!players[i])
                {
                    SpawnPlayer(i);
                }
            }

            // If start is pressed on a controller, toggle ready for that player
            if (XCI.GetButtonDown(XboxButton.Start, controllers[i]))
            {
                if (players[i])
                {
                    if (playersReady[i])
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

            // If bumper is pressed on a controller, change color of that player
            if (XCI.GetButtonDown(XboxButton.RightBumper, controllers[i]))
            {
                if(players[i])
                {
                    SetPlayerColorNext(i);
                }
            }
            else if(XCI.GetButtonDown(XboxButton.LeftBumper, controllers[i]))
            {
                if (players[i])
                {
                    SetPlayerColorPrevious(i);
                }
            }
        }

        // Check for keyboard player join
        if(Input.GetButtonDown("KeyboardJoin"))
        {
            if(!players[4])
            {
                SpawnPlayer(4);
            }
        }

        if(players[4])
        {
            // If Q or E is pressed, change color of the keyboard player
            if (Input.GetButtonDown("KeyboardScrollLeft"))
            {
                SetPlayerColorNext(4);
            }
            else if (Input.GetButtonDown("KeyboardScrollRight"))
            {
                SetPlayerColorPrevious(4);
            }

            // Check for keyboard player ready
            if (Input.GetButtonDown("KeyboardReady"))
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
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i])
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

    protected void SpawnPlayer(int playerIndex)
    {
        GameObject newPlayer = (GameObject)Instantiate(playerPrefab, spawnPositions[playerIndex], Quaternion.identity);
        newPlayer.GetComponentInChildren<PlayerController>().PlayerNumber = playerIndex + 1;

        players[playerIndex] = newPlayer;

        DontDestroyOnLoad(newPlayer);

        SetPlayerColorNext(playerIndex);

        // Play audio clip for player joining
        if (playerJoinSound)
        {
            AudioSource.PlayClipAtPoint(playerJoinSound, Camera.main.transform.position, soundVolume);
        }
    }

    protected void SetPlayerColorNext(int playerIndex)
    {
        if (playerIndex < 0 || playerIndex >= players.Length)
        {
            return;
        }

        // Check that the player has joined and actually exists
        if (players[playerIndex])
        {
            PlayerController player = players[playerIndex].GetComponentInChildren<PlayerController>();
            if (player)
            {
                // Find the next available color index
                int numColors = possibleColors.Length;
                int colorIndex = colorIndices[playerIndex];

                do
                {
                    colorIndex = (colorIndex + 1) % numColors;
                } while (!ColorIsFree(colorIndex));

                // Change the player's color to the one indicated by color index
                player.SetColor(possibleColors[colorIndex]);
                colorIndices[playerIndex] = colorIndex;
            }
        }
    }

    protected void SetPlayerColorPrevious(int playerIndex)
    {
        if (playerIndex < 0 || playerIndex >= players.Length)
        {
            return;
        }

        // Check that the player has joined and actually exists
        if (players[playerIndex])
        {
            PlayerController player = players[playerIndex].GetComponentInChildren<PlayerController>();
            if (player)
            {
                // Find the next available color index
                int numColors = possibleColors.Length;
                int colorIndex = colorIndices[playerIndex];

                do
                {
                    colorIndex--;

                    if(colorIndex < 0)
                    {
                        colorIndex = numColors - 1;
                    }
                } while (!ColorIsFree(colorIndex));

                // Change the player's color to the one indicated by color index
                player.SetColor(possibleColors[colorIndex]);
                colorIndices[playerIndex] = colorIndex;
            }
        }
    }

    private bool ColorIsFree(int colorIndex)
    {
        for(int i = 0; i < colorIndices.Length; i++)
        {
            if(colorIndices[i] == colorIndex)
            {
                return false;
            }
        }
        return true;
    }
}
