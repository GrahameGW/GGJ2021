using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public RoomLoader loader { private get; set; }
    public Compass Direction { private get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            loader.MoveRooms(Direction);
        }
    }
}
