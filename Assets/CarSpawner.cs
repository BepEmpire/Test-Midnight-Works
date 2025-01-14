using UnityEngine;
using EasyJoystick;

public class CarSpawner : MonoBehaviour
{
    [Header("Car Configuration")]
    public GameObject carPrefab; // The main car prefab (acts as the parent object)
    public GameObject[] childCarPrefabs; // Array of child car prefabs (e.g., Muscle, Police)
    public Material[] bodyMaterials; // Array of car body materials
    public Material[] wheelMaterials; // Array of wheel materials

    [Header("Spawn Point")]
    public Transform carSpawnPoint; // The point where the car is spawned as a child

    [Header("Joystick Connection")]
    public Joystick joystick; // Joystick reference for car control

    private GameObject currentCar;

    private void Start()
    {
        LoadSelectedCar();
    }

private void LoadSelectedCar()
{
    if (DataManager.Instance != null)
    {
        Debug.Log("Loading selected car...");

        int selectedChildCarIndex = DataManager.Instance.selectedCarIndex;
        int bodyMaterialIndex = DataManager.Instance.selectedBodyMaterialIndex;
        int wheelMaterialIndex = DataManager.Instance.selectedWheelMaterialIndex;

        // If there's an existing car, destroy it
        if (currentCar != null)
        {
            Destroy(currentCar);
        }

        // Instantiate the main car prefab
        currentCar = Instantiate(carPrefab, carSpawnPoint.position, carSpawnPoint.rotation, transform);

        if (currentCar == null)
        {
            Debug.LogError("CarPrefab instantiation failed!");
            return;
        }

        Debug.Log("CarPrefab instantiated successfully.");

        // Replace the child car
        if (selectedChildCarIndex >= 0 && selectedChildCarIndex < childCarPrefabs.Length)
        {
            Transform childCarParent = currentCar.transform.Find("ChildCar");
            if (childCarParent != null)
            {
                foreach (Transform child in childCarParent)
                {
                    Destroy(child.gameObject);
                }

                GameObject selectedChildCar = Instantiate(childCarPrefabs[selectedChildCarIndex], childCarParent);
                selectedChildCar.transform.localPosition = Vector3.zero;
                selectedChildCar.transform.localRotation = Quaternion.identity;

                Debug.Log($"Child car instantiated: {childCarPrefabs[selectedChildCarIndex].name}");
            }
            else
            {
                Debug.LogError("ChildCar placeholder not found in CarPrefab!");
            }
        }

        // Apply materials
        ApplyBodyMaterial(currentCar, bodyMaterialIndex);
        ApplyWheelMaterial(currentCar, wheelMaterialIndex);

        // Initialize car controllers
        InitializeCarControllers(currentCar);
    }
    else
    {
        Debug.LogError("DataManager.Instance is null. Cannot load selected car.");
    }
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
            if (renderer.CompareTag("Wheel")) // Ensure wheels have a "Wheel" tag
            {
                renderer.material = wheelMaterials[materialIndex];
            }
        }
    }

    private void InitializeCarControllers(GameObject car)
    {
        var carController = car.GetComponent<CarController>();
        if (carController != null)
        {
            carController.InitializeController(joystick); // Pass joystick reference to car controller
        }

        var wheelController = car.GetComponent<WheelController>();
        if (wheelController != null)
        {
            wheelController.InitializeController(joystick); // Pass joystick reference to wheel controller
        }
    }
}
