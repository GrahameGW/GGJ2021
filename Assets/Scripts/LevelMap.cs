using UnityEngine;

[CreateAssetMenu(menuName = "Level Map")]
public class LevelMap : ScriptableObject
{
    public LevelMapRow[] rows;

    public Room GetRoom(int x, int y)
    {
        if (y < rows.Length)
        {
            var rooms = rows[y].rooms;

            if (x < rooms.Length) 
                return rooms[x];
        }

        Debug.LogWarning($"Tried to access non-existent room at ({x},{y})");
        return null;
    }
}

[System.Serializable]
public class LevelMapRow {
    public int Length => rooms.Length;
    public Room[] rooms;
}

