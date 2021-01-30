using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Container class for all game objects comprising a room
    // Allows for easy pooling

    public Vector2Int coordinates;
    public List<GameObject> objects = new List<GameObject>();
    public RoomData data;
}
