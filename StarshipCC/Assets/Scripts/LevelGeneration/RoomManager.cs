using System.Collections.Generic;


public static class RoomManager
{
    static System.Random rnd = new System.Random();
    static List<char[,]> edgeRooms = new List<char[,]>();
    static List<char[,]> centerRooms = new List<char[,]>();
    
    static char[,] edgeRoom0 = new char[,]{
    {'#','#','#','#','#','#',' ',' ',' ','#','#','#','#','#','#'},
    {'#','#','#','#','#','#',' ',' ',' ','#','#','#','#','#','#'},
    {'#','#','#','#','#','#',' ','s',' ','#','#','#','#','#','#'}};

    static char[,] edgeRoom1 = new char[,]{
    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},};

    static char[,] centerRoom1 = new char[,]{
    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
    {'#',' ','3',' ','#','#','#','#','#','#','#','#','#','#','#','#','#'},
    {'#',' ',' ',' ','#','#','#','#','#','#','#','#','#','#','#','#','#'},
    {'#',' ',' ',' ','#','#','#','#','#','#','#','#','#','#','#','#','#'},
    {'#',' ',' ',' ',' ',' ',' ',' ','#',' ',' ','3',' ',' ',' ','#','#'},
    {' ','#',' ',' ',' ',' ',' ',' ','#','#',' ',' ',' ',' ',' ',' ','#'},
    {'w',' ',' ',' ','#',' ',' ',' ',' ','#',' ',' ','#',' ',' ',' ','e'},
    {' ','#',' ',' ','#','#',' ',' ',' ',' ',' ',' ','#','#',' ',' ','#'},
    {'#',' ',' ',' ',' ','#',' ','1',' ',' ',' ',' ',' ','#',' ','#','#'},
    {'#',' ',' ',' ','#','#','#','#','#','#','#','#','#','#','#','#','#'},
    {'#',' ',' ',' ','#','#','#','#','#','#','#','#','#','#','#','#','#'},
    {'#',' ','1',' ','#','#','#','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}};

    static char[,] centerRoom2 = new char[,]{
    {'#','#','#','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#','#',' ','#','#','#','#','#','#'},
    {'#','#','#','#','#',' ',' ',' ','#','#','#','#','#'},
    {'#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#'},
    {'#','#','#',' ',' ',' ',' ',' ',' ',' ','#','#','#'},
    {'#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#'},
    {'#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ','#'},
    {'#','2',' ',' ',' ','4','#','2',' ',' ',' ','4','#'},
    {'#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ','#'},
    {'#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ','#'},
    {'#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ','#'},
    {'#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ','#'},
    {'#','2',' ',' ',' ','4','#',' ',' ',' ',' ',' ','#'},
    {'#',' ',' ',' ',' ',' ','#','#',' ',' ',' ',' ',' '},
    {'#','#',' ',' ',' ',' ',' ','#','#',' ',' ',' ',' '},
    {' ','#','#',' ',' ',' ',' ',' ','#','#',' ',' ','e'},
    {' ',' ','#','#',' ',' ',' ',' ',' ','#','#',' ',' '},
    {'w',' ',' ','#','#',' ',' ',' ',' ',' ','#','#',' '},
    {' ',' ',' ',' ','#','#',' ',' ',' ',' ',' ','#','#'},
    {' ',' ',' ',' ',' ','#','#',' ',' ',' ',' ',' ','#'},
    {'#',' ',' ',' ',' ',' ','#','2',' ',' ',' ','4','#'},
    {'#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ','#'},
    {'#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ','#'},
    {'#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ','#'},
    {'#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ','#'},
    {'#','2',' ',' ',' ','4','#','2',' ',' ',' ','4','#'},
    {'#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ','#'},
    {'#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#'},
    {'#','#','#',' ',' ',' ',' ',' ',' ',' ','#','#','#'},
    {'#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#'},
    {'#','#','#','#','#',' ',' ',' ','#','#','#','#','#'},
    {'#','#','#','#','#','#',' ','#','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#','#','#','#'}};

    static char[,] centerRoom3 = new char[,]{
    {' ',' ',' ',' ',' ','n',' ',' ',' ',' ',' ',' ','n',' ',' ',' ',' ',' ',' ','n',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ',' ',' ','1',' ',' ',' ',' ',' ',' ',' ',' ',' ','1',' ',' ',' ',' ',' ',' ',' '},
    {'w',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ','e'},
    {' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ','4','#','#','#','#','#','2',' ',' ',' ','4','#','#','#','#','#','2',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ',' ',' ','3',' ',' ',' ',' ',' ',' ',' ',' ',' ','3',' ',' ',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
    {'w',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','e'},
    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ',' ',' ','1',' ',' ',' ',' ',' ',' ',' ',' ',' ','1',' ',' ',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ','4','#','#','#','#','#','2',' ',' ',' ','4','#','#','#','#','#','2',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' '},
    {'w',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ','e'},
    {' ',' ',' ',' ',' ',' ',' ','3',' ',' ',' ',' ',' ',' ',' ',' ',' ','3',' ',' ',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ','s',' ',' ',' ',' ',' ',' ','s',' ',' ',' ',' ',' ',' ','s',' ',' ',' ',' ',' '}};  

    static char[,] centerRoom4 = new char[,]{
    {'#','#','#','#','#','#','#','#','#','#',' ',' ','n',' ',' ','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#'},
    {'#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#'},
    {'#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#'},
    {'#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#'},
    {'#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ','1',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#'},
    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
    {'w',' ',' ',' ',' ',' ',' ',' ',' ','4','#','#','#','#','#','2',' ',' ',' ',' ',' ',' ',' ',' ','e'},
    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
    {'#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ','3',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#'},
    {'#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#'},
    {'#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#'},
    {'#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#'},
    {'#','#','#','#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#',' ',' ',' ',' ',' ','#','#','#','#','#','#','#','#','#','#'},
    {'#','#','#','#','#','#','#','#','#','#',' ',' ','s',' ',' ','#','#','#','#','#','#','#','#','#','#'}};

    public static void LoadMaps()
    {
        edgeRooms.Add(edgeRoom0);
        edgeRooms.Add(edgeRoom1);

        centerRooms.Add(centerRoom0);
        centerRooms.Add(centerRoom1);
        centerRooms.Add(centerRoom2);
        centerRooms.Add(centerRoom3);
        centerRooms.Add(centerRoom4);
    }

    public static Map GetRandomEdgeRoom()
    {
        return new Map(edgeRooms[rnd.Next(0, edgeRooms.Count)]);
    }

    public static Map GetRandomCenterRoom()
    {
         return new Map(centerRooms[rnd.Next(0, centerRooms.Count)]);
    }
}