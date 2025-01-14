using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageManager : MonoBehaviour
{
    public void SaveAndReturnToMainMenu()
    {
        Debug.Log("SaveAndReturnToMainMenu called.");
        if (DataManager.Instance != null)
        {
            DataManager.Instance.SaveSelection(0, 0, 0);
        }
        SceneManager.LoadScene(Scenes.MenuScene.ToString());
    }
}