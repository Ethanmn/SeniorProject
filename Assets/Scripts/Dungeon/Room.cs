using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class Room
{

    // The position of the room, from the bottom left corner
    private PointF position;

    // Height of the room in tiles (y size)
    private int tileWidth;
    // Width of the room in tiles (x size)
    private int tileHeight;

    // Array representation of a rooms tiles
    //private int[,] tileMap;
    private List<List<int>> tileMap;
    // Array representation of mobs in a room
    private int[,] mobMap;
    // Array representation of items in a room
    private int[,] itemMap;

    // Floor tile to use
    public GameObject floorTile;
    // Wall tile to use
    public GameObject wallTile;

    // The text array containing the room
    string[] roomFile;

    /// <summary>
    /// Create a room of the specified hight and width in tiles
    /// Position is the bottom left corner of the room and in world space
    /// Rooms are built up and right
    /// </summary>
    /// <param name="_position"></param>
    /// <param name="_width"></param>
    /// <param name="_height"></param>
    public Room(PointF _position, string doors)
    {
        // World space position
        position = _position;

        // Number of tiles
        //tileHeight = _height;
        //tileWidth = _width;

        // 2D Array maps
        //tileMap = new int[tileHeight, tileWidth];
        tileMap = new List<List<int>>();
        mobMap = new int[tileHeight, tileWidth];

        // Load the room's text file
        TextAsset file = Resources.Load("Rooms/" + doors + "1") as TextAsset;
        roomFile = file.text.Split('\n');

        // Create the room given the constraints
        CreateRoomTiles();
        // Creat the mobs in the room
        //CreateRoomMobs();
    }

    /// <summary>
    /// Creates the room on the map (Obsolete after Floor is implemented)
    /// </summary>
    public void CreateRoom()
    {
        for (int row = tileMap.Count - 1; row >= 0; row--)
        {
            for (int column = 0; column < tileMap[row].Count; column++)
            {
                // Calculate the tile position
                Vector3 tilePosition = new Vector3(column * DungeonTileK.TILE_SIZE + position.X, (tileMap.Count - 1 - row) * DungeonTileK.TILE_SIZE + position.Y, 0.1f);

                int tile = tileMap[row][column];

                // If the grid point is on the edge of the room
                if (tile == DungeonTileK.WALL_TILE)
                {
                    // Create wall tile at tilePosition
                    GameObject wall = GameObject.Instantiate(wallTile) as GameObject;
                    wall.GetComponent<Transform>().position = tilePosition;
                }
                else if (tile == DungeonTileK.FLOOR_TILE)
                {
                    // Create floor tile at tilePosition
                    GameObject floor = GameObject.Instantiate(floorTile) as GameObject;
                    floor.GetComponent<Transform>().position = tilePosition;
                }
            }
        }
    }
    
    /// <summary>
    /// [Depricated] Prints the room as ascii values to the console
    /// </summary>
    /// WARNING, THIS FUNCTION IS OUT OF DATE, IT USES THE OLD 2D ARRAY TILEMAP
    public void PrintRoom()
    {
        /*
        // String to print the map
        String map = "";
        // Add the name
        map += "tileMap" + Environment.NewLine;
        
        // For each point in the 2D array
        for (int row = 0; row < tileMap.Count; row++)
        {
            for (int column = 0; column < tileMap[row]; column++)
            {
                // Add that value to the string with two spaces
                map += tileMap[row][column] + "  ";
            }
            
           map += Environment.NewLine;
        }
        Debug.Log(map);
        */
    }

    private void CreateRoomTiles()
    {
        

        foreach (string line in roomFile)
        {
            // Create the row to add
            List<int> row = new List<int>();
            foreach (char val in line)
            {
                int valInt = Convert.ToInt32(Char.GetNumericValue(val));
                row.Add(valInt);
            }
            // Add the row to the map
            tileMap.Add(row);
        }

        /*
        string line = null;
        // Use the room file
        using (rmFile)
        {
            // For each line in the room file
            do
            {
                // Read the line
                line = rmFile.ReadLine();
                
                // FOR EACH character in the line, add it to the list
                if (line != null)
                {
                    // Create the row to add
                    List<int> row = new List<int>();
                    foreach (char val in line)
                    {
                        int valInt = Convert.ToInt32(Char.GetNumericValue(val));
                        row.Add(valInt);
                    }
                    // Add the row to the map
                    tileMap.Add(row);
                }

            } while (line != null);
        }
        */
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
