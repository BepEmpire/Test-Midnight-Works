using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverPopup : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinsText;

    private int _finalScore;
    private int _coinsEarned;
    
    public void ShowPopup(int finalScore)
    {
        _finalScore = finalScore;
        _coinsEarned = finalScore / 100;

        scoreText.text = $"Score: {_finalScore}";
        coinsText.text = $"Coins: {_coinsEarned}";

        gameObject.SetActive(true);
        
        WalletManager.Instance.AddCoins(_coinsEarned);
    }
    
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(Scenes.MenuScene.ToString());
    }
}
