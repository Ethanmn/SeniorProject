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

        // Set the new room
        curRoomPoint = new Point(X, Y);
        dungeon[curFloor - 1].GetFloor()[curRoomPoint].Activate();

        // Reset the player's position to the entrance
        // NEEDS TO BE CHANGED SO THE PLAYER APPEARS AT THE CENTER OF THE EXIT
        Vector3 entrancePos = Vector3.zero;
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        GameObject[] exits = GameObject.FindGameObjectsWithTag("Exit");
        foreach (GameObject exit in exits)
        {
            string exitDir = exit.GetComponent<ExitDoor>().ExitDir;
            if (direction == "n" && exitDir == "s")
            {
                entrancePos = new Vector3(exit.transform.position.x + 
                    (exit.GetComponent<BoxCollider2D>().size.x/2)/*half the size of exit*/, exit.transform.position.y + 0.75f, 0);
            }
            else if (direction == "e" && exitDir == "w")
            {
                entrancePos = new Vector3(exit.transform.position.x + 1.0f, exit.transform.position.y - 
                    (exit.GetComponent<BoxCollider2D>().size.y / 2)/*half the size of exit*/, 0);
            }
            else if (direction == "s" && exitDir == "n")
            {
                entrancePos = new Vector3(exit.transform.position.x + 
                    (exit.GetComponent<BoxCollider2D>().size.x / 2)/*half the size of exit*/, exit.transform.position.y - 1.0f, 0);
            }
            else if (direction == "w" && exitDir == "e")
            {
                entrancePos = new Vector3(exit.transform.position.x - 0.50f, exit.transform.position.y -
                    (exit.GetComponent<BoxCollider2D>().size.y / 2)/*half the size of exit*/, 0);
            }
        }

        // Set the camera's position to the same position as the hero
        Camera.main.transform.position = entrancePos;
        hero.transform.position = entrancePos;

        // Remove all attacks from the scene when transitioning
        GameObject[] attacks = GameObject.FindGameObjectsWithTag("Attack");
        foreach (GameObject attack in attacks)
        {
            Destroy(attack);
        }
    }
}
