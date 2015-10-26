using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour {

    Floor thisFloor;
    bool active = true;

	// Use this for initialization
	void Start () {
        GenerateMap();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
            if (active)
            {
                foreach (KeyValuePair<Point, Room> rm in thisFloor.GetFloor())
                {
                    rm.Value.Deactivate();
                }
                active = false;
            }
            else
            {
                foreach (KeyValuePair<Point, Room> rm in thisFloor.GetFloor())
                {
                    rm.Value.Activate();
                }
                active = true;
            }
        }
	}

    private void GenerateMap()
    {
        thisFloor = new Floor();

        foreach (KeyValuePair<Point, Room> rm in thisFloor.GetFloor())
        {
            rm.Value.CreateRoom(new PointF(DungeonTileK.TILE_SIZE * rm.Key.X * 5, DungeonTileK.TILE_SIZE * rm.Key.Y * 5));
        }
    }
}
