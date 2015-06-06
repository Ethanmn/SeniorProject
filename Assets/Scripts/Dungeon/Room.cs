using UnityEngine;
using System.Collections;

public class Room {

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
    }

}
