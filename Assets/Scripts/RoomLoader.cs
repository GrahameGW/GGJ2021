using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    public static RoomLoader Instance { get; private set; }
    public Room room;
    public GameObject wallPrefab;
    public GameObject floorPrefab;

    List<GameObject> roomObjects = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Load();
    }

    [ContextMenu("Load Room")]
    public void Load()
    {
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
