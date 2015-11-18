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

    // Room prefab to use
    private GameObject room;

    public GameObject RoomObject
    {
        get
        {
            return room;
        }

        set
        {
            room = value;
        }
    }

    // The position of the room, from the bottom left corner
    private PointF position;

    // Is this the root?
    private bool root;

    // Has this room been cleared?
    private bool cleared;
    public bool Cleared { get { return cleared; } }

    // List of mobs in the room (used to save rooms)
    private List<GameObject> mobList;
    // List representation of items in a room
    private List<GameObject> itemList;

    // Array representing doors
    public int[] doors;

    // Number of neighboring rooms
    public int numNeighbors;

    /// <summary>
    /// Create a basic room, with no doors
    /// </summary>
    public Room(bool root)
    {
        // Instantiate lists
        mobList = new List<GameObject>();
        itemList = new List<GameObject>();

        // Set up a framework for doors
        doors = new int[4] { NO_DOOR, NO_DOOR, NO_DOOR, NO_DOOR };

        // A room does not know its neighbors on creation
        numNeighbors = 0;

        this.root = root;

        // Subscribe to the OnKillEvent to track clearing state
        PublisherBox.onKillPub.RaiseOnKillEvent += HandleOnKillEvent;
    }

    /// <summary>
    /// Sets the room's prefab to spawn
    /// </summary>
    /// <returns>Returns the room</returns>
    public GameObject SetRoom(int floorNum)
    {
        string roomName;

        // Room name is defined by floor # + doors + varient #
        if (!root)
        {
            roomName = /*floorNum.ToString() OR generic number (0) +*/ GetDoorString() + "1" /*Random 1-3*/;
        }
        // If it IS the root, make a root room
        else
        {
            roomName = /*floorNum.ToString() OR generic number (0) +*/ GetDoorString() + "R" /*Random 1-3*/;
        }

        GameObject roomObj = Resources.Load<GameObject>(roomName);
        // If the room cannot be found, throw an error
        if (roomObj == null)
        {
            Debug.Log("Could not load room " + roomName);
            return null;
        }
        // Instantiate the room
        room = GameObject.Instantiate(roomObj);
        // Run the start to make sure the exit directions are set
        // Another stupid work around because rooms would not call their Start() before deactivating
        ExitDoor[] doors = room.GetComponentsInChildren<ExitDoor>();
        foreach (ExitDoor door in doors)
        {
            door.SetDirection();
        }

        // Check if the room is cleared
        cleared = CheckCleared();

        Deactivate();

        return room;
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
    /// Activate the room. Draws all tiles and activates all the mobs
    /// </summary>
    public void Activate()
    {
        if (room != null)
        {
            room.SetActive(true);
        }
        else
            Debug.Log("No room to activate!");

        // Activate all the mobs
        foreach (GameObject mob in mobList)
        {
            mob.SetActive(true);
        }

        foreach (GameObject item in itemList)
        {
            item.SetActive(true);
        }

        // Subscribe to the OnKillEvent to continue tracking clearing
        PublisherBox.onKillPub.RaiseOnKillEvent += HandleOnKillEvent;
    }

    /// <summary>
    /// Deactivate the room. Destroys all tiles and deactivates all mobs
    /// </summary>
    public void Deactivate()
    {
        
        // Turn off the room prefab
        if (room != null)
        {
            room.SetActive(false);
        }
        else
            Debug.Log("No room to deactivate!");

        // Deactivates all the mobs
        foreach (GameObject mob in mobList)
        {
            mob.SetActive(false);
        }

        foreach (GameObject item in itemList)
        {
            item.SetActive(false);
        }

        // Unsubscribe to the OnKillEvent so it doesn't track while the room is deactivated
        PublisherBox.onKillPub.RaiseOnKillEvent -= HandleOnKillEvent;
    }

    private void HandleOnKillEvent(object sender, POnKillEventArgs e)
    {
        // Check if the room is cleared
        cleared = CheckCleared();
        Debug.Log("Checking after a kill! " + cleared);
    }

    private bool CheckCleared()
    {
        Transform mobs = RoomObject.transform.FindChild("Mobs");
        if (mobs.childCount > 0)
        {
            if (mobs.FindChild("TileObject").childCount > 0)
                return false;
            else
                return true;
        }
        else
        {
            Debug.Log("Room cleared!");
            return true;
        }

        return false;
    }
}
