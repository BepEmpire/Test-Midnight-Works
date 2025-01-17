using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartLevel1()
    {
        SceneManager.LoadScene(Scenes.Level1.ToString());
    }
    
    public void StartLevel2()
    {
        SceneManager.LoadScene(Scenes.Level2.ToString());
    }
    
    public void StartMultiplayerGame()
    {
        SceneManager.LoadScene(Scenes.Lobby.ToString());
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