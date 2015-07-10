using UnityEngine;
using System;
using System.Collections.Generic;

public class Floor
{
    List<Room> rooms;
    Dictionary<Point, GameObject> map;

    public Floor()
    {
        rooms = new List<Room>();
        map = new Dictionary<Point, GameObject>();
    }

    public string GenerateFloor()
    {
        int numRooms = 0;
        string ret = "";

        // Create the first room
        Room startRoom = new Room(new PointF(0, 0), 30, 30);
        startRoom.CreateRoom();

        // Add the first room
        rooms.Add(startRoom);
        numRooms++;

        /* 
        Create random rooms according to the following rules:
        1) Room size and position are randomly decided
        2) Rooms position are based on the position of the three rooms that came before it
        3) Rooms may only move as far as their length in either direction (AKA rooms must touch at some point)
        */



        return ret;
    }
}
