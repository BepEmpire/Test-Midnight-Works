using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class WalletManager : MonoBehaviour
{
    public static WalletManager Instance;

    public UnityEvent<int> OnCoinsChanged;
    public UnityEvent<int> OnDollarsChanged;

    private const string COINS_KEY = "TotalCoins";
    private const string DOLLARS_KEY = "TotalDollars";
    public int TotalCoins { get; private set; }
    public int TotalDollars { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        TotalCoins = PlayerPrefs.GetInt(COINS_KEY, 0);
        TotalDollars = PlayerPrefs.GetInt(DOLLARS_KEY, 0);
    }

    public void AddCoins(int coins)
    {
        TotalCoins += coins;
        SaveCoins();
        OnCoinsChanged?.Invoke(TotalCoins);
    }
    
    public void AddDollars(int dollars)
    {
        TotalDollars += dollars;
        SaveDollars();
        OnDollarsChanged?.Invoke(TotalDollars);
    }

    public bool SpendCoins(int amount)
    {
        if (TotalCoins >= amount)
        {
            TotalCoins -= amount;
            SaveCoins();
            OnCoinsChanged?.Invoke(TotalCoins);
            return true;
        }

        Debug.Log("Not enough coins to complete this purchase");
        return false;
    }
    
    public bool SpendDollars(int amount)
    {
        if (TotalDollars >= amount)
        {
            TotalDollars -= amount;
            SaveDollars();
            OnDollarsChanged?.Invoke(TotalDollars);
            return true;
        }

        Debug.Log("Not enough dollars to complete this purchase");
        return false;
    }
    
    private void SaveCoins()
    {
        PlayerPrefs.SetInt(COINS_KEY, TotalCoins);
        PlayerPrefs.Save();
    }
    
    private void SaveDollars()
    {
        PlayerPrefs.SetInt(DOLLARS_KEY, TotalDollars);
        PlayerPrefs.Save();
    }
}
