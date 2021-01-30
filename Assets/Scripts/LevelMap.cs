using UnityEngine;

[CreateAssetMenu(menuName = "Level Map")]
public class LevelMap : ScriptableObject
{
    public LevelMapRow[] rows;

    public RoomData GetRoomData(Vector2Int coord)
    {
        if (coord.y < rows.Length)
        {
            var rooms = rows[coord.y].rooms;

            if (coord.x < rooms.Length)
            {
                var room = rooms[coord.x];
                room.coordinates = coord;

                return room;
            }

        }

        Debug.LogWarning($"Tried to access non-existent room at ({coord.x},{coord.y})");
        return null;
    }
}

[System.Serializable]
public class LevelMapRow {
    public int Length => rooms.Length;
    public RoomData[] rooms;
}

