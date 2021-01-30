using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Compass Direction;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            RoomManager.Instance.MoveRooms(Direction);
        }
    }
}
