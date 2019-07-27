using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileShipGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject backgroundPrefab;

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

                if(map.cells[x, y] == new char())
                {
                    Instantiate(wallPrefab, position, Quaternion.identity);   
                }
                else if(map.cells[x, y] == '#')
                {
                    Instantiate(wallPrefab, position, Quaternion.identity);   
                }    
                else if(map.cells[x, y] == ' ')
                {
                    Instantiate(backgroundPrefab, position, Quaternion.identity);   
                }   
            }
        }
    }

    void PrintMap(Map map)
    {
        string drawnMap = "";
        
        for(int x=0; x<map.cells.GetLength(0); x++)
        {
            for(int y=0; y<map.cells.GetLength(1); y++)
            {
                if(map.cells[x, y] == new char())
                    drawnMap += ' ';
                else
                    drawnMap += map.cells[x, y];         
            }
            drawnMap += '\n';
        }

        Debug.Log(drawnMap);
    }
}
