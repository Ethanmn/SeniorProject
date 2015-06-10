using UnityEngine;
using System.Collections;
using System;

public class Room {

    // The position of the room, from the center
    private Vector2 position;

    // Height of the room in tiles (y size)
    private int tileWidth;
    // Width of the room in tiles (x size)
    private int tileHeight;

    // Height in world space
    private float height;
    // Width in world space
    private float width;

    // Array representation of a rooms tiles
    private int[,] tileMap;
    // Array representation of mobs in a room
    private int[,] mobMap;
    // Array representation of items in a room
    private int[,] itemMap;

    // Size of square tile sprites
    private float tileSize;

    // Floor tile to use
    public GameObject floorTile;
    // Wall tile to use
    public GameObject wallTile;

    /// <summary>
    /// Create a room of the specified hight and width in tiles
    /// Position is the bottom left corner of the room
    /// </summary>
    /// <param name="_position"></param>
    /// <param name="_width"></param>
    /// <param name="_height"></param>
    public Room(Vector2 _position, int _width, int _height)
    {
        position = _position;

        tileHeight = _height;
        tileWidth = _width;

        tileMap = new int[tileHeight, tileWidth];
        mobMap = new int[tileHeight, tileWidth];

        tileSize = 0.32f;

        height = tileWidth * tileSize;
        width = tileHeight * tileSize;
        
    }

    public bool IsInRoom(Vector2 point)
    {
        // Check if the point is within the bounds of the room
        return (point.x < position.x + width &&
                point.x > position.x - width) &&
               (point.y < position.y + height &&
                 point.y > position.y - height);
    }

    public void CreateRoom()
    {
        CreateRoomTiles();

        for (int row = 0; row < tileHeight; row++)
        {
            for (int column = 0; column < tileWidth; column++)
            {
                // Calculate the tile position
                Vector3 tilePosition = new Vector3(column * tileSize + position.x, row * tileSize + position.y, 0.1f);

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
    
    public void PrintRoom()
    {
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

}
