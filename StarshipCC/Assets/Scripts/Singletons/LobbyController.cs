using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class LobbyController : MonoBehaviour {

    public GameObject missionStartZone;

    public string levelSceneName;

    public SceneChangeController sceneChanger;

    GameObject[] players = new GameObject[5];
    public Transform[] spawnPositions = new Transform[5];

    int[] colorIndices = new int[] {0, 0, 0, 0, 0};
    [SerializeField] Color[] possibleColors;

    int[] classIndices = new int[] { 0, 0, 0, 0, 0 };
    [SerializeField] PlayerClass[] possibleClasses;

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

        sceneChanger = GameObject.FindObjectOfType<SceneChangeController>();
        if(!sceneChanger)
        {
            Debug.LogError("No scene change manager found in lobby");
        }
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

            // If dpad up/down is pressed on a controller, change color of that player
            if (XCI.GetButtonDown(XboxButton.DPadUp, controllers[i]))
            {
                if(players[i])
                {
                    SetPlayerColorNext(i);
                }
            }
            else if(XCI.GetButtonDown(XboxButton.DPadDown, controllers[i]))
            {
                if (players[i])
                {
                    SetPlayerColorPrevious(i);
                }
            }

            // If dpad up/down is pressed on a controller, change class of that player
            if (XCI.GetButtonDown(XboxButton.DPadRight, controllers[i]))
            {
                if (players[i])
                {
                    SetPlayerClassNext(i);
                }
            }
            else if (XCI.GetButtonDown(XboxButton.DPadLeft, controllers[i]))
            {
                if (players[i])
                {
                    SetPlayerClassPrevious(i);
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
            // If Q or E is pressed, change class of the keyboard player
            if (Input.GetButtonDown("KeyboardScrollLeft"))
            {
                SetPlayerClassNext(4);
            }
            else if (Input.GetButtonDown("KeyboardScrollRight"))
            {
                SetPlayerClassPrevious(4);
            }

            // If R or F is pressed, change color of the keyboard player
            if (Input.GetButtonDown("KeyboardScrollUp"))
            {
                SetPlayerColorNext(4);
            }
            else if (Input.GetButtonDown("KeyboardScrollDown"))
            {
                SetPlayerColorPrevious(4);
            }
        }

        // If everyone is ready, start game
        if (CanStartGame())
        {
            sceneChanger.ChangeSceneTo(levelSceneName);
        }
    }

    // Returns true if there is at least one player and all players are ready
    protected bool CanStartGame()
    {
        if(!missionStartZone)
        {
            return false;
        }

        Collider2D missionStartCollider = missionStartZone.GetComponent<Collider2D>(); 

        bool canStartGame = true;
        bool atLeastOnePlayer = false;
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i])
            {
                atLeastOnePlayer = true;

                Collider2D shipCollider = players[i].GetComponentInChildren<Collider2D>();

                if (!shipCollider.IsTouching(missionStartCollider))
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
        Transform spawnPos;
        if(spawnPositions[playerIndex])
        {
            spawnPos = spawnPositions[playerIndex];
        }
        else
        {
            spawnPos = new GameObject().transform;
        }

        GameObject newPlayer = (GameObject)Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation);
        PlayerController playerController = newPlayer.GetComponentInChildren<PlayerController>();
        playerController.PlayerNumber = playerIndex + 1;
        playerController.SetPlayerClass(possibleClasses[0]);

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

    protected void SetPlayerClassNext(int playerIndex)
    {
        // Make sure the player is valid and has joined
        if (playerIndex < 0 || playerIndex >= players.Length)
        {
            return;
        }

        if (players[playerIndex])
        {
            PlayerController player = players[playerIndex].GetComponentInChildren<PlayerController>();
            if (player)
            {
                // Change the player class
                int numClasses = possibleClasses.Length;
                int classIndex = classIndices[playerIndex];

                classIndex = (classIndex + 1) % numClasses;

                // Change the player's class to the one indicated by class index
                player.SetPlayerClass(possibleClasses[classIndex]);
                classIndices[playerIndex] = classIndex;
            }
        }
    }

    protected void SetPlayerClassPrevious(int playerIndex)
    {
        // Make sure the player is valid and has joined
        if (playerIndex < 0 || playerIndex >= players.Length)
        {
            return;
        }

        if (players[playerIndex])
        {
            PlayerController player = players[playerIndex].GetComponentInChildren<PlayerController>();
            if (player)
            {
                // Change the player class
                int numClasses = possibleClasses.Length;
                int classIndex = classIndices[playerIndex];

                classIndex--;
                if(classIndex < 0)
                {
                    classIndex = numClasses - 1;
                }

                // Change the player's class to the one indicated by class index
                player.SetPlayerClass(possibleClasses[classIndex]);
                classIndices[playerIndex] = classIndex;
            }
        }
    }
}
