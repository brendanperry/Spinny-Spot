/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using SecPlayerPrefs;

public class ShowBanner : MonoBehaviour {

    private BannerView bannerView;

#if UNITY_ANDROID
    public string adUnitIdAndroid;
    // Test ID -> "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
    public string adUnitIdIOS;
    // Test ID -> "ca-app-pub-3940256099942544/6300978111";
#endif

    public void Start() {
        if(PlayerPrefs.GetInt("RemoveBannersAndPopUps", 0) == 0) {
            this.RequestBanner();
        }
    }

    private void RequestBanner() {
        // Create a smart banner at the bottom of the screen.
#if UNITY_ANDROID
    bannerView = new BannerView(adUnitIdAndroid, AdSize.SmartBanner, AdPosition.Bottom);
#elif UNITY_IPHONE
    bannerView = new BannerView(adUnitIdIOS, AdSize.SmartBanner, AdPosition.Bottom);
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

        bannerView.OnAdLoaded += BannerView_OnAdLoaded;
    }

    private void BannerView_OnAdLoaded(object sender, System.EventArgs e) {
        bannerView.Show();
    }

    public void HandleAdLoaded(object sender, EventArgs args) {
        if(SecurePlayerPrefs.GetInt("RemoveAds", 0) == 1) {
            bannerView.Hide();
        }
    }
}*/
