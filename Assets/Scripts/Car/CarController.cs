using UnityEngine;
using TMPro;
using EasyJoystick;
using Photon.Pun;

public class CarController : MonoBehaviour
{
    [SerializeField] private GameObject nicknameHolder;
    [SerializeField] private TextMeshPro nickNameText;
    
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 50;
    [SerializeField] private float maxSpeed = 15;
    [SerializeField] private float acceleration = 0.98f;
    [SerializeField] private float steerAngle = 20;
    [SerializeField] private float interpolate = 0.1f;

    [Header("Drift")] 
    [SerializeField] private int driftIncreaser = 1;
    [SerializeField] private float minVerticalDriftResponse = 0.1f;
    [SerializeField] private float minHorizontalDriftResponse = 0.2f;

    private Vector3 _currentMoveForce;
    private Vector3 _targetPosition;

    private Joystick joystick;
    private PhotonView _photonView;
    private int _currentDriftScore;
    private bool _isControlEnabled = true;
    
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }
    
    public void ActivateNicknameText()
    {
        nicknameHolder.SetActive(true);
        nickNameText.text = _photonView.Controller.NickName;
    }
    
    public void InitializeController(Joystick assignedJoystick)
    {
        joystick = assignedJoystick;
    }

    private void Update()
    {
        if (_isControlEnabled || _photonView.IsMine)
        {
            UpdateMoveForce();
            Steer();
            InterpolatePosition();
            UpdateDriftScore();
        }
    }

    private void FixedUpdate()
    {
        if (_isControlEnabled || _photonView.IsMine)
        {
            Accelerate();
            UpdateTargetPosition();
        }
    }

    private void UpdateMoveForce()
    {
        _currentMoveForce += transform.forward * (moveSpeed * joystick.Vertical() * Time.fixedDeltaTime);
    }

    private void Steer()
    {
        float steerInput = joystick.Horizontal();
        transform.Rotate(Vector3.up * (steerInput * _currentMoveForce.magnitude * steerAngle * Time.deltaTime));
    }

    private void InterpolatePosition()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, interpolate);
    }

    private void Accelerate()
    {
        _currentMoveForce *= acceleration;
        _currentMoveForce = Vector3.ClampMagnitude(_currentMoveForce, maxSpeed);
    }

    private void UpdateTargetPosition()
    {
        _targetPosition = transform.position + _currentMoveForce * Time.fixedDeltaTime;
    }

    private void UpdateDriftScore()
    {
        if (Mathf.Abs(joystick.Vertical()) > minVerticalDriftResponse && Mathf.Abs(joystick.Horizontal()) > minHorizontalDriftResponse)
        {
            _currentDriftScore += driftIncreaser;
            Debug.Log($"Drift Score: {_currentDriftScore}");
        }
    }

    public void DisableControl()
    {
        _isControlEnabled = false;
    }
    
    public int GetDriftScore()
    {
        return _currentDriftScore;
    }
}
