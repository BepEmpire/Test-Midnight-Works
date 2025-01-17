using UnityEngine;

public class CarPreviewManager : MonoBehaviour
{
    [Header("Car Configuration")]
    [SerializeField] private GameObject[] carPrefabs;
    [SerializeField] private Material[] mainMaterials;
    [SerializeField] private Material[] wheelMaterials;

    [Header("Preview Settings")]
    [SerializeField] private Transform carPreviewArea;

    private int _selectedCarIndex = 0;
    private int _selectedMainMaterialIndex = 0;
    private int _selectedWheelMaterialIndex = 0;
    
    private GameObject _currentCar;

    private void Start()
    {
        LoadCustomization();
        LoadCarPreview();
    }
    
    public void ApplyMainMaterial(int materialIndex)
    {
        if (materialIndex < 0 || materialIndex >= mainMaterials.Length) return;

        Renderer carRenderer = _currentCar.GetComponentInChildren<Renderer>();
        
        if (carRenderer != null)
        {
            carRenderer.material = mainMaterials[materialIndex];
        }

        _selectedMainMaterialIndex = materialIndex;
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

        _selectedWheelMaterialIndex = materialIndex;
        SaveCustomization();
    }

    public void SaveCustomization()
    {
        PlayerPrefs.SetInt(Keys.SELECTED_CAR, _selectedCarIndex);
        PlayerPrefs.SetInt(Keys.SELECTED_MAIN_MATERIAL, _selectedMainMaterialIndex);
        PlayerPrefs.SetInt(Keys.SELECTED_WHEEL_MATERIAL, _selectedWheelMaterialIndex);
        PlayerPrefs.Save();
    }
    
    private void LoadCustomization()
    {
        _selectedCarIndex = PlayerPrefs.GetInt(Keys.SELECTED_CAR, 0);
        _selectedMainMaterialIndex = PlayerPrefs.GetInt(Keys.SELECTED_MAIN_MATERIAL, 0);
        _selectedWheelMaterialIndex = PlayerPrefs.GetInt(Keys.SELECTED_WHEEL_MATERIAL, 0);
    }
    
    private void LoadCarPreview()
    {
        if (_currentCar != null)
        {
            Destroy(_currentCar);
        }
        
        _currentCar = Instantiate(carPrefabs[_selectedCarIndex], carPreviewArea.position, carPreviewArea.rotation, carPreviewArea);
        
        ApplyMainMaterial(_selectedMainMaterialIndex);
        ApplyWheelMaterial(_selectedWheelMaterialIndex);
    }
}