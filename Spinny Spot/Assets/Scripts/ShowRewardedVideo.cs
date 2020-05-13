using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using SecPlayerPrefs;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class ShowRewardedVideo : MonoBehaviour {

    public GameObject Currency_ManagerObj;
    CurrencyManager currencyManagerScript;

    public GameObject CurrencyTextObj;
    DisplayCurrencyCount displayCurrencyCountScript;

    public GameObject showVideoButton;
    public GameObject showVideoStars;

    public GameObject circle;

    int gamesBetweenVideo = 0;
    string nowString;

    int gamesBetweenVideoStars = 0;
    string nowStringStars;

    public int gamesRequiredBetweenVideo = 5;
    public float minutesRequiredBetweenVideo = 3;

    public int gamesRequiredBetweenVideoStars = 5;
    public float minutesRequiredBetweenVideoStars = 3;

    int enemiesRemaining = 0;
    int level = 0;

    private void Start() {
        currencyManagerScript = Currency_ManagerObj.GetComponent<CurrencyManager>();
        displayCurrencyCountScript = CurrencyTextObj.GetComponent<DisplayCurrencyCount>();

        if(!Advertisement.isInitialized){
            Advertisement.Initialize("2677701");
        }
    }

    void ShowStars() {
        gamesBetweenVideoStars = SecurePlayerPrefs.GetInt("gamesBetweenVideoStars", 0);
        gamesBetweenVideoStars++;
        SecurePlayerPrefs.SetInt("gamesBetweenVideoStars", gamesBetweenVideoStars);

        // Get the amount of time passed since the last video
        nowStringStars = System.DateTime.Now.ToString();
        print(nowStringStars);
        TimeSpan timeStars = Convert.ToDateTime(nowStringStars) - Convert.ToDateTime(SecurePlayerPrefs.GetString("timeLastVideoShowedStars", nowStringStars));
        print(timeStars);
        print(timeStars.TotalMinutes);

        // Ensure that time will be set for first video to show up
        if (SecurePlayerPrefs.GetString("timeLastVideoShowedStars", nowStringStars) == nowStringStars) {
            print("Before: " + SecurePlayerPrefs.GetString("timeLastVideoShowedStars", nowStringStars));
            SecurePlayerPrefs.SetString("timeLastVideoShowedStars", nowStringStars);
            print("After: " + SecurePlayerPrefs.GetString("timeLastVideoShowedStars", nowStringStars));
        }

        if (Advertisement.IsReady() && gamesBetweenVideoStars >= gamesRequiredBetweenVideoStars && timeStars.TotalMinutes >= minutesRequiredBetweenVideoStars) {
            showVideoStars.SetActive(true);
        }
    }

    public void OnGameOver(int enemiesLeftOnDeath, int currentLevel) {
        ShowStars();

        gamesBetweenVideo = SecurePlayerPrefs.GetInt("gamesBetweenVideo", 0);
        gamesBetweenVideo++;
        SecurePlayerPrefs.SetInt("gamesBetweenVideo", gamesBetweenVideo);

        enemiesRemaining = enemiesLeftOnDeath;
        level = currentLevel;

        // Get the amount of time passed since the last video
        nowString = System.DateTime.Now.ToString();
        print(nowString);
        TimeSpan time = Convert.ToDateTime(nowString) - Convert.ToDateTime(SecurePlayerPrefs.GetString("timeLastVideoShowed", nowString));
        print(time);
        print(time.TotalMinutes);

        // Ensure that time will be set for first video to show up
        if (SecurePlayerPrefs.GetString("timeLastVideoShowed", nowString) == nowString) {
            print("Before: " + SecurePlayerPrefs.GetString("timeLastVideoShowed", nowString));
            SecurePlayerPrefs.SetString("timeLastVideoShowed", nowString);
            print("After: " + SecurePlayerPrefs.GetString("timeLastVideoShowed", nowString));
        }


        bool canShow = false;
        if (Advertisement.IsReady() && gamesBetweenVideo >= gamesRequiredBetweenVideo && time.TotalMinutes >= minutesRequiredBetweenVideo) {
            if(SceneManager.GetActiveScene().name == "Game") {
                if(level >= 10 && level < 20 && enemiesLeftOnDeath <= 10) {
                    canShow = true;
                } else if (level >= 20 && level < 30 && enemiesLeftOnDeath <= 15) {
                    canShow = true;
                } else if (level >= 30 && level < 40 && enemiesLeftOnDeath <= 20) {
                    canShow = true;
                } else if (level >= 40 && level <= 50 && enemiesLeftOnDeath <= 25) {
                    canShow = true;
                } 
            } else {
                if(level >= 5 && level < 10 && enemiesLeftOnDeath <= 5) {
                    canShow = true;
                } else if (level >= 10 && level < 15 && enemiesLeftOnDeath <= 10) {
                    canShow = true;
                } else if (level >= 15 && level < 20 && enemiesLeftOnDeath <= 15) {
                    canShow = true;
                } else if (level >= 20 && level <= 25 && enemiesLeftOnDeath <= 20) {
                    canShow = true;
                } 
            }

            if(canShow == true) {
                showVideoButton.SetActive(true);
                circle.SetActive(false);

                Analytics.CustomEvent("Revive Pop-up", new Dictionary<string, object>
                {
                    {"Shown", 1 }
                });
            } else {
                print("Not revive capable");
            }
            
        } else {
            print("Don't show video");
        }
    }

    public void ShowVideo() {
        if (Advertisement.IsReady()) {
            circle.SetActive(true);
            //showVideoButton.SetActive(false);

            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;

            Advertisement.Show("revive", options);

            CloseVideoPanel();
        }
    }

    void HandleShowResult (ShowResult result) {
        if(result == ShowResult.Finished) {
            print("Video Complete");

            SecurePlayerPrefs.SetInt("reviveEnemyAmount", enemiesRemaining + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void CloseVideoPanel() {
        SecurePlayerPrefs.SetInt("gamesBetweenVideo", 0);
        SecurePlayerPrefs.SetString("timeLastVideoShowed", nowString);

        StartCoroutine(CloseVideoDelay());
    }

    IEnumerator CloseVideoDelay() {
        yield return new WaitForSeconds(.25f);
        showVideoButton.SetActive(false);
        circle.SetActive(true);
    }

    public void ShowStarVideo() {
        if(Advertisement.IsReady()) {

            SecurePlayerPrefs.SetInt("gamesBetweenVideoStars", 0);
            SecurePlayerPrefs.SetString("timeLastVideoShowedStars", nowStringStars);
            print(nowStringStars);

            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResultStars;

            Advertisement.Show("rewardedVideo", options);
        }
    }

    void HandleShowResultStars(ShowResult result) {
        if(result == ShowResult.Finished) {
            print("Video Complete");

            currencyManagerScript.AddCurrency(25, new Vector3(0, 0, 0));
            displayCurrencyCountScript.UpdateTotal();
            showVideoStars.SetActive(false);
        }
    }
}