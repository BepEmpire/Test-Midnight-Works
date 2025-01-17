using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Configuration")]
    [SerializeField] private int[] carPrices = { 0, 1000 };
    [SerializeField] private Image[] carImages;
    [SerializeField] private TextMeshProUGUI buyButtonText;
    [SerializeField] private Button buyButton;

    [Header("UI Navigation")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    private const string DEFAULT_CAR = "CarPurchased_0";
    private const string PURCHASED_CAR = "CarPurchased_";
    private const string SELECTED = "Selected";
    private const string SELECT = "Select";

    private int _currentCarId;

    private void Start()
    {
        InitializeShop();
    }

    private void InitializeShop()
    {
        PlayerPrefs.SetInt(DEFAULT_CAR, 1);
        _currentCarId = PlayerPrefs.GetInt(Keys.SELECTED_CAR, 0);

        nextButton.onClick.AddListener(() => SwitchCar(1));
        previousButton.onClick.AddListener(() => SwitchCar(-1));
        buyButton.onClick.AddListener(() => BuyOrSelectCar(_currentCarId));

        UpdateShopUI();
    }

    private void UpdateShopUI()
    {
        for (int i = 0; i < carImages.Length; i++)
        {
            carImages[i].gameObject.SetActive(i == _currentCarId);
        }

        bool isPurchased = _currentCarId == 0 || PlayerPrefs.GetInt(PURCHASED_CAR + _currentCarId, 0) == 1;
        bool isSelected = _currentCarId == PlayerPrefs.GetInt(Keys.SELECTED_CAR, 0);
        
        if (isSelected)
        {
            buyButtonText.text = SELECTED;
            buyButton.interactable = false;
        }
        else if (isPurchased)
        {
            buyButtonText.text = SELECT;
            buyButton.interactable = true;
        }
        else
        {
            buyButtonText.text = carPrices[_currentCarId].ToString();
            buyButton.interactable = WalletManager.Instance.TotalCoins >= carPrices[_currentCarId];
        }
    }

    private void BuyOrSelectCar(int carId)
    {
        bool isPurchased = PlayerPrefs.GetInt(PURCHASED_CAR + carId, 0) == 1;

        if (!isPurchased && WalletManager.Instance.SpendCoins(carPrices[carId]))
        {
            isPurchased = true;
            PlayerPrefs.SetInt(PURCHASED_CAR + carId, 1);
        }

        if (isPurchased || carId == 0)
        {
            SelectCar(carId);
        }

        UpdateShopUI();
    }

    private void SelectCar(int carId)
    {
        PlayerPrefs.SetInt(Keys.SELECTED_CAR, carId);
        PlayerPrefs.Save();
    }

    private void SwitchCar(int direction)
    {
        _currentCarId += direction;
        if (_currentCarId < 0) _currentCarId = carImages.Length - 1;
        if (_currentCarId >= carImages.Length) _currentCarId = 0;

        UpdateShopUI();
    }
}