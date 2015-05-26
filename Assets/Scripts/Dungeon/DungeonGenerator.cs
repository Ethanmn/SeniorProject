using UnityEngine;
using System.Collections;

// Credit to David Leon (http://xdavidleon.tumblr.com/)

public class DungeonGenerator : MonoBehaviour
{

    // Dungeon Size
    // NOTE: Must be a square, because of AABB(Center, Half)
    public const int MAP_WIDTH = 64;
    public const int MAP_HEIGHT = 64;

    // Room Size Parameters
    public const int ROOM_MAX_SIZE = 12;
    public const int ROOM_MIN_SIZE = 4;

    // Dungeon Generation Parameters
    public const int MAX_DEPTH = 10;

    // Misc variables
    Tile[,] tiles;
    public GameObject prefab_wall01;

    // The Random Seed
    public int seed;

    // QuadTree for dungeon distribution
    private QuadTree quadTree;

    // On Awake
    void Awake()
    {
        // Create a new QuadTree
        quadTree = new QuadTree(new AABB(new XY(MAP_WIDTH / 2.0f, MAP_HEIGHT / 2.0f), new XY(MAP_WIDTH / 2.0f, MAP_HEIGHT / 2.0f)));

        // Initialize the tilemap
        tiles = new Tile[MAP_WIDTH, MAP_HEIGHT];
        for (int i = 0; i < MAP_WIDTH; i++)
            for (int j = 0; j < MAP_HEIGHT; j++)
                tiles[i, j] = new Tile(Tile.TILE_EMPTY);
    }

    // On Start
    void Start()
    {

    }

    // Each frame
    void Update()
    {
        // Generate a new Test QuadTree
        if (Input.GetButtonDown("Jump"))
        {
            // Generate a new Seed
            seed = System.DateTime.Now.Millisecond;

            // Generate QuadTree
            quadTree.GenerateDungeon(seed);

            // Change Floor texture to visualize room distribution
            Texture2D dungeonFloor = quadTree.DungeonToTexture();
            GameObject floor = GameObject.Find("Floor");
            floor.GetComponent<Renderer>().material.mainTexture = dungeonFloor;
            quadTree.TextureToFile(dungeonFloor);
        }
    }

    // Create a new room at tilemap
    void CreateRoom(bool first, int x1, int y1, int x2, int y2)
    {
        Dig(x1, y1, x2, y2);
        if (first)
        {
            // put the player in the first room
        }
        else
        {
            // put enemies, items or whatever
        }
    }

    // Helper Methods
    bool IsEmpty(int x, int y) { return tiles[x, y].id == Tile.TILE_EMPTY; }

    void SetEmpty(int x, int y) { tiles[x, y].id = Tile.TILE_EMPTY; }

    bool IsFloor(int x, int y) { return tiles[x, y].id == Tile.TILE_FLOOR; }

    void SetFloor(int x, int y) { tiles[x, y].id = Tile.TILE_FLOOR; }

    // Dig a room, placing floor tiles
    void Dig(int x1, int y1, int x2, int y2)
    {
        // Out of range
        if (x2 < x1)
        {
            int tmp = x2;
            x2 = x1;
            x1 = tmp;
        }

        // Out of range
        if (y2 < y1)
        {
            int tmp = y2;
            y2 = y1;
            y1 = tmp;
        }

        // Dig floor
        for (int tilex = x1; tilex <= x2; tilex++)
            for (int tiley = y1; tiley <= y2; tiley++)
                tiles[tilex, tiley].id = Tile.TILE_FLOOR;
    }


}