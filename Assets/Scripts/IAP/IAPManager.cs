using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour
{
    private const string DOLLARS_PACK_1 = "com.drifting.dollars1000";
    private const int DOLLARS_AMOUNT_PACK_1 = 1000;
    
    private const string DOLLARS_PACK_2 = "com.drifting.dollars2000";
    private const int DOLLARS_AMOUNT_PACK_2 = 2000;
    
    private const string DOLLARS_PACK_3 = "com.drifting.dollars5000";
    private const int DOLLARS_AMOUNT_PACK_3 = 5000;

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == DOLLARS_PACK_1)
        {
            WalletManager.Instance.AddDollars(DOLLARS_AMOUNT_PACK_1);
            Debug.Log("Dollars purchased successfully!");
        }

        if (product.definition.id == DOLLARS_PACK_2)
        {
            WalletManager.Instance.AddDollars(DOLLARS_AMOUNT_PACK_2);
            Debug.Log("Dollars purchased successfully!");
        }
        
        if (product.definition.id == DOLLARS_PACK_3)
        {
            WalletManager.Instance.AddDollars(DOLLARS_AMOUNT_PACK_3);
            Debug.Log("Dollars purchased successfully!");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription description)
    {
        Debug.LogError($"Purchase failed for product {product.definition.id}: {description}");
    }
}
