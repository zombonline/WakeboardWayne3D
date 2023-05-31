using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener, IUnityAdsLoadListener
{
    [SerializeField] UnityEvent rewardedAdFinished, rewardedAdSkipped, rewardedAdFailed;

    private void Start()
    {
        Advertisement.Initialize("5269533", true, this);
    }

    public void PlayAD()
    {
        if(Advertisement.isInitialized)
        {
            Advertisement.Load("Interstitial_Android", this);
            Advertisement.Show("Interstitial_Android", this);
        }
    }

    public void PlayRewardedAd()
    {
        if(Advertisement.isInitialized)
        {
            Advertisement.Load("Rewarded_Android", this);
            Advertisement.Show("Rewarded_Android", this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization Complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
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

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(placementId == "Rewarded_Android")
        {
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                rewardedAdFinished.Invoke();
            }
            else if(showCompletionState == UnityAdsShowCompletionState.SKIPPED)
            {
                rewardedAdFinished.Invoke();
            }
            else
            {
                rewardedAdFailed.Invoke();
            }
        }
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        
    }
}
