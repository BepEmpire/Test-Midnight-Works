using UnityEngine;
using EasyJoystick;

public class WheelController : MonoBehaviour
{
    [Header("Wheels Transform")]
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform rearLeftTransform;
    [SerializeField] private Transform rearRightTransform;

    [Header("Wheels Colliders")]
    [SerializeField] private WheelCollider frontLeftWheel;
    [SerializeField] private WheelCollider frontRightWheel;
    [SerializeField] private WheelCollider rearLeftWheel;
    [SerializeField] private WheelCollider rearRightWheel;

    [Header("Settings")]
    [SerializeField] private float motorForce = 50;
    [SerializeField] private float maxSteerAngle = 30;

    private Joystick joystick;
    
    private float _verticalInput;
    private float _horizontalInput;

    private float _steeringAngle;
    private float _rotationAngle;
    
    public void InitializeController(Joystick assignedJoystick)
    {
        joystick = assignedJoystick;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(joystick.Vertical()) > 0.05f || Mathf.Abs(joystick.Horizontal()) > 0.05f)
        {
            Input();
            Steer();
            Accelerate();
            UpdateWheelPosition();
        }

        if (Mathf.Abs(joystick.Vertical()) > 0.1f)
        {
            _rotationAngle += 1000 * joystick.Vertical() * Time.deltaTime;

            frontLeftTransform.Rotate(_rotationAngle, 0.0f, 0.0f);
            frontRightTransform.Rotate(_rotationAngle, 0.0f, 0.0f);
            rearLeftTransform.Rotate(_rotationAngle, 0.0f, 0.0f);
            rearRightTransform.Rotate(_rotationAngle, 0.0f, 0.0f);
        }
    }

    private void Input()
    {
        _horizontalInput = joystick.Horizontal();
        _verticalInput = joystick.Vertical();
    }

    private void Steer()
    {
        _steeringAngle = maxSteerAngle * _horizontalInput;
        frontLeftWheel.steerAngle = _steeringAngle;
        frontRightWheel.steerAngle = _steeringAngle;
    }

    private void Accelerate()
    {
        rearLeftWheel.motorTorque = _verticalInput * motorForce;
        rearRightWheel.motorTorque = _verticalInput * motorForce;
    }

    private void UpdateWheelPosition()
    {
        UpdateWheelPose(frontLeftWheel, frontLeftTransform);
        UpdateWheelPose(frontRightWheel, frontRightTransform);
        UpdateWheelPose(rearLeftWheel, rearLeftTransform);
        UpdateWheelPose(rearRightWheel, rearRightTransform);
    }

    private void UpdateWheelPose(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position = wheelTransform.position;
        Quaternion rotation = wheelTransform.rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}