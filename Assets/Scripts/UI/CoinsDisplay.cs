using UnityEngine;
using TMPro;

public class CoinsDisplay : MonoBehaviour
{
    private TextMeshProUGUI _coinsText;

    private void Awake()
    {
        _coinsText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetText(WalletManager.Instance.TotalCoins);
        WalletManager.Instance.OnWalletChanged.AddListener(SetText);
    }

    private void OnDestroy()
    {
        WalletManager.Instance.OnWalletChanged.RemoveListener(SetText);
    }

    private void SetText(int coins)
    {
        _coinsText.text = coins.ToString();
    }
}
