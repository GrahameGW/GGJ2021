﻿using UnityEngine;

[CreateAssetMenu(menuName = "Room")]
public class Room : ScriptableObject
{
    public Vector2Int coordinates;

    public float Width;
    public float Height;

    public bool DoorEast;
    public bool DoorWest;
    public bool DoorNorth;
    public bool DoorSouth;
}
