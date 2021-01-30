using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    private Room CurrentRoom
    {
        get => _currentRoom;
        set
        {
            _currentRoom?.gameObject.SetActive(false);
            _currentRoom = value;
            _currentRoom.gameObject.SetActive(true);
        }
    }

    public LevelMap levelMap;
    public Transform player;
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject doorTriggerPrefab;

    private List<Room> loadedRooms = new List<Room>();
    private List<DoorTrigger> doors = new List<DoorTrigger>();
    private Room _currentRoom = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        GameObject.DontDestroyOnLoad(this.gameObject);
        Load(new Vector2Int(0,0));
    }

    public void Load(Vector2Int coordinates)
    {
        var newRoom = loadedRooms.FirstOrDefault(r => r.coordinates == coordinates);

        if (!newRoom) // if room has not been pooled
        {
            RoomData data = levelMap.GetRoomData(coordinates);
            if (!data) return;

            newRoom = GenerateRoom(data, true);
        }

        CurrentRoom = newRoom;
    }

    public void MoveRooms(Compass door)
    {
        var coord = CurrentRoom.coordinates;

        switch (door)
        {
            case Compass.E:
                coord += Vector2Int.right;
                break;
            case Compass.N:
                coord += Vector2Int.up;
                break;
            case Compass.S:
                coord += Vector2Int.down;
                break;
            case Compass.W:
                coord += Vector2Int.left;
                break;
        }

        if (coord != CurrentRoom.coordinates)
        {
            Load(coord);
            player.position = GetRoomStart(door);
        }
    }

    private Vector2 GetRoomStart(Compass comingFrom)
    {
        switch (comingFrom)
        {
            case Compass.E: return CurrentRoom.data.WestStart;
            case Compass.W: return CurrentRoom.data.EastStart;
            case Compass.N: return CurrentRoom.data.SouthStart;
            case Compass.S: return CurrentRoom.data.NorthStart;
            default: return Vector2.zero;
        }
    }

    private Room GenerateRoom(RoomData data, bool loadAdjacent = false)
    {
        var gameObj = new GameObject();
        gameObj.AddComponent<Room>();
        gameObj.name = data.coordinates.ToString();

        var room = gameObj.GetComponent<Room>();
        room.coordinates = data.coordinates;
        room.data = data;

        var tilemap = Instantiate(data.TilemapPrefab, gameObj.transform);

        if (loadAdjacent)
        {
            GenerateAdjacentRooms(data);
        }

        gameObj.SetActive(false);
        loadedRooms.Add(room);
        return room;
    }

    private void GenerateAdjacentRooms(RoomData current)
    {
        var coord = current.coordinates;

        var newCoords = new List<Vector2Int>() {
             new Vector2Int(coord.x + 1, coord.y),
             new Vector2Int(coord.x - 1, coord.y),
             new Vector2Int(coord.x, coord.y + 1),
             new Vector2Int(coord.x, coord.y - 1)
        };

        for (int i = 0; i < newCoords.Count; i++)
        {
            if (!loadedRooms.Any(r => r.coordinates == newCoords[i])) {
                var data = levelMap.GetRoomData(newCoords[i]);
                if (!data) return;
                GenerateRoom(data);
            }
        }
    }
}
