using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Level testLevel = new Level();
        testLevel.CreateRooms();
        testLevel.CreateDoors();
        testLevel.PrintLevel();

        Dictionary<Point, PHRoom> level = testLevel.GetLevel();

        foreach(Point p in level.Keys)
        {
            Room testRoom;
            testRoom = new Room(new PointF(DungeonTileK.TILE_SIZE * p.X * 10, DungeonTileK.TILE_SIZE * p.Y * 10), 10, 10);
            testRoom.floorTile = Resources.Load("Prefabs/FloorTile") as GameObject;
            testRoom.wallTile = Resources.Load("Prefabs/WallTile") as GameObject;
            testRoom.CreateRoom();
        }
        /*
        Room testRoom = new Room(new Point(0, 0), 30, 30);
        testRoom.floorTile = Resources.Load("Prefabs/FloorTile") as GameObject;
        testRoom.wallTile = Resources.Load("Prefabs/WallTile") as GameObject;

        testRoom.CreateRoom();

        testRoom.PrintRoom();
        */
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
