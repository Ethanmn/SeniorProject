using UnityEngine;
using System;

public class Room
{

    // The position of the room, from the bottom left corner
    private PointF position;

    // Height of the room in tiles (y size)
    private int tileWidth;
    // Width of the room in tiles (x size)
    private int tileHeight;

    // Array representation of a rooms tiles
    private int[,] tileMap;
    // Array representation of mobs in a room
    private int[,] mobMap;
    // Array representation of items in a room
    private int[,] itemMap;

    // Floor tile to use
    public GameObject floorTile;
    // Wall tile to use
    public GameObject wallTile;

    /// <summary>
    /// Create a room of the specified hight and width in tiles
    /// Position is the bottom left corner of the room and in world space
    /// Rooms are built up and right
    /// </summary>
    /// <param name="_position"></param>
    /// <param name="_width"></param>
    /// <param name="_height"></param>
    public Room(PointF _position, int _width, int _height)
    {
        // World space position
        position = _position;

        // Number of tiles
        tileHeight = _height;
        tileWidth = _width;

        // 2D Array maps
        tileMap = new int[tileHeight, tileWidth];
        mobMap = new int[tileHeight, tileWidth];

        // Create the room given the constraints
        CreateRoomTiles();
        // Creat the mobs in the room
        CreateRoomMobs();
    }

    /// <summary>
    /// Creates the room on the map (Obsolete after Floor is implemented)
    /// </summary>
    public void CreateRoom()
    {
        for (int row = 0; row < tileHeight; row++)
        {
            for (int column = 0; column < tileWidth; column++)
            {
                // Calculate the tile position
                Vector3 tilePosition = new Vector3(column * DungeonTileK.TILE_SIZE + position.X, row * DungeonTileK.TILE_SIZE + position.Y, 0.1f);

                // If the grid point is on the edge of the room
                if (tileMap[row, column] == DungeonTileK.WALL_TILE)
                {
                    // Create wall tile at tilePosition
                    GameObject wall = GameObject.Instantiate(wallTile) as GameObject;
                    wall.GetComponent<Transform>().position = tilePosition;
                }
                else if (tileMap[row, column] == DungeonTileK.FLOOR_TILE)
                {
                    // Create floor tile at tilePosition
                    GameObject floor = GameObject.Instantiate(floorTile) as GameObject;
                    floor.GetComponent<Transform>().position = tilePosition;
                }
            }
        }
    }
    
    /// <summary>
    /// Prints the room as ascii values to the console
    /// </summary>
    public void PrintRoom()
    {
        // String to print the map
        String map = "";
        // Add the name
        map += "tileMap" + Environment.NewLine;
        
        // For each point in the 2D array
        for (int row = 0; row < tileMap.GetLength(0); row++)
        {
            for (int column = 0; column < tileMap.GetLength(1); column++)
            {
                // Add that value to the string with two spaces
                map += tileMap[row, column] + "  ";
            }
            
           map += Environment.NewLine;
        }
        Debug.Log(map);
    }

    private void CreateRoomTiles()
    {
        // For each point in the room
        for (int row = 0; row < tileMap.GetLength(0); row++)
        {
            for (int column = 0; column < tileMap.GetLength(1); column++)
            {
                // If the grid point is on the edge of the room
                if (row == 0 ||
                    row == tileHeight - 1 ||
                    column == 0 ||
                    column == tileWidth - 1)
                {
                    // Create wall tile at tilePosition
                    tileMap[row, column] = DungeonTileK.WALL_TILE;
                }
                else
                {
                    // Create floor tile at tilePosition
                    tileMap[row, column] = DungeonTileK.FLOOR_TILE;
                }
            }
        }
    }

    private void CreateRoomMobs()
    {
        for (int row = 0; row < tileHeight; row++)
        {
            for (int column = 0; column < tileWidth; column++)
            {
                // If the grid point is NOT on the edge of the room
                if (!(row == 0 ||
                    row == tileHeight - 1 ||
                    column == 0 ||
                    column == tileWidth - 1))
                {
                    int chance = (int)UnityEngine.Random.Range(0f, 200f);
                    if (chance == 2)
                        mobMap[row, column] = 1;
                }
            }
        }

        for (int row = 0; row < tileHeight; row++)
        {
            for (int column = 0; column < tileWidth; column++)
            {
                // Calculate the tile position
                Vector3 mobPosition = new Vector3(column * DungeonTileK.TILE_SIZE + position.X, row * DungeonTileK.TILE_SIZE + position.Y, 0.1f);

                // If the grid point is on the edge of the room
                if (mobMap[row, column] == 1)
                {
                    // Create wall tile at tilePosition
                    GameObject mob = GameObject.Instantiate(Resources.Load("Prefabs/Blob")) as GameObject;
                    mob.GetComponent<Transform>().position = mobPosition;
                }
            }
        }
    }

}
