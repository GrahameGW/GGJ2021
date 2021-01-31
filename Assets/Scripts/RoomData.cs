using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Room")]
public class RoomData : ScriptableObject
{
    public Vector2Int coordinates;

    public List<Tuple<ItemData, Vector2>> items;

    public Grid TilemapPrefab;
}
