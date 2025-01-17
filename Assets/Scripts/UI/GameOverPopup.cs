using Photon.Pun;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverPopup : MonoBehaviour
{
    [Header("UI References")] [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private bool isPhotonUsed;

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

    public void DoubleReward()
    {
        scoreText.text = $"Score: {_finalScore * 2}";
        coinsText.text = $"Coins: {_coinsEarned * 2}";

        WalletManager.Instance.AddCoins(_coinsEarned);
    }

    public void ExitToMainMenu()
    {
        if (isPhotonUsed)
        {
            PhotonNetwork.Disconnect();
        }

        SceneManager.LoadScene(Scenes.MenuScene.ToString());
    }
}