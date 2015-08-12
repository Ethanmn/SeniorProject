using UnityEngine;
using System.Collections.Generic;


public class Floor
{
    // Max number of neighbors
    private static int MAX_NEIGHBORS = 4;

    private static int DOOR = 1;
    private static int NO_DOOR = 0;

    // Room constants
    private static int MIN_ROOMS = 10;
    private static int MAX_ROOMS = 15;

    // Direction constants
    private static Point NP = new Point(0, 1);
    private static Point EP = new Point(1, 0);
    private static Point SP = new Point(0, -1);
    private static Point WP = new Point(-1, 0);

    private static Dictionary<Point, int> DIRECTIONS = new Dictionary<Point, int>();

    // Number of rooms in this floor
    private int numRooms;
    // Graph / Point representation of floor rooms
    private Dictionary<Point, PHRoom> floor;
    // Queue of rooms that need neighboring rooms
    private List<KeyValuePair<Point, PHRoom>> roomQ;

    // Root room
    private KeyValuePair<Point, PHRoom> rootRoom;

    public Floor()
    {
        // Determine the number of rooms randomly
        numRooms = Random.Range(MIN_ROOMS, MAX_ROOMS);
        floor = new Dictionary<Point, PHRoom>();
        roomQ = new List<KeyValuePair<Point, PHRoom>>();

        DIRECTIONS.Add(NP, 0);
        DIRECTIONS.Add(EP, 1);
        DIRECTIONS.Add(SP, 2);
        DIRECTIONS.Add(WP, 3);
    }

    public void PrintFloor()
    {
        Debug.Log(numRooms + " rooms");
        int i = 0;
        foreach (KeyValuePair<Point, PHRoom> room in floor)
        {
            i++;
            Debug.Log("Room " + i + " @ " + room.Key.ToString() + "\n" + "Doors " + room.Value.GetDoorString());
        }
        if (numRooms != floor.Count)
        {
            Debug.Log("Number of rooms [" + numRooms + "] does not match the actual numbers of rooms [" + floor.Count + "]");
        }
    }

    public Dictionary<Point, PHRoom> GetFloor()
    {
        return this.floor;
    }

    public void CreateRooms()
    {
        // Number of rooms created thus far
        int countRooms = 0;

        // Create the ROOT ROOM
        rootRoom = new KeyValuePair<Point, PHRoom>(new Point(0, 0), new PHRoom());

        // Add the room to the floor
        floor.Add(rootRoom.Key, rootRoom.Value);
        countRooms++;

        // Randomly determine number of neighbouring rooms to create
        int addRooms = Random.Range(2, 4);

        // List of valid direction to make a room in
        List<Point> directions = new List<Point>();
        // Populate the list with all cardinal directions
        foreach (Point key in DIRECTIONS.Keys)
        {
            directions.Add(key);
        }

        // Loop around until all rooms have been created
        while (addRooms > 0)
        {
            // Randomly choose the direction of the room
            int roomDir = Random.Range(0, directions.Count);
            Point addRoomPt;

            // Convert random int to direction
            addRoomPt = rootRoom.Key + directions[roomDir];
            // Remove that point from the possible directions
            directions.RemoveAt(roomDir);

            // Create the room
            PHRoom addPHRoom = new PHRoom();

            KeyValuePair<Point, PHRoom> addRoom =
                new KeyValuePair<Point, PHRoom>(addRoomPt, addPHRoom);

            // Add room to floor
            floor.Add(addRoom.Key, addRoom.Value);

            // Add the new room to the room queue
            roomQ.Add(addRoom);

            // Increment the created rooms total
            countRooms++;

            // Decrement number of rooms needed to create
            addRooms--;
        }

        // The root room and its neighbors are all set up
        // CREATE THE REST OF THE ROOMS
        // Go through the room queue and add rooms until # of rooms is achieved
        KeyValuePair<Point, PHRoom> curRoom;
        int idx = 0;
        // Index used to fix the "trapped in a hole" edge case (last room on queue is surrounded)
        int fix = 0;
        Point edgePoint = null;
        while (countRooms < numRooms)
        {
            while (idx < roomQ.Count)
            {
                // Get the room from the queue
                curRoom = (KeyValuePair<Point, PHRoom>)roomQ[idx++];

                // Create the set of directions
                directions = new List<Point>();

                // Count the number of neighbors
                int numNeighbors = 0;

                // FOR EACH direction, check if there is a neighbor
                foreach (KeyValuePair<Point, int> dir in DIRECTIONS)
                {
                    // IF there is a neighbor to this room
                    if (floor.ContainsKey(curRoom.Key + dir.Key))
                    {
                        // Increment the number of neighbors
                        numNeighbors++;
                    }
                    //ELSE
                    else
                    {
                        // Add it to valid directions list
                        directions.Add(dir.Key);
                    }
                }

                // Randomly determine number of neighbouring rooms to add
                // MIN = 0 (OR 1, if this is the last room on the queue and the number of rooms to add has not been achieved)
                // MAX = 4 - numNeighbors
                if (idx == roomQ.Count && numNeighbors < MAX_NEIGHBORS)
                {
                    addRooms = Random.Range(1, MAX_NEIGHBORS - numNeighbors);
                }
                else
                {
                    addRooms = Random.Range(0, MAX_NEIGHBORS - numNeighbors);
                }

                // Loop around until all rooms have been added OR until max rooms have been made
                while (countRooms < numRooms && addRooms > 0)
                {
                    // Randomly choose the direction of the room
                    int roomDir = Random.Range(0, directions.Count);
                    Point addRoomPt;

                    // Convert the random int into a direction point
                    addRoomPt = curRoom.Key + directions[roomDir];
                    directions.RemoveAt(roomDir);

                    // Create the room
                    PHRoom addPHRoom = new PHRoom();
                    KeyValuePair<Point, PHRoom> addRoom =
                        new KeyValuePair<Point, PHRoom>(addRoomPt, addPHRoom);

                    // Add room to floor
                    floor.Add(addRoom.Key, addRoom.Value);

                    // Add the new room to the room queue
                    roomQ.Add(addRoom);

                    // Increment the created rooms total
                    countRooms++;

                    // Decrement number of rooms left to add
                    addRooms--;
                }
            }
            // Catches the case where the queue runs out of rooms before the number of rooms has been a achieved
            if (countRooms < numRooms)
            {
                // New edge case
                if (!roomQ[roomQ.Count - 1].Key.Equals(edgePoint))
                {
                    fix = 2;
                    edgePoint = roomQ[roomQ.Count - 1].Key;
                }

                //Debug.Log("Edge case @ " + roomQ[roomQ.Count - 1].Key.ToString());

                if (idx - fix >= 0) idx -= fix++;
            }
        }

        // Do a final loop to count up neighbors
        foreach (KeyValuePair<Point, PHRoom> room in roomQ)
        {
            int numNeighbors = 0;

            foreach (KeyValuePair<Point, int> dir in DIRECTIONS)
            {
                if (floor.ContainsKey(room.Key + dir.Key))
                {
                    numNeighbors++;
                }
            }

            room.Value.numNeighbors = numNeighbors;

        }
    }

    /***
    Pre-requisite: For maximum efficiency (aka for it to do anything) run CreateRooms() first
    */
    public void CreateDoors()
    {
        // Add doors to the root
        // Check roomQ 0-3, only those could have originally been from the root
        for (int i = 0; i < 3; i++)
        {
            // Get the key from the queue
            Point key = roomQ[i].Key;

            // If this room is a cardinal direction
            if (DIRECTIONS.ContainsKey(key))
            {
                // Create the door in the root room
                floor[rootRoom.Key].doors[DIRECTIONS[key]] = DOOR;
                // Create the door in the checking room
                if (DIRECTIONS[key] < 2)
                {
                    floor[key].doors[DIRECTIONS[key] + 2] = DOOR;
                }
                else
                {
                    floor[key].doors[DIRECTIONS[key] - 2] = DOOR;
                }
            }
        }

        // Add doors randomly to the rest of the rooms
        foreach (KeyValuePair<Point, PHRoom> room in roomQ)
        {
            // List of directions that are allowed
            List<Point> directions = new List<Point>();

            // Find the valid directions
            directions = GetDirections(room);

            // Add the doors
            // Randomly add between 0 and 3 (or whatever is allowed) doors
            // This is just a small tweak to make slightly more interesting dungeons
            int numAddDoors = Random.Range(0, System.Math.Min(3, directions.Count));
            int doorsAdded = AddDoors(room.Key, directions, numAddDoors);
        }

        // Now make sure every door can be reached by the root room
        // Create a list of stranded rooms to check on at the very end
        List<KeyValuePair<Point, PHRoom>> strandedRooms = new List<KeyValuePair<Point, PHRoom>>();

        // FOR EACH room in the roomQ
        foreach (KeyValuePair<Point, PHRoom> room in roomQ)
        {
            // Attempt to find the root from each room
            // IF the room cannot find the ROOT ROOM
            if (!FindRoot(room.Key))
            {
                //Debug.WriteLine("Room @ " + room.Key.ToString() + " NOT found the root!"); //Debug.Log("Room @ " + room.Key.ToString() + " NOT found the root!");
                bool foundRoot = false;
                List<Point> directions;

                // While there are possible doors
                while (room.Value.GetNumDoors() < room.Value.numNeighbors && !foundRoot)
                {
                    //Debug.WriteLine("Room @ " + room.Key.ToString() + " is getting another door"); //Debug.Log("Room @ " + room.Key.ToString() + " is getting another door");
                    // ADD a door
                    directions = GetDirections(room);
                    AddDoors(room.Key, directions, 1);
                    // Check if it is still stranded
                    foundRoot = FindRoot(room.Key);
                }

                // Ran out of doors without ever finding the root
                if (!foundRoot)
                {
                    strandedRooms.Add(room);
                    //Debug.Log("Room @ " + room.Key.ToString() + " ran out of doors");
                }
                // Adding doors enabled the room to make its way home
                else
                {
                    //Debug.Log("Room @ " + room.Key.ToString() + " found the root@");
                }
            }
            // ELSE the room is fine
            else
            {
                // Found the root
                //Debug.Log("Room @ " + room.Key.ToString() + " found the root~");
            }
        }

        // FOR EACH room in the stranded list, make a final check
        foreach (KeyValuePair<Point, PHRoom> room in strandedRooms)
        {
            if (!FindRoot(room.Key))
            {
                Debug.Log("Room @ " + room.Key.ToString() + " is STRANDED FOREVER!!");
            }
            else
            {
                Debug.Log("Room @ " + room.Key.ToString() + " found its way home~~");
            }
        }
        // Doors are done!
    }

    private bool FindRoot(Point room)
    {
        // List of all Points visited
        List<Point> visited = new List<Point>();
        // List of rooms to visit
        Queue<Point> searchQ = new Queue<Point>();
        // The current room
        Point curRoom = room;

        do
        {
            // Add this room to visited list
            visited.Add(curRoom);

            // If this room is the root room, stop
            if (curRoom.Equals(rootRoom.Key))
            {
                return true;
            }
            else
            {
                PHRoom curValue;
                if (!floor.TryGetValue(curRoom, out curValue))
                {
                    Debug.Log("SOMETHING WENT WRONG!");
                }

                // Find the doors and add each valid connecting room to the queue
                // FOR EACH cardinal direction
                foreach (KeyValuePair<Point, int> dir in DIRECTIONS)
                {
                    // IF it is a valid neighbor (is connected via a door, is in the floor, has NOT been visited, is NOT in the queue)
                    if (curValue.doors[dir.Value] == DOOR &&
                        floor.ContainsKey(curRoom + dir.Key) &&
                        !visited.Contains(curRoom + dir.Key) &&
                        !searchQ.Contains(curRoom + dir.Key))
                    {
                        // Add the neighbor to the queue
                        searchQ.Enqueue(curRoom + dir.Key);
                    }
                }
            }
            // Continue as long as there are things on the queue
        } while (searchQ.Count > 0 && ((curRoom = searchQ.Dequeue()) != null));

        return false;
    }

    private int AddDoors(Point roomKey, List<Point> directions)
    {
        // Randomly choose number of doors to add
        // MIN = 0
        // MAX = number of neighbors - number of doors already exist
        int numAddDoors;

        if (floor[roomKey].GetNumDoors() == 0)
        {
            numAddDoors = Random.Range(1, directions.Count);
        }
        else
        {
            numAddDoors = Random.Range(0, directions.Count);
        }

        return AddDoors(roomKey, directions, numAddDoors);
    }

    private int AddDoors(Point roomKey, List<Point> directions, int numAddDoors)
    {
        int ret = numAddDoors;
        // Add that number of doors randomly to connected rooms
        while (numAddDoors > 0 && directions.Count > 0)
        {
            // Get a random direction
            int dirIdx = Random.Range(0, directions.Count - 1);
            Point chosenDir = directions[dirIdx];

            // Remove the direction from the list
            directions.RemoveAt(dirIdx);
            // Decrement the number of doors to make
            numAddDoors--;

            // Create the door for that direction
            floor[roomKey].doors[DIRECTIONS[chosenDir]] = DOOR;
            // Switch between NE EW and SN WE
            if (DIRECTIONS[chosenDir] < 2)
            {
                floor[roomKey + chosenDir].doors[DIRECTIONS[chosenDir] + 2] = DOOR;
            }
            else
            {
                floor[roomKey + chosenDir].doors[DIRECTIONS[chosenDir] - 2] = DOOR;
            }
        }

        return ret;
    }

    private List<Point> GetDirections(KeyValuePair<Point, PHRoom> room)
    {
        List<Point> directions = new List<Point>();

        // FOR EACH carinal direction
        foreach (KeyValuePair<Point, int> dir in DIRECTIONS)
        {
            // IF there is not already a door AND the room exists
            if (room.Value.doors[dir.Value] == NO_DOOR &&
                floor.ContainsKey(room.Key + dir.Key))
            {
                // ADD that direction to the valid directions list
                directions.Add(dir.Key);
            }
        }

        return directions;
    }
}
