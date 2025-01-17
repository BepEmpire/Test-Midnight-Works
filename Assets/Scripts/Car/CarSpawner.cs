using UnityEngine;
using EasyJoystick;
using Photon.Pun;

public class CarSpawner : MonoBehaviour
{
    [Header("Car Configuration")]
    [SerializeField] private GameObject[] carPrefabs;
    [SerializeField] private Material[] bodyMaterials;
    [SerializeField] private Material[] wheelMaterials;

    [Header("Spawn Point")]
    [SerializeField] private Transform[] carSpawnPoints;

    [Header("Joystick and Camera Connection")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private CameraFollow cameraFollow;

    [Header("Photon Settings")]
    [SerializeField] private bool isPhotonUsed;
    
    private GameObject _currentCar;

    private void Start()
    {
        LoadSelectedCar();
    }
    
    public void DisableCurrentCarControl()
    {
        if (_currentCar != null)
        {
            CarController carController = _currentCar.GetComponent<CarController>();
            
            if (carController != null)
            {
                carController.DisableControl();
            }
        }
    }

    private void LoadSelectedCar()
    {
        int selectedChildCarIndex = PlayerPrefs.GetInt(Keys.SELECTED_CAR, 0);
        int bodyMaterialIndex = PlayerPrefs.GetInt(Keys.SELECTED_MAIN_MATERIAL, 0);
        int wheelMaterialIndex = PlayerPrefs.GetInt(Keys.SELECTED_WHEEL_MATERIAL, 0);

        if (isPhotonUsed)
        {
            _currentCar = PhotonNetwork.Instantiate(carPrefabs[selectedChildCarIndex].name, GetRandomPosition(), Quaternion.identity);
        }
        else
        {
            _currentCar = Instantiate(carPrefabs[selectedChildCarIndex], GetRandomPosition(), Quaternion.identity);
        }

        ApplyBodyMaterial(_currentCar, bodyMaterialIndex);
        ApplyWheelMaterial(_currentCar, wheelMaterialIndex);

        InitializeCarControllers(_currentCar);
    }

    private void ApplyBodyMaterial(GameObject car, int materialIndex)
    {
        if (materialIndex < 0 || materialIndex >= bodyMaterials.Length) return;

        Renderer carRenderer = car.GetComponentInChildren<Renderer>();
        
        if (carRenderer != null)
        {
            carRenderer.material = bodyMaterials[materialIndex];
        }
    }

    private void ApplyWheelMaterial(GameObject car, int materialIndex)
    {
        if (materialIndex < 0 || materialIndex >= wheelMaterials.Length) return;

        Renderer[] wheelRenderers = car.GetComponentsInChildren<Renderer>();
        
        foreach (Renderer renderer in wheelRenderers)
        {
            if (renderer.CompareTag("Wheel"))
            {
                renderer.material = wheelMaterials[materialIndex];
            }
        }
    }

    private void InitializeCarControllers(GameObject car)
    {
        cameraFollow.SetTarget(_currentCar.transform);
        
        var carController = car.GetComponent<CarController>();
        
        if (carController != null)
        {
            carController.InitializeController(joystick);

            if (isPhotonUsed)
            {
                carController.ActivateNicknameText();
            }
        }

        var wheelController = car.GetComponent<WheelController>();
        
        if (wheelController != null)
        {
            wheelController.InitializeController(joystick);
        }
    }
    
    private Vector3 GetRandomPosition()
    {
        return carSpawnPoints[Random.Range(0, carSpawnPoints.Length)].position;
    }
}