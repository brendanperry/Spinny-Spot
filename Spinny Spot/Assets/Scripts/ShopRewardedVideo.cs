/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using SecPlayerPrefs;
using TMPro;

public class ShopRewardedVideo : MonoBehaviour {

    private RewardBasedVideoAd rewardBasedVideo;

    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Unlock unlock;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] GameObject errorPanel;
    int videosWatched = 0;

    private void Start() {

        videosWatched = SecurePlayerPrefs.GetInt("Watch5Videos", 0);

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        RequestRewardedVideo();
    }

    private void RequestRewardedVideo() {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(request, adUnitId);
    }

    private void HandleRewardBasedVideoLoaded(object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    private void HandleRewardBasedVideoOpened(object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    private void HandleRewardBasedVideoStarted(object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    private void HandleRewardBasedVideoClosed(object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    private void HandleRewardBasedVideoRewarded(object sender, Reward args) {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);

        videosWatched++;
        SecurePlayerPrefs.SetInt(progressBar.playerPrefsName, videosWatched);
        progressBar.UpdateProgress();
        if (videosWatched >= 5) {
            unlock.UnlockCharacter(progressBar.characterName);
        }
    }

    private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args) {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    public void ShowVideo() {
        if (rewardBasedVideo.IsLoaded()) {
            rewardBasedVideo.Show();

#if UNITY_EDITOR
            videosWatched++;
            SecurePlayerPrefs.SetInt("Watch5Videos", videosWatched);
            progressBar.UpdateProgress();
            if (videosWatched >= 5) {
                print(progressBar.characterName);
                unlock.UnlockCharacter(progressBar.characterName);
                buttonText.text = "Select";
                progressBar.gameObject.SetActive(false);
                SecurePlayerPrefs.SetInt("ProgressBar" + progressBar.characterName, 1);
            }
#endif
        } else {
            errorPanel.SetActive(true);
        }
    }
}*/