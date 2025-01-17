using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private Vector3 offset;
    [SerializeField, Min(0)] private float smoothSpeed = 0.1f;
    
    private Transform _target;

    private void LateUpdate()
    {
        SmoothFollow();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void SmoothFollow()
    {
        if (_target != null)
        {
            Vector3 targetPos = _target.position + offset;
            Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

            transform.position = smoothFollow;
        }
    }
}