using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Compass Direction { private get; set; }

    public UnityIntEvent OnDoorEntered;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.TryGetComponent(out Player player))
        {
            OnDoorEntered.Invoke((int)Direction);
            OnDoorEntered.RemoveAllListeners();
        }
    }
}
