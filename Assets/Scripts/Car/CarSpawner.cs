using UnityEngine;
using EasyJoystick;

public class CarSpawner : MonoBehaviour
{
    [Header("Car Configuration")]
    [SerializeField] private GameObject[] carPrefabs;
    [SerializeField] private Material[] bodyMaterials;
    [SerializeField] private Material[] wheelMaterials;

    [Header("Spawn Point")]
    [SerializeField] private Transform carSpawnPoint;

    [Header("Joystick and Camera Connection")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private CameraFollow cameraFollow;

    private GameObject _currentCar;
    
    public GameObject CurrentCar => _currentCar;

    private void Start()
    {
        LoadSelectedCar();
    }

    private void LoadSelectedCar()
    {
        int selectedChildCarIndex = PlayerPrefs.GetInt(Keys.SELECTED_CAR, 0);
        int bodyMaterialIndex = PlayerPrefs.GetInt(Keys.SELECTED_MAIN_MATERIAL, 0);
        int wheelMaterialIndex = PlayerPrefs.GetInt(Keys.SELECTED_WHEEL_MATERIAL, 0);

        _currentCar = Instantiate(carPrefabs[selectedChildCarIndex], transform);
        _currentCar.transform.localPosition = Vector3.zero;

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
        }

        var wheelController = car.GetComponent<WheelController>();
        if (wheelController != null)
        {
            wheelController.InitializeController(joystick);
        }
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
}