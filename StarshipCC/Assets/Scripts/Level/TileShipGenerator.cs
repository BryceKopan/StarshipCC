using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileShipGenerator : LevelGenerator
{
    public int chunkWidth = 10;
    public int chunkHeight = 10;

    public GameObject wallPrefab;
    public GameObject turretPrefab;
    public GameObject chestPrefab;
    public GameObject backgroundPrefab;
    public GameObject shopPrefab;

    [SerializeField] public List<GameObject> SmallAttachmentPrefabs;

    private int numEdgeRooms = 2;

    Map level;

    CapitalShipChunk[,] chunks;
    private GameObject chunksObj;

    public Vector3 tileSize;

    void Start()
    {

    }

    public override void GenerateLevel()
    {
        Debug.Log("GenerateLevel");
        chunksObj = new GameObject("Chunks");
        chunksObj.transform.SetParent(transform);

        tileSize = wallPrefab.GetComponent<SpriteRenderer>().bounds.size;

        RoomManager.LoadMaps();

        level = new Map();

        // Add entrances to the ship
        int entranceRoomY = 0;
        for (int i = 0; i < numEdgeRooms; i++)
        {
            Map room = RoomManager.GetRandomEdgeRoom();
            level.PlaceMap(room, 0, entranceRoomY);
            entranceRoomY += room.GetHeight();
        }

        for (int i = 0; i < 30; i++)
        {
            level.GenerateRoomOnEntrance();
        }

        // Separate level into chunks
        int numChunksX = (int)Mathf.Ceil(1.0f * (level.GetWidth() / chunkWidth)); 
        int numChunksY = (int)Mathf.Ceil(1.0f * (level.GetHeight() / chunkHeight));

        chunks = new CapitalShipChunk[numChunksX, numChunksY];

        for (int x = 0; x < numChunksX; x++)
        {
            for (int y = 0; y < numChunksY; y++)
            {
                int chunkXEnd = chunkWidth * (x + 1) - 1;
                int chunkYEnd = chunkHeight * (y + 1) - 1;

                // Need this in case the final chunks are not full-sized
                if(x == numChunksX - 1)
                {
                    chunkXEnd = level.GetWidth() - 1;
                }

                if (y == numChunksY - 1)
                {
                    chunkYEnd = level.GetHeight() - 1;
                }

                Map chunkMap = level.GetSubset(chunkWidth * x, chunkXEnd, chunkHeight * y, chunkYEnd);

                GameObject chunkObj = new GameObject("Chunk " + x + "," + y);
                chunkObj.transform.SetParent(chunksObj.transform);

                CapitalShipChunk chunk = chunkObj.AddComponent<CapitalShipChunk>();
                chunk.transform.position = new Vector3((tileSize.x * chunkWidth) * x + transform.position.x
                                                    , (tileSize.y * chunkHeight) * y + transform.position.y
                                                    , 0);
                chunk.init(chunkMap, this);
                chunks[x, y] = chunk;
            }
        }
        BuildBackground(level);

        Debug.Log("Starting level");
        LevelController levelController = GameObject.FindObjectOfType<LevelController>();
        if (levelController)
        {
            levelController.DoneBuildingLevel();
        }
    }

    public void BuildBackground(Map map)
    {
        //Instatiate and size background
        GameObject background = Instantiate(backgroundPrefab, new Vector3(map.GetWidth() * tileSize.x / 2 + transform.position.x, map.GetHeight() * tileSize.y / 2 + transform.position.y, 10), Quaternion.identity);
        background.transform.localScale = new Vector3(background.transform.localScale.x * map.GetWidth(), background.transform.localScale.x * map.GetHeight(), 1);
    }

    private void PrintMap(Map map)
    {
        string drawnMap = "";

        for (int y = map.GetHeight() - 1; y >= 0; y--)
        {
            for (int x = 0; x < map.GetWidth(); x++)
            {
                if (map.cells[x, y] == new char())
                    drawnMap += '_';
                else
                    drawnMap += map.cells[x, y];
            }
            drawnMap += '\n';
        }

        Debug.Log(drawnMap);
    }

    public GameObject getRandomSmallAttachment()
    {
        int r = Random.Range(0, SmallAttachmentPrefabs.Count);
        return SmallAttachmentPrefabs[r];
    }
}
