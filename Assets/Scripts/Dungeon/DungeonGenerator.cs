using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GenerateMap();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GenerateMap();
        }
	}

    private void GenerateMap()
    {
        Floor testFloor = new Floor();
        //testLevel.PrintLevel();

        foreach (KeyValuePair<Point, PHRoom> rm in testFloor.GetFloor())
        {
            Room testRoom;
            testRoom = new Room(new PointF(DungeonTileK.TILE_SIZE * rm.Key.X * 5, DungeonTileK.TILE_SIZE * rm.Key.Y * 5), rm.Value.GetDoorString());
            testRoom.CreateRoom();
        }
    }
}
