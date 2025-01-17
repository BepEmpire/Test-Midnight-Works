using UnityEngine;
using TMPro;
using EasyJoystick;
using Photon.Pun;

public class CarController : MonoBehaviour
{
    [Header("Nickname")]
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
    
    [Header("Bounds")] 
    [SerializeField] private Vector2 xBounds = new Vector2(-30f, 30f);
    [SerializeField] private Vector2 zBounds = new Vector2(-20f, 20f);
    [SerializeField] private float yPosition = 7.9f;

    private Joystick _joystick;
    private PhotonView _photonView;
    
    private Vector3 _currentMoveForce;
    private Vector3 _targetPosition;

    private int _currentDriftScore;
    
    private bool _isControlEnabled = true;
    private bool _isMultiplayer;
    private bool _hasInitializedPosition = false;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        _isMultiplayer = PhotonNetwork.InRoom;
        
        _targetPosition = new Vector3(transform.position.x, yPosition, transform.position.z);
        transform.position = _targetPosition;

        _hasInitializedPosition = true;
    }

    public void ActivateNicknameText()
    {
        nicknameHolder.SetActive(true);
        nickNameText.text = _photonView.Controller.NickName;
    }
    
    public void InitializeController(Joystick assignedJoystick)
    {
        _joystick = assignedJoystick;
    }
    
    public void DisableControl()
    {
        _isControlEnabled = false;
    }
    
    public int GetDriftScore()
    {
        return _currentDriftScore;
    }

    private void Update()
    {
        if (_isControlEnabled && (!_isMultiplayer || (_isMultiplayer && _photonView.IsMine)))
        {
            UpdateMoveForce();
            Steer();
            InterpolatePosition();
            UpdateDriftScore();
        }
    }

    private void FixedUpdate()
    {
        if (_isControlEnabled && (!_isMultiplayer || (_isMultiplayer && _photonView.IsMine)))
        {
            Accelerate();
            UpdateTargetPosition();
        }
    }

    private void UpdateMoveForce()
    {
        _currentMoveForce += transform.forward * (moveSpeed * _joystick.Vertical() * Time.fixedDeltaTime);
    }

    private void Steer()
    {
        float steerInput = _joystick.Horizontal();
        transform.Rotate(Vector3.up * (steerInput * _currentMoveForce.magnitude * steerAngle * Time.deltaTime));
    }

    private void InterpolatePosition()
    {
        if (_hasInitializedPosition)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, interpolate);
        }
    }

    private void Accelerate()
    {
        _currentMoveForce *= acceleration;
        _currentMoveForce = Vector3.ClampMagnitude(_currentMoveForce, maxSpeed);
    }

    private void UpdateTargetPosition()
    {
        _targetPosition = transform.position + _currentMoveForce * Time.fixedDeltaTime;
        
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, xBounds.x, xBounds.y);
        _targetPosition.z = Mathf.Clamp(_targetPosition.z, zBounds.x, zBounds.y);
    }

    private void UpdateDriftScore()
    {
        if (Mathf.Abs(_joystick.Vertical()) > minVerticalDriftResponse && Mathf.Abs(_joystick.Horizontal()) > minHorizontalDriftResponse)
        {
            _currentDriftScore += driftIncreaser;
            Debug.Log($"Drift Score: {_currentDriftScore}");
        }
    }
}