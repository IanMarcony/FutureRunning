using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ShowAdsUnity : MonoBehaviour,IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField]
    private string gameIDAndroid;
    public bool showAd;
    public bool isFirstScene;
    public bool isBannerAd;

    public string adUnitPlacement;

    private int score ;
 

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameIDAndroid);
        Advertisement.Load(adUnitPlacement, this);
        score = PlayerPrefs.GetInt("score");
    }


    void Update()
    {
        if(isFirstScene&&Advertisement.IsReady()&&isBannerAd&&showAd){
            showAd =false;
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show(adUnitPlacement);
        }else if((score%3==0)&&Advertisement.IsReady()&&showAd){
            showAd =false;
            Advertisement.Show(adUnitPlacement, this);
        }
        
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
    
    }
 
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { }

}
