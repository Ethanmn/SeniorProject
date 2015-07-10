public class PHRoom
{
    public static int NORTH = 0;
    public static int EAST = 1;
    public static int SOUTH = 2;
    public static int WEST = 3;

    // Max number of neighbors
    private static int MAX_NEIGHBORS = 4;

    private static int DOOR = 1;
    private static int NO_DOOR = 0;

    // Array representing doors
    public int[] doors;

    // Number of neighboring rooms
    public int numNeighbors;

    public PHRoom()
    {
        doors = new int[4] { NO_DOOR, NO_DOOR, NO_DOOR, NO_DOOR };

        numNeighbors = 0;
    }

    public int GetNumDoors()
    {
        int ret = 0;

        for (int i = 0; i < doors.Length; i++)
        {
            ret += doors[i];
        }

        return ret;
    }

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
}