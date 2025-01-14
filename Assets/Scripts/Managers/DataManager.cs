using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [Header("Car Selection")]
    public int selectedCarIndex = 0;
    public int selectedBodyMaterialIndex = 0;
    public int selectedWheelMaterialIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("DataManager is created");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveSelection(int carIndex, int bodyMaterialIndex, int wheelMaterialIndex)
    {
        selectedCarIndex = carIndex;
        selectedBodyMaterialIndex = bodyMaterialIndex;
        selectedWheelMaterialIndex = wheelMaterialIndex;
    }
}
