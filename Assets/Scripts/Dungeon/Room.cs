using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class Room
{
    /* CONSTANTS BEGIN */
    public static int NORTH = 0;
    public static int EAST = 1;
    public static int SOUTH = 2;
    public static int WEST = 3;

    // Max number of neighbors
    private static int MAX_NEIGHBORS = 4;

    private static int DOOR = 1;
    private static int NO_DOOR = 0;
    /* CONSTANTS END */

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
    // List of mobs in the room (used to save rooms)
    private List<GameObject> mobList;
    // Array representation of items in a room
    private int[,] itemMap;
    private List<GameObject> itemList;

    // Floor tile to use
    public GameObject floorTile;
    // Wall tile to use
    public GameObject wallTile;

    // List of tiles in the room (Use to destroy them)
    private List<GameObject> tileList;

    // The text array containing the room
    string[] roomFile;

    // Array representing doors
    public int[] doors;

    // Number of neighboring rooms
    public int numNeighbors;

    /// <summary>
    /// Create a basic room, with no doors
    /// </summary>
    public Room()
    {
        // 2D Array maps
        //tileMap = new int[tileHeight, tileWidth];
        tileMap = new List<List<int>>();
        mobMap = new int[tileHeight, tileWidth];

        // Instantiate lists
        mobList = new List<GameObject>();
        tileList = new List<GameObject>();

        // Tile prefabs
        floorTile = Resources.Load("Prefabs/FloorTile") as GameObject;
        wallTile = Resources.Load("Prefabs/WallTile") as GameObject;

        // Set up a framework for doors
        doors = new int[4] { NO_DOOR, NO_DOOR, NO_DOOR, NO_DOOR };

        // A room does not know its neighbors on creation
        numNeighbors = 0;
    }

    /// <summary>
    /// Counts the number of doors
    /// </summary>
    /// <returns>Returns number of doors</returns>
    public int GetNumDoors()
    {
        int ret = 0;

        for (int i = 0; i < doors.Length; i++)
        {
            ret += doors[i];
        }

        return ret;
    }

    /// <summary>
    /// Creates a string representing cardinal directions with doors
    /// </summary>
    /// <returns>Returns string representing directions with doors</returns>
    public string GetDoorString()
    {
        string ret = "";

        if (doors[NORTH] != 0)
            ret += "N";
        if (doors[EAST] != 0)
            ret += "E";
        if (doors[SOUTH] != 0)
            ret += "S";
        if (doors[WEST] != 0)
            ret += "W";

        return ret;
    }

    /// <summary>
    /// Creates the room on the map
    /// </summary>
    public void CreateRoom(PointF _position)
    {
        position = _position;

        // Load the room's text file
        TextAsset file = Resources.Load("Rooms/" + GetDoorString() + "1") as TextAsset;
        if (file != null)
            roomFile = file.text.Split('\n');
        else
        {
            Debug.Log("Could not load room file");
            return;
        }

        // Create the room given the constraints
        CreateTileArray();
        // Creat the mobs in the room
        CreateMobArray();

        CreateTiles();
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

    private void CreateTileArray()
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
    }

    private void CreateMobArray()
    {
        mobMap = new int[tileMap.Count, tileMap[0].Count];
        // Randomly add mobs to the array
        for (int row = 0; row < tileMap.Count; row++)
        {
            for (int column = 0; column < tileMap[row].Count; column++)
            {
                // If the grid point is NOT on the edge of the room
                if (!(row == 0 ||
                    row == tileHeight - 1 ||
                    column == 0 ||
                    column == tileWidth - 1))
                {
                    int chance = (int)UnityEngine.Random.Range(0f, 100f);
                    if (chance == 2)
                        mobMap[row, column] = 1;
                }
            }
        }

        // Instantiate the mobs
        for (int row = 0; row < tileMap.Count; row++)
        {
            for (int column = 0; column < tileMap[row].Count; column++)
            {
                // If the grid point is on the edge of the room
                if (mobMap[row, column] == 1)
                {
                    // Calculate the tile position
                    Vector3 mobPosition;

                    // Create wall tile at tilePosition
                    GameObject mob = GameObject.Instantiate(Resources.Load("Prefabs/Blob")) as GameObject;
                    mobPosition = new Vector3(column * DungeonTileK.TILE_SIZE + position.X, row * DungeonTileK.TILE_SIZE + position.Y, mob.transform.position.z);
                    mob.GetComponent<Transform>().position = mobPosition;
                    mobList.Add(mob);
                }
            }
        }
    }

    private void CreateTiles()
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
                    tileList.Add(wall);
                }
                else if (tile == DungeonTileK.FLOOR_TILE)
                {
                    // Create floor tile at tilePosition
                    GameObject floor = GameObject.Instantiate(floorTile) as GameObject;
                    floor.GetComponent<Transform>().position = tilePosition;
                    tileList.Add(floor);
                }
            }
        }
    }

    /// <summary>
    /// Deactivate the room. Destroys all tiles and deactivates all mobs
    /// </summary>
    public void Deactivate()
    {
        // Destroy the tiles and remove them from the list
        foreach (GameObject tile in tileList)
        {
            UnityEngine.Object.Destroy(tile);
        }
        for (int i = tileList.Count - 1; i >= 0; i--)
        {
            tileList.Remove(tileList[i]);
        }

        // Deactivates all the mobs
        foreach (GameObject mob in mobList)
        {
            mob.SetActive(false);
        }
    }

    /// <summary>
    /// Activate the room. Draws all tiles and activates all the mobs
    /// </summary>
    public void Activate()
    {
        // Create the room tiles
        CreateTiles();

        // Activate all the mobs
        foreach (GameObject mob in mobList)
        {
            mob.SetActive(true);
        }
    }
}
