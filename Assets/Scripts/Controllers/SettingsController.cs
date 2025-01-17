using UnityEngine;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;

    public void SaveName()
    {
        string playerName = nameInputField.text;
        PlayerPrefs.SetString(Keys.PLAYER_NAME, playerName);
        Debug.Log("Player name saved: " + playerName);
    }
}