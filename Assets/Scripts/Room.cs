using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Container class for all game objects comprising a room
    // Allows for easy pooling

    public Vector2Int coordinates;
    public List<GameObject> objects = new List<GameObject>();
    public RoomEntryPoint[] entryPoints;
    public RoomData data;
    private bool itemSpawned = false;
    private AudioSource audio;


    private void Awake()
    {
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = data.Ambience;
    }

    private void Start()
    {
        audio.Play();
    }

    private void OnDisable()
    {
        audio.Stop();
    }

    public void SpawnItems()
    {
        if (data.items == null) return;

        for (int i = 0; i < data.items.Count; i++)
        {
            if (itemSpawned) return;

            if (Random.Range(0, 2) == 1)
            {
                var tuple = data.items[i]; // Tuple<ItemData, Vector2>
                var obj = Instantiate(tuple.Item1.storedPrefab, transform);
                obj.transform.position = tuple.Item2;
                itemSpawned = true;
            }
        }
    }
}
