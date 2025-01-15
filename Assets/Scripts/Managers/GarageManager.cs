using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageManager : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(Scenes.MenuScene.ToString());
    }
}