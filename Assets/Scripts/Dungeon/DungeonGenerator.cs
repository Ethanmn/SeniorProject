using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Floor testFloor = new Floor();
        //testLevel.PrintLevel();

        Dictionary<Point, PHRoom> level = testFloor.GetFloor();

        foreach(KeyValuePair<Point, PHRoom> rm in level)
        {
            Room testRoom;
            testRoom = new Room(new PointF(DungeonTileK.TILE_SIZE * rm.Key.X * 5, DungeonTileK.TILE_SIZE * rm.Key.Y * 5), rm.Value.GetDoorString());
            testRoom.floorTile = Resources.Load("Prefabs/FloorTile") as GameObject;
            testRoom.wallTile = Resources.Load("Prefabs/WallTile") as GameObject;
            testRoom.CreateRoom();
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
