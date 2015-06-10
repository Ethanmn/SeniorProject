using UnityEngine;
using System.Collections;
using System;

public class Room {

    // Constants
    private static int FLOOR_TILE = 1;
    private static int WALL_TILE = 2;
    private static int HOLE_TILE = 0;

    // The position of the room, from the center
    private Vector2 position;

    // Height of the room in tiles (y size)
    private int tileHeight;
    // Width of the room in tiles (x size)
    private int tileWidth;

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

        tileMap = new int[_width, _height];
        mobMap = new int[_width, _height];

        tileSize = 0.32f;

        tileWidth = _width;
        tileHeight = _height;

        height = tileHeight * tileSize;
        width = tileWidth * tileSize;
        
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
        for (int x = 0; x < tileWidth; x++)
        {
            for (int y = 0; y < tileHeight; y++)
            {
                // Calculate the tile position
                Vector3 tilePosition = new Vector3(x * tileSize + position.x, y * tileSize + position.y, 0.1f);

                // If the grid point is on the edge of the room
                if (x == 0 ||
                    x == tileWidth - 1 ||
                    y == 0 ||
                    y == tileHeight - 1)
                {
                    // Create wall tile at tilePosition
                    GameObject wall = GameObject.Instantiate(wallTile) as GameObject;
                    wall.GetComponent<Transform>().position = tilePosition;
                }
                else
                {
                    // Create floor tile at tilePosition
                    GameObject floor = GameObject.Instantiate(floorTile) as GameObject;
                    floor.GetComponent<Transform>().position = tilePosition;
                }
            }
        }
        CreateRoomTiles();
    }
    
    public void PrintRoom()
    {
        String map = "";
        for (int x = 0; x < tileMap.GetLength(0); x++)
        {
            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                map += tileMap[x, y] + "  ";
            }
            
           map += Environment.NewLine;
        }
        Debug.Log(map);
    }

    private void CreateRoomTiles()
    {
        for (int x = 0; x < tileWidth; x++)
        {
            for (int y = 0; y < tileHeight; y++)
            {
                // If the grid point is on the edge of the room
                if (x == 0 ||
                    x == tileWidth - 1 ||
                    y == 0 ||
                    y == tileHeight - 1)
                {
                    // Create wall tile at tilePosition
                    tileMap[x, y] = WALL_TILE;
                }
                else
                {
                    // Create floor tile at tilePosition
                    tileMap[x, y] = FLOOR_TILE;
                }
            }
        }
    }

}
