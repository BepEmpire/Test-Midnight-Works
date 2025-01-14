using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;
    
    [Header("Settings")]
    [SerializeField] private Vector3 offset;
    [SerializeField, Min(0)] private float smoothSpeed = 0.1f;

    private void LateUpdate()
    {
        SmoothFollow();   
    }

    private void SmoothFollow()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        transform.position = smoothFollow;
    }
}