using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Must be set in inspector
    public Transform LoadingArea;
    public Transform PlayerSpawn;
    public GameObject loadingUI;

    public int playerSpawnOffset;

    // Will be found dynamically 
    public List<PlayerController> players;

    public CameraController cameraController;

    public bool levelStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        players = new List<PlayerController>(GameObject.FindObjectsOfType<PlayerController>());
        cameraController = GameObject.FindObjectOfType<CameraController>();

        StartLoadingLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLoadingLevel()
    {
        // Move players into the loading zone
        for(int i = 0; i < players.Count; i++)
        {
            GameObject player = players[i].gameObject;
            player.transform.position = new Vector3(LoadingArea.position.x, 
                LoadingArea.position.y + i * playerSpawnOffset, 
                player.transform.position.z);
        }

        cameraController.transform.position = new Vector3(LoadingArea.position.x, 
            LoadingArea.position.y, 
            cameraController.transform.position.z);
        cameraController.freezeCamera = true;

        loadingUI.transform.position = LoadingArea.position;

        LevelGenerator levelGenerator = GameObject.FindObjectOfType<LevelGenerator>();
        levelGenerator.GenerateLevel();

        if(players.Count == 0)
        {
            StartLevel();
        }
    }

    public void StartLevel()
    {
        Debug.Log("Starting level");

        levelStarted = true;

        // Move players to the level start
        for (int i = 0; i < players.Count; i++)
        {
            GameObject player = players[i].gameObject;
            player.transform.position = new Vector3(PlayerSpawn.position.x,
                PlayerSpawn.position.y + i * playerSpawnOffset,
                player.transform.position.z);
        }

        cameraController.transform.position = new Vector3(PlayerSpawn.position.x,
            PlayerSpawn.position.y,
            cameraController.transform.position.z);
        cameraController.freezeCamera = true;

        LoadingArea.gameObject.SetActive(false);
    }

    public void DoneBuildingLevel()
    {
        StartLevel();
    }
}
