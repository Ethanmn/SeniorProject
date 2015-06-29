﻿using UnityEngine;
using System.Collections;

public class DungeonGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Room testRoom = new Room(new Point(0, 0), 30, 30);
        testRoom.floorTile = Resources.Load("Prefabs/FloorTile") as GameObject;
        testRoom.wallTile = Resources.Load("Prefabs/WallTile") as GameObject;

        testRoom.CreateRoom();

        testRoom.PrintRoom();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
