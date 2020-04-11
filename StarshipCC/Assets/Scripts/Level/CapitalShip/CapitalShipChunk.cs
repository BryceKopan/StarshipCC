using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalShipChunk : MonoBehaviour
{
    public int width;
    public int height;

    public Map map;

    public GameObject tiles;

    public TileShipGenerator shipGenerator;

    public void init(Map map, TileShipGenerator shipGenerator)
    {
        this.map = map;
        this.width = map.GetWidth();
        this.height = map.GetHeight();
        this.shipGenerator = shipGenerator;
    }

    public void GenerateChunk()
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

        Vector3 wallSize = shipGenerator.wallPrefab.GetComponent<SpriteRenderer>().bounds.size;

        for (int y = 0; y < map.GetHeight(); y++)
        {
            for (int x = 0; x < map.GetWidth(); x++)
            {
                Vector3 position = new Vector3((x * wallSize.x) + transform.position.x, y * wallSize.y + transform.position.y, 1);

                switch (map.cells[x, y])
                {
                    case (char)Tile.Shop:
                        Instantiate(shipGenerator.shopPrefab, position, Quaternion.Euler(0, 0, 0), tiles.transform);
                        break;
                    case (char)Tile.Treasure:
                        Instantiate(shipGenerator.chestPrefab, position, Quaternion.Euler(0, 0, 0), tiles.transform);
                        break;
                    case (char)Tile.NorthTurret:
                        Instantiate(shipGenerator.turretPrefab, position, Quaternion.Euler(0, 0, 0), tiles.transform);
                        break;
                    case (char)Tile.EastTurret:
                        Instantiate(shipGenerator.turretPrefab, position, Quaternion.Euler(0, 0, 90), tiles.transform);
                        break;
                    case (char)Tile.SouthTurret:
                        Instantiate(shipGenerator.turretPrefab, position, Quaternion.Euler(0, 0, 180), tiles.transform);
                        break;
                    case (char)Tile.WestTurret:
                        Instantiate(shipGenerator.turretPrefab, position, Quaternion.Euler(0, 0, -90), tiles.transform);
                        break;
                    case (char)Tile.Wall:
                    case new char():
                        Instantiate(shipGenerator.wallPrefab, position, Quaternion.identity, tiles.transform);
                        break;
                    case (char)Tile.NoBackground:
                        break;
                }
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
        
    }
}
