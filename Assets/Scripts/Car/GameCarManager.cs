using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCarManager : MonoBehaviour
{
    [Header("Car Setup")]
    public GameObject[] carPrefabs;
    public Material[] mainMaterials;
    public Material[] wheelMaterials;
    public Transform carSpawnPoint;

    private GameObject currentCar;

    private void Start()
    {
        LoadCar();
    }

    private void LoadCar()
    {
        int carIndex = PlayerPrefs.GetInt("SelectedCar", 0);
        int mainMaterialIndex = PlayerPrefs.GetInt("SelectedMainMaterial", 0);
        int wheelMaterialIndex = PlayerPrefs.GetInt("SelectedWheelMaterial", 0);
        
        if (carIndex < carPrefabs.Length)
        {
            currentCar = Instantiate(carPrefabs[carIndex], carSpawnPoint.position, carSpawnPoint.rotation);
        }
        
        if (mainMaterialIndex < mainMaterials.Length)
        {
            Renderer carRenderer = currentCar.GetComponentInChildren<Renderer>();
            if (carRenderer != null)
            {
                carRenderer.material = mainMaterials[mainMaterialIndex];
            }
        }
        
        if (wheelMaterialIndex < wheelMaterials.Length)
        {
            Renderer[] wheelRenderers = currentCar.GetComponentsInChildren<Renderer>();
            foreach (Renderer wheelRenderer in wheelRenderers)
            {
                if (wheelRenderer.CompareTag("Wheel"))
                {
                    wheelRenderer.material = wheelMaterials[wheelMaterialIndex];
                }
            }
        }
    }
}
