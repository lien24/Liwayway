using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target the camera will follow
    public float smoothSpeed = 0.125f; // The speed with which the camera will follow
    public Vector3 offset; // The offset at which the camera will follow the target

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
