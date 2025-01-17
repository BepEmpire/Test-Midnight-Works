using UnityEngine;
using UnityEngine.SceneManagement;

public class CarPreviewManager : MonoBehaviour
{
   [Header("Car Configuration")]
    [SerializeField] private GameObject[] carPrefabs;
    [SerializeField] private Material[] mainMaterials;
    [SerializeField] private Material[] wheelMaterials;

    [Header("Preview Settings")]
    [SerializeField] private Transform carPreviewArea;
    private GameObject _currentCar;

    [Header("Player Data")]
    public int selectedCarIndex = 0;
    public int selectedMainMaterialIndex = 0;
    public int selectedWheelMaterialIndex = 0;

    private void Start()
    {
        LoadCustomization();
        LoadCarPreview();
    }
    
    public void LoadCarPreview()
    {
        if (_currentCar != null)
        {
            Destroy(_currentCar);
        }
        
        _currentCar = Instantiate(carPrefabs[selectedCarIndex], carPreviewArea.position, carPreviewArea.rotation, carPreviewArea);
        
        ApplyMainMaterial(selectedMainMaterialIndex);
        ApplyWheelMaterial(selectedWheelMaterialIndex);
    }
    
    public void ApplyMainMaterial(int materialIndex)
    {
        if (materialIndex < 0 || materialIndex >= mainMaterials.Length) return;

        Renderer carRenderer = _currentCar.GetComponentInChildren<Renderer>();
        if (carRenderer != null)
        {
            carRenderer.material = mainMaterials[materialIndex];
        }

        selectedMainMaterialIndex = materialIndex;
        SaveCustomization();
    }
    
    public void ApplyWheelMaterial(int materialIndex)
    {
        if (materialIndex < 0 || materialIndex >= wheelMaterials.Length) return;

        Renderer[] wheelRenderers = _currentCar.GetComponentsInChildren<Renderer>();
        foreach (Renderer wheelRenderer in wheelRenderers)
        {
            if (wheelRenderer.CompareTag("Wheel"))
            {
                wheelRenderer.material = wheelMaterials[materialIndex];
            }
        }

        selectedWheelMaterialIndex = materialIndex;
        SaveCustomization();
    }

    public void SaveCustomization()
    {
        PlayerPrefs.SetInt(Keys.SELECTED_CAR, selectedCarIndex);
        PlayerPrefs.SetInt(Keys.SELECTED_MAIN_MATERIAL, selectedMainMaterialIndex);
        PlayerPrefs.SetInt(Keys.SELECTED_WHEEL_MATERIAL, selectedWheelMaterialIndex);
        PlayerPrefs.Save();
    }
    
    public void LoadCustomization()
    {
        selectedCarIndex = PlayerPrefs.GetInt(Keys.SELECTED_CAR, 0);
        selectedMainMaterialIndex = PlayerPrefs.GetInt(Keys.SELECTED_MAIN_MATERIAL, 0);
        selectedWheelMaterialIndex = PlayerPrefs.GetInt(Keys.SELECTED_WHEEL_MATERIAL, 0);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scenes.MenuScene.ToString());
    }
}
