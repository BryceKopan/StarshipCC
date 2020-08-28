using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalShipChunk : MonoBehaviour
{
    public float chunkLoadingDistance = 300f;

    public float distanceToCamera;

    public int width;
    public int height;

    public Map map;

    public GameObject tiles;

    public TileShipGenerator shipGenerator;

    public bool initialized = false;
    public bool notGenerated = true;

    Camera mainCamera;

    Vector3 tileSize;
    Vector3 centerPos;

    public void init(Map map, TileShipGenerator shipGenerator)
    {
        this.map = map;
        this.width = map.GetWidth();
        this.height = map.GetHeight();
        this.shipGenerator = shipGenerator;

        mainCamera = Camera.main;
        centerPos = GetCenterPos();
        tileSize = shipGenerator.wallPrefab.GetComponent<SpriteRenderer>().bounds.size;
        initialized = true;
    }

    public void GenerateChunk()
    {
        notGenerated = false;
        StartCoroutine(GenerateChunkAsync());
    }

    IEnumerator GenerateChunkAsync()
    {
        tiles = new GameObject("Tiles");
        if(tiles == null)
        {
            Debug.Log("tiles is null");
        }
        if (transform == null)
        {
            Debug.Log("transform is null");
        }
        tiles.transform.SetParent(transform);

        for (int y = 0; y < map.GetHeight(); y++)
        {
            for (int x = 0; x < map.GetWidth(); x++)
            {
                Vector3 position = new Vector3((x * tileSize.x) + transform.position.x, y * tileSize.y + transform.position.y, 1);

                switch (map.cells[x, y])
                {
                    case (char)Tile.Shop:
                        Instantiate(shipGenerator.shopPrefab, position, Quaternion.Euler(0, 0, 0), tiles.transform);
                        break;
                    case (char)Tile.Treasure:
                        Instantiate(shipGenerator.chestPrefab, position, Quaternion.Euler(0, 0, 0), tiles.transform);
                        break;
                    case (char)Tile.NorthTurret:
                        Instantiate(shipGenerator.getRandomSmallAttachment(), position, Quaternion.Euler(0, 0, 90), tiles.transform);
                        break;
                    case (char)Tile.EastTurret:
                        Instantiate(shipGenerator.getRandomSmallAttachment(), position, Quaternion.Euler(0, 0, 0), tiles.transform);
                        break;
                    case (char)Tile.SouthTurret:
                        Instantiate(shipGenerator.getRandomSmallAttachment(), position, Quaternion.Euler(0, 0, -90), tiles.transform);
                        break;
                    case (char)Tile.WestTurret:
                        Instantiate(shipGenerator.getRandomSmallAttachment(), position, Quaternion.Euler(0, 0, 180), tiles.transform);
                        break;
                    case (char)Tile.Wall:
                    case new char():
                        Instantiate(shipGenerator.wallPrefab, position, Quaternion.identity, tiles.transform);
                        break;
                    case (char)Tile.NoBackground:
                        break;
                }
                yield return null;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(initialized)
        {
            if (notGenerated)
            {
                distanceToCamera = Vector2.Distance(mainCamera.gameObject.transform.position,
                                                            centerPos);
                if (distanceToCamera <= chunkLoadingDistance)
                {
                    GenerateChunk();
                }
            }
        }
    }

    Vector3 GetCenterPos()
    {
        Vector3 center = new Vector3();
        center.x = transform.position.x + (tileSize.x * width / 2);
        center.y = transform.position.y + (tileSize.y * height / 2);
        center.z = transform.position.z;

        return center;
    }
}
