using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public char[,] cells = new char[0,0];

    public Map()
    {
    }

    public Map(char[,] newCells)
    {
        cells = newCells;
    }

    public void GenerateRoomOnEntrance()
    {
        List<int[]> entrances = this.GetEntrances(new char[]{'n', 'e', 'w', 's'});
        System.Random rnd = new System.Random();

        bool roomPlaced = false;
        int attemptCount = 0, attemptMax = 40;
        while(!roomPlaced && attemptCount <= attemptMax)
        {
            attemptCount ++;

            int r = rnd.Next(entrances.Count);

            int[] rEntrance = entrances[r];
            Map room = RoomManager.GetRandomCenterRoom();
            room.Rotate90Clockwise(rnd.Next(0,4));

            List<int[]> roomEntrances = new List<int[]>();
            int[] offSet = new int[]{0, 0};

            switch(cells[rEntrance[0], rEntrance[1]])
            {
                case 'n':
                    roomEntrances = room.GetEntrances(new char[]{'s'});
                    offSet[0] = -1;
                    break;
                case 'e':
                    roomEntrances = room.GetEntrances(new char[]{'w'});
                    offSet[1] = 1;
                    break;
                case 's':
                    roomEntrances = room.GetEntrances(new char[]{'n'});
                    offSet[0] = 1;
                    break;
                case 'w':
                    roomEntrances = room.GetEntrances(new char[]{'e'});
                    offSet[1] = -1;
                    break;
            }

            if(roomEntrances.Count > 0)
            {
                int[] rRoomEntrance = roomEntrances[rnd.Next(roomEntrances.Count)];

                int[] roomOriginPosition = new int[]{rEntrance[0] + offSet[0] - rRoomEntrance[0], rEntrance[1] + offSet[1] - rRoomEntrance[1]};

                if(PlaceMap(room, roomOriginPosition[0], roomOriginPosition[1]))
                {
                    cells[rEntrance[0], rEntrance[1]] = ' ';
                    cells[rEntrance[0] + offSet[0], rEntrance[1] + offSet[1]] = ' ';
                    roomPlaced = true;
                }
            }
        }
    }

    private List<int[]> GetEntrances(char[] directions)
    {
        List<int[]> entrances = new List<int[]>();

        for(int x=0; x<cells.GetLength(0); x++)
        {
            for(int y=0; y<cells.GetLength(1); y++)
            {
                foreach(char direction in directions)
                {
                    if(cells[x, y] == direction)
                        entrances.Add(new int[]{x, y});
                }
            }
        }

        return entrances;
    }

    public bool PlaceMap(Map newMap, int targetX, int targetY)
    {
        int xMax = targetX + newMap.cells.GetLength(0);
        int yMax = targetY + newMap.cells.GetLength(1);

        if(xMax > cells.GetLength(0) || yMax > cells.GetLength(1))
            this.IncreaseDimensions(xMax, yMax);

        for(int x=0; x<newMap.cells.GetLength(0); x++)
        {
            for(int y=0; y<newMap.cells.GetLength(1); y++)
            {
                if(targetX + x < 0 || targetY + y < 0)
                    return false;

                if(cells[targetX + x, targetY + y] != new char())
                {
                    return false;
                }
            }
        }

        for(int x=0; x<newMap.cells.GetLength(0); x++)
        {
            for(int y=0; y<newMap.cells.GetLength(1); y++)
            {
                cells[targetX + x, targetY + y] = newMap.cells[x,y];
            }
        }

        return true;
    }

    private void Rotate90Clockwise(int numberOfRotations)
    {
        for(int i=0; i<numberOfRotations; i++)
        {
            char[,] newMap = new char[cells.GetLength(1), cells.GetLength(0)];

            for(int x=0; x<cells.GetLength(0); x++)
            {
                for(int y=0; y<cells.GetLength(1); y++)
                {
                    switch(cells[x, y])
                    {
                        case 'n':
                            newMap[y, x] = 'e';
                            break;
                        case 'e':
                            newMap[y, x] = 's';
                            break;
                        case 's':
                            newMap[y, x] = 'w';
                            break;
                        case 'w':
                            newMap[y, x] = 'n';
                            break;
                        default:
                            newMap[y, x] = cells[x, y];
                            break;
                    }
                }
            }

            cells = newMap;
        }
    }

    private void IncreaseDimensions(int newX, int newY)
    {
        if(newX < cells.GetLength(0))
            newX = cells.GetLength(0);
        if(newY < cells.GetLength(1))
            newY = cells.GetLength(1);
        
        char[,] newCells = new char[newX,newY]; 

        for(int x=0; x<cells.GetLength(0); x++)
        {
            for(int y=0; y<cells.GetLength(1); y++)
            {
                newCells[x, y] = cells[x, y];
            }
        }

        cells = newCells;
    } 
}
