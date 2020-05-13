using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using SecPlayerPrefs;

public class ShowFullScreenAds : MonoBehaviour {

    int gamesBetweenAd = 0;
    public int gamesRequiredBetweenAd = 2;
    public float minutesRequiredBetweenAd = 1.0f;
    string lastTime;
    string nowString;
    int minutes;

    void Start() {
        if(!Advertisement.isInitialized){
            Advertisement.Initialize("2677701");
        }
    }

    public void ShowInterstitial() {

        gamesBetweenAd = SecurePlayerPrefs.GetInt("gamesBetweenAd", 0);
        gamesBetweenAd++;
        SecurePlayerPrefs.SetInt("gamesBetweenAd", gamesBetweenAd);

        // Get the amount of time passed since the last video
        nowString = System.DateTime.Now.ToString();
        TimeSpan time = Convert.ToDateTime(nowString) - Convert.ToDateTime(SecurePlayerPrefs.GetString("timeLastAdShowed", nowString));

        // Ensure that time will be set for first video to show up
        if (SecurePlayerPrefs.GetString("timeLastAdShowed", nowString) == nowString) {
            SecurePlayerPrefs.SetString("timeLastAdShowed", nowString);
        }

        print("Key for successful interstitial: 0, true, true, true. Any false will keep the ad from showing.");
        print(SecurePlayerPrefs.GetInt("RemoveBannersAndPopUps", 0));
        print(gamesBetweenAd >= gamesRequiredBetweenAd);
        print("Games Required: " + gamesRequiredBetweenAd + " Games between: " + gamesBetweenAd);
        print(time.TotalMinutes >= minutesRequiredBetweenAd);
        print("Time Required: " + minutesRequiredBetweenAd + " Time: " + time.TotalMinutes);
        print(Advertisement.IsReady());

        if (SecurePlayerPrefs.GetInt("RemoveBannersAndPopUps", 0) == 0 && SecurePlayerPrefs.GetInt("DoubleStars", 0) == 0) {
            if (gamesBetweenAd >= gamesRequiredBetweenAd) {
                if (time.TotalMinutes >= minutesRequiredBetweenAd) {
                    if (Advertisement.IsReady()) {
                        Advertisement.Show("video");
                        SecurePlayerPrefs.SetInt("gamesBetweenAd", 0);
                        SecurePlayerPrefs.SetString("timeLastAdShowed", nowString);
                    }
                }
            }
        }
    }
}