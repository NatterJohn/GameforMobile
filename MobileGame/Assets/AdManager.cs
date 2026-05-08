using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });

        /*LoadBanner();
        LoadInterstitial();
        LoadRewarded();
        ShowBanner();
        ShowInterstitial();
        ShowRewarded();*/
    }

    public void LoadBanner()
    {
        print("Banner Loaded!");
        string adUnitId = "ca-app-pub-8352643905405810/9281723139"; // Test ID

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest();
        bannerView.LoadAd(request);
    }

    public void ShowBanner()
    {
        print("Banner Shown!");
        bannerView?.Show();
    }

    public void HideBanner()
    {
        print("Banner Hidden!");
        bannerView?.Hide();
    }
    public void LoadInterstitial()
    {
        print("Inter Loaded!");
        string adUnitId = "ca-app-pub-8352643905405810/5414630318"; // Test ID

        InterstitialAd.Load(adUnitId, new AdRequest(),
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError("Interstitial failed to load: " + error);
                    return;
                }

                interstitialAd = ad;

                interstitialAd.OnAdFullScreenContentClosed += () =>
                {
                    LoadInterstitial(); // Reload after closing
                };
            });
    }

    public void ShowInterstitial()
    {
        print("Inter Shown!");
        if (interstitialAd != null)
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial not loaded yet");
        }
    }

    public void LoadRewarded()
    {
        print("Rewarded Loaded!");
        string adUnitId = "ca-app-pub-8352643905405810/1499436037"; // Test ID

        RewardedAd.Load(adUnitId, new AdRequest(),
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError("Rewarded failed to load: " + error);
                    return;
                }

                rewardedAd = ad;

                rewardedAd.OnAdFullScreenContentClosed += () =>
                {
                    LoadRewarded(); // Reload after closing
                };
            });
    }

    public void ShowRewarded()
    {
        print("Rewarded Shown!");
        if (rewardedAd != null)
        {
            rewardedAd.Show(reward =>
            {
                Debug.Log("User earned reward: " + reward.Amount);
            });
        }
        else
        {
            Debug.Log("Rewarded ad not loaded yet");
        }
    }
}


