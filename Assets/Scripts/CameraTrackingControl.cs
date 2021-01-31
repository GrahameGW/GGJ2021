using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
public class CameraTrackingControl : MonoBehaviour
{
    public delegate void TrackingTargetSwitchDelegate(Transform newTarget);
    public event TrackingTargetSwitchDelegate OnTrackingTargetSwitched;

    [SerializeField]
    private Transform targetToTrack;

    [SerializeField]
    private Vector3 offsetFromTarget = Vector3.zero;

    [SerializeField]
    [Range(0.1f, 2.0f)]
    private float speedRatioToTarget = 1.0f;

    private Vector3 cameraVelocity = Vector2.zero;

    private Camera camera;
    private Bounds roomBounds;

    public Transform TargetToTrack
    {
        get { return targetToTrack; }
        set 
        { 
            targetToTrack = value;
            OnTrackingTargetSwitched?.Invoke(targetToTrack);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        camera = GetComponent<Camera>();

        roomBounds = RoomManager.Instance.GetBoundsOfCurrentRoom();
        RoomManager.Instance.OnRoomLoaded += UpdateRoomBounds;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float xMin = roomBounds.min.x;
        float xMax = roomBounds.max.x;
        float yMin = roomBounds.min.y;
        float yMax = roomBounds.max.y;

        Vector3 clampedTarget = new Vector3();
        clampedTarget.x = Mathf.Clamp(TargetToTrack.position.x, xMin, xMax);
        clampedTarget.y = Mathf.Clamp(TargetToTrack.position.y, yMin, yMax);
        clampedTarget.z = transform.position.z;

        Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, clampedTarget + offsetFromTarget, ref cameraVelocity, speedRatioToTarget); //Vector3.MoveTowards(transform.position, TargetToTrack.position, speed * speedRatioToTarget);

        transform.position = newCameraPosition;
    }

    private void UpdateRoomBounds()
    {
        roomBounds = RoomManager.Instance.GetBoundsOfCurrentRoom();
    }
}
