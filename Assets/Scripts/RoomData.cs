using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Room")]
public class RoomData : ScriptableObject
{
    public Vector2Int coordinates;

    public float Width;
    public float Height;

    public bool DoorEast;
    public Vector2 EastStart = new Vector2(6.75f, 0);

    public bool DoorWest;
    public Vector2 WestStart = new Vector2(-6.75f, 0);

    public bool DoorNorth;
    public Vector2 NorthStart = new Vector2(0, 3.5f);

    public bool DoorSouth;
    public Vector2 SouthStart = new Vector2(0, -3.5f);

    public List<ItemData> items;

    public Grid TilemapPrefab;
}
