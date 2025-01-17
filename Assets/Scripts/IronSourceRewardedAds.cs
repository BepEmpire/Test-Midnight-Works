using UnityEngine;
using UnityEngine.Events;

public class IronSourceRewardedAds : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent onGetReward;
    [SerializeField] private UnityEvent onEditorGetReward;
    [SerializeField] private UnityEvent onRewardNotAvailable;
    
    [Header("Settings")]
    [SerializeField] private string androidAppKey = "85460dcd";
    [SerializeField] private string iOSAppKey = "8545d445";

    private string _currentAppKey;
    
    private void Awake()
    {
#if UNITY_ANDROID
        _currentAppKey = androidAppKey;
#elif UNITY_IPHONE
        _currentAppKey = iOSAppKey;
#else
        _currentAppKey = "unexpected_platform";
#endif
        
        IronSource.Agent.validateIntegration();
        
        InitRewardedVideo();
    }

    private void Start()
    {
        IronSourceEvents.onRewardedVideoAdRewardedEvent += OnRewardedAdCompleted;
        IronSourceEvents.onRewardedVideoAdClosedEvent += OnRewardedAdClosed;
    }
    
    private void OnDestroy()
    {
        IronSourceEvents.onRewardedVideoAdRewardedEvent -= OnRewardedAdCompleted;
        IronSourceEvents.onRewardedVideoAdClosedEvent -= OnRewardedAdClosed;
    }

    private void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void GetReward()
    {
#if UNITY_EDITOR
        Debug.Log("Test Reward In Editor");
        onEditorGetReward?.Invoke();
#endif
        
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            Debug.Log("Show Rewarded Video");
            
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("Rewarded Video Is Not Available");
            
            onRewardNotAvailable?.Invoke();
        }
    }

    private void InitRewardedVideo()
    {
        IronSource.Agent.init(_currentAppKey, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.shouldTrackNetworkState(true);
    }
    
    private void OnRewardedAdCompleted(IronSourcePlacement placement)
    {
        Debug.Log($"Reward received: {placement.getRewardName()} - {placement.getRewardAmount()}");
        
        onGetReward?.Invoke();
    }

    private void OnRewardedAdClosed()
    {
        InitRewardedVideo();
    }
}