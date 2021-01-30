using UnityEngine;

public class RoomEntryPoint : MonoBehaviour
{
    public Compass arrivalDirection;

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
