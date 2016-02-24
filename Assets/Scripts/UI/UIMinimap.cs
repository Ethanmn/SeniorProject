using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIMinimap : MonoBehaviour {
    // Floors on the map
    List<Dictionary<Point, GameObject>> floors;

    // Sprites for the rooms
    Sprite sizeSprite;

    GameObject playerMarker;

    // Size of the room sprites
    private float pixelScaler;
    private float spriteWidth;
    private float spriteHeight;

    // Use this for initialization
    void Start () {
        // Get a sample sprite
        sizeSprite = Resources.Load<Sprite>("Sprites/Minimap/MiniNESW");

        // Use that for size
        pixelScaler = transform.parent.GetComponent<CanvasScaler>().referencePixelsPerUnit;
        spriteWidth = sizeSprite.bounds.size.x * pixelScaler;
        spriteHeight = sizeSprite.bounds.size.y * pixelScaler;

        floors = new List<Dictionary<Point, GameObject>>();

        // Move the RoomParent according to how many rooms there are to the East and South
        int eastRooms = 0;
        int southRooms = 0;
        Floor floor = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>().Dungeon[0];
        // Get the number of rooms east and south
        foreach (Point point in floor.GetFloor().Keys)
        {
            eastRooms = eastRooms < point.X ? point.X : eastRooms;
            southRooms = southRooms > point.Y ? point.Y : southRooms;
        }

        transform.localPosition -= new Vector3(eastRooms * spriteWidth, southRooms * spriteHeight);

        // Listen for room entering events
        PublisherBox.onRoomEnterPub.RaiseOnRoomEnterEvent += HandleOnRoomEnter;
        // Signal for the first room
        PublisherBox.onRoomEnterPub.RaiseEvent(0, 0, 0);
    }

    void OnDestroy()
    {
        // Stop listening for room entering events
        PublisherBox.onRoomEnterPub.RaiseOnRoomEnterEvent -= HandleOnRoomEnter;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void HandleOnRoomEnter(object sender, POnRoomEnterEventArgs e)
    {
        // Use e.X and e.Y to show the room on the map

        // If the floor has not yet been visited, create a map dictionary for it
        if (floors.Capacity <= e.Floor)
        {
            floors.Insert(e.Floor, new Dictionary<Point, GameObject>());
        }

        // If the room has not been visited yet
        if (!floors[e.Floor].ContainsKey(new Point(e.X, e.Y)))
        {
            // Get the room's doors
            string doors = GameObject.FindGameObjectWithTag("DungeonManager").
                GetComponent<DungeonManager>().Dungeon[e.Floor].GetFloor()[new Point(e.X, e.Y)].GetDoorString();

            // Create a new room object
            GameObject newRoom = new GameObject();
            newRoom.layer = gameObject.layer;
            newRoom.name = e.Floor + " " + e.X + " " + e.Y;
            newRoom.transform.SetParent(transform, false);
            newRoom.AddComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Minimap/Mini" + doors);
            newRoom.GetComponent<RectTransform>().sizeDelta = new Vector2(spriteWidth, spriteHeight);
            newRoom.transform.localPosition = new Vector3(e.X * spriteWidth, e.Y * spriteHeight);
            newRoom.GetComponent<Image>().color = new Color(1, 1, 1, 0.65f); 

            floors[e.Floor].Add(new Point(e.X, e.Y), newRoom);
        }

        // IF there is no player marker yet, make one!
        if (playerMarker == null)
        {
            // Create a new room object
            GameObject newPlayerMarker = new GameObject();
            newPlayerMarker.layer = gameObject.layer;
            newPlayerMarker.name = "PlayerMarker";
            newPlayerMarker.transform.SetParent(transform, false);
            newPlayerMarker.AddComponent<Image>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[0];
            newPlayerMarker.GetComponent<RectTransform>().sizeDelta = new Vector2(spriteWidth / 4, spriteHeight / 2);
            playerMarker = newPlayerMarker;
        }
        // Set the player marker
        // Bring it to the front
        playerMarker.transform.SetAsLastSibling();
        // Set the position
        playerMarker.transform.localPosition = new Vector3(e.X * spriteWidth, e.Y * spriteHeight);

        // Draw the map
        DrawMap();
    }

    private void DrawMap()
    {
        // Draw all rooms on this floor
    }
}
