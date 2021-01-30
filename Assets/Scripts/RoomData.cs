using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room")]
public class RoomData : ScriptableObject
{
    public Vector2Int coordinates;
    public float DistanceLoadingFromDoor = 2f;

    public Vector2 WestStart;
    public Vector2 EastStart;
    public Vector2 NorthStart;
    public Vector2 SouthStart;

    public List<ItemData> items;

    public Grid TilemapPrefab;
}
