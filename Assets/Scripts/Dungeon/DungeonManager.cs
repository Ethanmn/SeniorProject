﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonManager : MonoBehaviour {

    // Dungeon variable is a list of floors
    private List<Floor> dungeon;

    // Number of floors in the dungeon
    private int numFloors = 3;

    // Number of the floor hero is on
    private int curFloor = 1;
    // Point of the current room the hero is in
    private Point curRoomPoint;

	// Use this for initialization
	void Start () {
        // Initial set up to start a dungeon
        dungeon = new List<Floor>();
        GenerateDungeon(numFloors);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void GenerateDungeon(int floors)
    {
        // Create each floor
        for (int i = 0; i < floors; i++)
        {
            // Create the floor
            // Add it to the dungeon
            Floor genFloor = new Floor(i + 1);
            dungeon.Add(genFloor);
        }

        // Activate the root of the first floor
        dungeon[0].GetRoot().Activate();
        curRoomPoint = new Point(0, 0);
    }

    /// <summary>
    /// Move from the current room in the direction specified
    /// </summary>
    /// <param name="direction"></param>
    public void MoveRoom(string direction)
    {
        print("Moving " + direction);
        // Deactivate the current room
        dungeon[curFloor - 1].GetFloor()[curRoomPoint].Deactivate();
        
        // Activate the next room
        int X = curRoomPoint.X;
        int Y = curRoomPoint.Y;

        if (direction == "n")
        {
            Y++;
        }
        else if (direction == "e")
        {
            X++;
        }
        else if (direction == "s")
        {
            Y--;
        }
        else if (direction == "w")
        {
            X--;
        }
        print("Moving from (" + curRoomPoint.X + ", " + curRoomPoint.Y + ") to (" + X + ", " + Y + ")");

        // Set the new room
        curRoomPoint = new Point(X, Y);
        dungeon[curFloor - 1].GetFloor()[curRoomPoint].Activate();

        // Reset the player's position (For some reason ExitDir works, while other times it is not even close to working)
        Vector3 entrancePos = Vector3.zero;
        GameObject[] exits = GameObject.FindGameObjectsWithTag("Exit");
        foreach (GameObject exit in exits)
        {
            string exitDir = exit.GetComponent<ExitDoor>().ExitDir;
            if (direction == "n" && exitDir == "s")
            {
                print("NS");
                entrancePos = new Vector3(exit.transform.position.x, exit.transform.position.y + 0.75f, 0);
            }
            else if (direction == "e" && exitDir == "w")
            {
                print("EW");
                entrancePos = new Vector3(exit.transform.position.x + 1.75f, exit.transform.position.y, 0);
            }
            else if (direction == "s" && exitDir == "n")
            {
                print("SN");
                entrancePos = new Vector3(exit.transform.position.x, exit.transform.position.y - 1.75f, 0);
            }
            else if (direction == "w" && exitDir == "e")
            {
                print("WE");
                entrancePos = new Vector3(exit.transform.position.x - 0.75f, exit.transform.position.y, 0);
            }
        }
        Camera.main.transform.position = entrancePos;
        GameObject.FindGameObjectWithTag("Hero").transform.position = entrancePos;
        
    }
}