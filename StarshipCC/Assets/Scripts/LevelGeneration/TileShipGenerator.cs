using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileShipGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject turretPrefab;

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

        for(int i=0; i<60; i++)
        {
            level.GenerateRoomOnEntrance();
        }

        PrintMap(level);
        BuildMap(level);
    }

    void BuildMap(Map map)
    {
        SpriteRenderer wallPrefabSR = wallPrefab.GetComponent<SpriteRenderer>();

        for(int x=0; x<map.cells.GetLength(0); x++)
        {
            for(int y=0; y<map.cells.GetLength(1); y++)
            {
                Vector3 position = new Vector3((y * wallPrefabSR.bounds.size.x) + transform.position.x, x * wallPrefabSR.bounds.size.y + transform.position.y, 1);

                switch(map.cells[x, y])
                {
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
