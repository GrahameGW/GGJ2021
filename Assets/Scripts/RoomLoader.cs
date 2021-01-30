using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    public static RoomLoader Instance { get; private set; }
    public LevelMap levelMap;

    public Room room;
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject doorTriggerPrefab;

    List<GameObject> roomObjects = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Load(new Vector2Int(0,0));
    }

    public void Load(Vector2Int coordinates)
    {
        room = levelMap.GetRoom(coordinates.x, coordinates.y);
        room.coordinates = coordinates; // set value so it can be found later when changing rooms

        Unload();

        var leftWall = Instantiate(wallPrefab);
        leftWall.transform.position = new Vector2(-room.Width * 0.5f, 0);

        var rightWall = Instantiate(wallPrefab);
        rightWall.transform.position = new Vector2(room.Width * 0.5f, 0);

        var floor = Instantiate(floorPrefab);
        floor.transform.position = new Vector2(0, -room.Height * 0.5f);

        var ceiling = Instantiate(floorPrefab);
        ceiling.transform.position = new Vector2(0, room.Height * 0.5f);


        roomObjects = new List<GameObject>()
        {
            leftWall,
            rightWall,
            floor,
            ceiling
        };

        if (room.DoorEast)
        {
            var obj = SetDoor(rightWall.transform.position, 90);
            roomObjects.Add(obj);
        };

        if (room.DoorWest)
        {
            var obj = SetDoor(leftWall.transform.position, 90);
            roomObjects.Add(obj);
        };
        if (room.DoorNorth)
        {
            var obj = SetDoor(ceiling.transform.position, 0);
            roomObjects.Add(obj);
        };
        if (room.DoorSouth)
        {
            var obj = SetDoor(floor.transform.position, 0);
            roomObjects.Add(obj);
        };

    }

    private GameObject SetDoor(Vector2 pos, float rotation)
    {
        var obj = Instantiate(doorTriggerPrefab);
        obj.transform.position = pos;
        obj.transform.eulerAngles = new Vector3(0, 0, rotation);

        return obj;
    }

    [ContextMenu("Unload Room")]
    private void Unload()
    {
        for (int i = roomObjects.Count - 1; i >= 0; i--)
        {
            var obj = roomObjects[i];
            Destroy(obj);
            roomObjects.RemoveAt(i);
        }
    }

}
