using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(Scenes.GameScene.ToString());
    }

    public void OpenGarage()
    {
        SceneManager.LoadScene(Scenes.GarageScene.ToString());
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
