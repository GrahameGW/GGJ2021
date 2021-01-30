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
    private Room _currentRoom = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Load(new Vector2Int(0,0));
    }

    private void Start()
    {
        player.position = GetRoomStart(Compass.E);
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

    private GameObject SetDoor(Vector2 pos, float rotation, Compass dir)
    {
        var obj = Instantiate(doorTriggerPrefab);
        obj.transform.position = pos;
        obj.transform.eulerAngles = new Vector3(0, 0, rotation);

        var trigger = obj.GetComponent<DoorTrigger>();
        trigger.Direction = dir;
        trigger.Loader = this;

        return obj;
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

        var leftWall = Instantiate(wallPrefab, gameObj.transform);
        leftWall.transform.position = new Vector2(-data.Width * 0.5f, 0);

        var rightWall = Instantiate(wallPrefab, gameObj.transform);
        rightWall.transform.position = new Vector2(data.Width * 0.5f, 0);

        var floor = Instantiate(floorPrefab, gameObj.transform);
        floor.transform.position = new Vector2(0, -data.Height * 0.5f);

        var ceiling = Instantiate(floorPrefab, gameObj.transform);
        ceiling.transform.position = new Vector2(0, data.Height * 0.5f);

        room.objects.AddRange(new List<GameObject>()
            {
                leftWall,
                rightWall,
                floor,
                ceiling
            });

        if (data.DoorEast)
        {
            var obj = SetDoor(rightWall.transform.position, 90, Compass.E);
            obj.transform.parent = gameObj.transform;
            room.objects.Add(obj);
        };

        if (data.DoorWest)
        {
            var obj = SetDoor(leftWall.transform.position, 90, Compass.W);
            obj.transform.parent = gameObj.transform;
            room.objects.Add(obj);
        };
        if (data.DoorNorth)
        {
            var obj = SetDoor(ceiling.transform.position, 0, Compass.N);
            obj.transform.parent = gameObj.transform;
            room.objects.Add(obj);
        };
        if (data.DoorSouth)
        {
            var obj = SetDoor(floor.transform.position, 0, Compass.S);
            obj.transform.parent = gameObj.transform;
            room.objects.Add(obj);
        };


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

        if (current.DoorEast)
        {
            var newCoord = new Vector2Int(coord.x + 1, coord.y);
            if (!loadedRooms.Any(r => r.coordinates == newCoord))
            {
                var data = levelMap.GetRoomData(newCoord);
                if (!data) return;
                GenerateRoom(data);
            }
        };
        if (current.DoorWest)
        {
            var newCoord = new Vector2Int(coord.x - 1, coord.y);
            if (!loadedRooms.Any(r => r.coordinates == newCoord))
            {
                var data = levelMap.GetRoomData(newCoord);
                if (!data) return;
                GenerateRoom(data);
            }
        };
        if (current.DoorNorth)
        {
            var newCoord = new Vector2Int(coord.x, coord.y + 1);
            if (!loadedRooms.Any(r => r.coordinates == newCoord))
            {
                var data = levelMap.GetRoomData(newCoord);
                if (!data) return;
                GenerateRoom(data);
            }
        };
        if (current.DoorSouth)
        {
            var newCoord = new Vector2Int(coord.x, coord.y - 1);
            if (!loadedRooms.Any(r => r.coordinates == newCoord))
            {
                var data = levelMap.GetRoomData(newCoord);
                if (!data) return;
                GenerateRoom(data);
            }
        };
    }
}
