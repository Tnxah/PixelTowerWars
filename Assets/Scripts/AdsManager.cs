using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
#if UNITY_ANDROID
    string gameID = "5332709";
    string interstitialId = "Interstitial_Android";
#endif 
    public static AdsManager instance;

    private void Awake()
    {
        Advertisement.Initialize(gameID, false, this);
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        Advertisement.Load(interstitialId, this);
    }

    public void ShowInterstitial()
    {
        Advertisement.Show(interstitialId, this);
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnInitializationComplete()
    {
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        //debug.text += $"Unity Ads Initialization Failed: {error.ToString()} - {message}";
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

 

    public void OnUnityAdsAdLoaded(string placementId)
    {
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        
    }
}
