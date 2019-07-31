using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileShipGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject turretPrefab;
    public GameObject chestPrefab;
    public GameObject backgroundPrefab;
    public GameObject shopPrefab;

    void Start()
    {
        RoomManager.LoadMaps();

        Map level = new Map();

        int leadingEdgeX = 0;
        for(int i=0; i<8; i++)
        {
            Map room = RoomManager.GetRandomEdgeRoom();
            level.PlaceMap(room, 0, leadingEdgeX);
            leadingEdgeX += room.cells.GetLength(1);
        }

        for(int i=0; i<30; i++)
        {
            level.GenerateRoomOnEntrance();
        }

        BuildMap(level);
    }

    void BuildMap(Map map)
    {
        Vector3 wallSize = wallPrefab.GetComponent<SpriteRenderer>().bounds.size;

        //Instatiate and size background
        GameObject background = Instantiate(backgroundPrefab, new Vector3(map.cells.GetLength(1)/2 * wallSize.x + transform.position.x, map.cells.GetLength(0)/2 * wallSize.y + transform.position.y, 10), Quaternion.identity);
        background.transform.localScale = new Vector3(background.transform.localScale.x * map.cells.GetLength(1), background.transform.localScale.x * map.cells.GetLength(0), 1);

        //Construct Map
        for(int x=0; x<map.cells.GetLength(0); x++)
        {
            for(int y=0; y<map.cells.GetLength(1); y++)
            {
                Vector3 position = new Vector3((y * wallSize.x) + transform.position.x, x * wallSize.y + transform.position.y, 1);

                switch(map.cells[x, y])
                {
                    case (char)Tile.Shop:
                        Instantiate(shopPrefab, position, Quaternion.Euler(0, 0, 0));
                        break;
                    case (char)Tile.Treasure:
                        Instantiate(chestPrefab, position, Quaternion.Euler(0, 0, 0));
                        break;
                    case (char)Tile.NorthTurret:
                        Instantiate(turretPrefab, position, Quaternion.Euler(0, 0, 0));
                        break;
                    case (char)Tile.EastTurret:
                        Instantiate(turretPrefab, position, Quaternion.Euler(0, 0, 90));
                        break;
                    case (char)Tile.SouthTurret:
                        Instantiate(turretPrefab, position, Quaternion.Euler(0, 0, 180));
                        break;
                    case (char)Tile.WestTurret:
                        Instantiate(turretPrefab, position, Quaternion.Euler(0, 0, -90));
                        break;
                    case (char)Tile.Wall:
                    case new char():
                        Instantiate(wallPrefab, position, Quaternion.identity);   
                        break;
                    case (char)Tile.NoBackground:
                        break;
                }   
            }
        }
    }

    private 

    void PrintMap(Map map)
    {
        string drawnMap = "";
        
        for(int x=0; x<map.cells.GetLength(0); x++)
        {
            for(int y=0; y<map.cells.GetLength(1); y++)
            {
                if(map.cells[x, y] == new char())
                    drawnMap += '_';
                else
                    drawnMap += map.cells[x, y];         
            }
            drawnMap += '\n';
        }

        Debug.Log(drawnMap);
    }
}
