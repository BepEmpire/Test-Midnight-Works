using UnityEngine;
using TMPro;

public class DollarsDisplay : MonoBehaviour
{
    private TextMeshProUGUI _dollarsText;

    private void Awake()
    {
        _dollarsText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetText(WalletManager.Instance.TotalDollars);
        WalletManager.Instance.OnDollarsChanged.AddListener(SetText);
    }

    private void OnDestroy()
    {
        WalletManager.Instance.OnDollarsChanged.RemoveListener(SetText);
    }

    private void SetText(int dollars)
    {
        _dollarsText.text = $"$ {dollars}";
    }
}
