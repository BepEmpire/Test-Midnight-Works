using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExchangeManager : MonoBehaviour
{
    [Header("Exchange Rates")]
    [SerializeField] private int[] coinAmounts = { 10000, 50000, 100000 };
    [SerializeField] private int[] dollarCosts = { 1000, 5000, 10000 };

    [Header("UI Elements")]
    [SerializeField] private Button coinsPotButton1;
    [SerializeField] private Button coinsPotButton2;
    [SerializeField] private Button coinsPotButton3;
    [SerializeField] private TextMeshProUGUI feedbackText;

    private const string INIT_TEXT = "Let's exchange some money.";
    
    private void Start()
    {
        SetInitText();
        
        coinsPotButton1.onClick.AddListener(() => Exchange(0));
        coinsPotButton2.onClick.AddListener(() => Exchange(1));
        coinsPotButton3.onClick.AddListener(() => Exchange(2));
    }

    public void SetInitText()
    {
        feedbackText.text = INIT_TEXT;
    }

    private void Exchange(int index)
    {
        if (WalletManager.Instance.SpendDollars(dollarCosts[index]))
        {
            WalletManager.Instance.AddCoins(coinAmounts[index]);
            UpdateFeedback($"Exchanged ${dollarCosts[index]} for {coinAmounts[index]} coins.");
        }
        else
        {
            UpdateFeedback("Not enough dollars to complete this exchange.");
        }
    }

    private void UpdateFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
        }
    }
}
