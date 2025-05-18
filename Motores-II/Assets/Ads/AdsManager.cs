using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public InitializeAds initializeAds;
    public BannerAds bannerAds;
    public InterstitialAds interstitialAds;
    public RewardedAds rewardedAds;

    public static AdsManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        rewardedAds.LoadRewardedAd();
        StartCoroutine(BannerAd());
        interstitialAds.LoadInterstitialAd();

    }

    public void ShowInterstitialAd()
    {
        interstitialAds.ShowInterstitialAd();
    }

    public void ShowRewardedAd()
    {
        rewardedAds.ShowRewardedAd();
    }

    IEnumerator BannerAd()
    {
        while (true) 
        {
            bannerAds.LoadBannerAd();
            yield return new WaitForSeconds(5);
            bannerAds.ShowBannerAd();
            yield return new WaitForSeconds(30);
            bannerAds.HideBannerAd();
            yield return new WaitForSeconds(30);
        }
    }
}

