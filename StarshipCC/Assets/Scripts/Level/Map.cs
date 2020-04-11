using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public char[,] cells = new char[0, 0];

    public Map()
    {
    }

    public Map(char[,] newCells)
    {
        cells = newCells;
    }

    public void GenerateRoomOnEntrance()
    {
        char[] entranceTypes = new char[] { (char)Tile.NorthEdge, (char)Tile.EastEdge, (char)Tile.SouthEdge, (char)Tile.WestEdge };
        List<int[]> entrances = this.GetEntrances(entranceTypes);
        System.Random rnd = new System.Random();

        bool roomPlaced = false;
        int attemptCount = 0, attemptMax = 40;
        while (!roomPlaced && attemptCount <= attemptMax)
        {
            attemptCount++;

            int r = rnd.Next(entrances.Count);

            int[] rEntrance = entrances[r];
            Map room = RoomManager.GetRandomCenterRoom();

            int rotateAmount = rnd.Next(0, 4);
            for (int i = 0; i < rotateAmount; i++)
            {
                room.RotateCounterClockwise();
            }

            List<int[]> roomEntrances = new List<int[]>();
            int[] offSet = new int[] { 0, 0 };

            switch (cells[rEntrance[0], rEntrance[1]])
            {
                case (char)Tile.NorthEdge:
                    roomEntrances = room.GetEntrances((char)Tile.SouthEdge);
                    offSet[0] = -1;
                    break;
                case (char)Tile.EastEdge:
                    roomEntrances = room.GetEntrances((char)Tile.WestEdge);
                    offSet[1] = 1;
                    break;
                case (char)Tile.SouthEdge:
                    roomEntrances = room.GetEntrances((char)Tile.NorthEdge);
                    offSet[0] = 1;
                    break;
                case (char)Tile.WestEdge:
                    roomEntrances = room.GetEntrances((char)Tile.EastEdge);
                    offSet[1] = -1;
                    break;
            }

            if (roomEntrances.Count > 0)
            {
                int[] rRoomEntrance = roomEntrances[rnd.Next(roomEntrances.Count)];

                int[] roomOriginPosition = new int[] { rEntrance[0] + offSet[0] - rRoomEntrance[0], rEntrance[1] + offSet[1] - rRoomEntrance[1] };

                if (PlaceMap(room, roomOriginPosition[0], roomOriginPosition[1]))
                {
                    cells[rEntrance[0], rEntrance[1]] = (char)Tile.Empty;
                    cells[rEntrance[0] + offSet[0], rEntrance[1] + offSet[1]] = (char)Tile.Empty;
                    roomPlaced = true;
                }
            }
        }
    }

    private List<int[]> GetEntrances(char type)
    {
        return GetEntrances(new char[] { type });
    }

    private List<int[]> GetEntrances(char[] types)
    {
        List<int[]> entrances = new List<int[]>();

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                foreach (char type in types)
                {
                    if (cells[x, y] == type)
                        entrances.Add(new int[] { x, y });
                }
            }
        }

        return entrances;
    }

    public bool PlaceMap(Map newMap, int targetX, int targetY)
    {
        int xMax = targetX + newMap.GetWidth();
        int yMax = targetY + newMap.GetHeight();

        if (xMax > GetWidth() || yMax > GetHeight())
            this.IncreaseDimensions(xMax, yMax);

        for (int x = 0; x < newMap.GetWidth(); x++)
        {
            for (int y = 0; y < newMap.GetHeight(); y++)
            {
                if (targetX + x < 0 || targetY + y < 0)
                    return false;

                if (cells[targetX + x, targetY + y] != new char())
                {
                    return false;
                }
            }
        }

        for (int x = 0; x < newMap.GetWidth(); x++)
        {
            for (int y = 0; y < newMap.GetHeight(); y++)
            {
                cells[targetX + x, targetY + y] = newMap.cells[x, y];
            }
        }

        return true;
    }

    private char[,] RotateCounterClockwise()
    {
        char[,] oldMatrix = cells;
        char[,] newMatrix = new char[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
        int newColumn, newRow = 0;
        for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
        {
            newColumn = 0;
            for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
            {
                switch (oldMatrix[oldRow, oldColumn])
                {
                    case (char)Tile.NorthEdge:
                        newMatrix[newRow, newColumn] = (char)Tile.EastEdge;
                        break;
                    case (char)Tile.EastEdge:
                        newMatrix[newRow, newColumn] = (char)Tile.SouthEdge;
                        break;
                    case (char)Tile.SouthEdge:
                        newMatrix[newRow, newColumn] = (char)Tile.WestEdge;
                        break;
                    case (char)Tile.WestEdge:
                        newMatrix[newRow, newColumn] = (char)Tile.NorthEdge;
                        break;
                    case (char)Tile.NorthTurret:
                        newMatrix[newRow, newColumn] = (char)Tile.EastTurret;
                        break;
                    case (char)Tile.EastTurret:
                        newMatrix[newRow, newColumn] = (char)Tile.SouthTurret;
                        break;
                    case (char)Tile.SouthTurret:
                        newMatrix[newRow, newColumn] = (char)Tile.WestTurret;
                        break;
                    case (char)Tile.WestTurret:
                        newMatrix[newRow, newColumn] = (char)Tile.NorthTurret;
                        break;
                    default:
                        newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                        break;
                }
                newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                newColumn++;
            }
            newRow++;
        }
        return newMatrix;
    }

    private void IncreaseDimensions(int newX, int newY)
    {
        if (newX < GetWidth())
            newX = GetWidth();
        if (newY < GetHeight())
            newY = GetHeight();   

        char[,] newCells = new char[newX, newY];
        
        for (int x = 0; x < GetWidth(); x++)
        {
            for (int y = 0; y < GetHeight(); y++)
            {
                newCells[x, y] = cells[x, y];
            }
        }

        cells = newCells;
    }

    public int GetWidth()
    {
        return cells.GetLength(0);
    }

    public int GetHeight()
    {
        return cells.GetLength(1);
    }

    public Map GetSubset(int xStart, int xEnd, int yStart, int yEnd)
    {
        int subsetWidth = xEnd - xStart + 1;
        int subsetHeight = yEnd - yStart + 1;
        char[,] subsetArray = new char[subsetWidth, subsetHeight];
        for (int i = 0; i < subsetWidth; i++)
        {
            Array.Copy(cells, (i + xStart) * GetHeight() + yStart, subsetArray, i * subsetHeight, subsetHeight);
        }

        return new Map(subsetArray);
    }
}
