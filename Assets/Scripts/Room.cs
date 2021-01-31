using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Container class for all game objects comprising a room
    // Allows for easy pooling

    public Vector2Int coordinates;
    public List<GameObject> objects = new List<GameObject>();
    public RoomEntryPoint[] entryPoints;
    private RoomData data;
    private bool itemSpawned = false;
    private AudioSource audio;

    public RoomData Data
    {
        get { return data; }
        set
        {
            data = value;

            if (audio == null)
            {
                audio = gameObject.AddComponent<AudioSource>();
            }

            audio.clip = data.Ambience;
            
            if (audio.clip != null)
            {
                audio.Play();
            }
        }
    }

    private void OnDisable()
    {
        if (audio != null && audio.isPlaying)
        {
            audio.Stop();
        }
    }

    public void SpawnItems()
    {
        if (Data.items == null) return;

        for (int i = 0; i < Data.items.Count; i++)
        {
            if (itemSpawned) return;

            if (Random.Range(0, 2) == 1)
            {
                var tuple = Data.items[i]; // Tuple<ItemData, Vector2>
                var obj = Instantiate(tuple.Item1.storedPrefab, transform);
                obj.transform.position = tuple.Item2;
                itemSpawned = true;
            }
        }
    }
}
