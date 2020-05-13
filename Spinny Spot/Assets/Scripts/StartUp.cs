using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.Advertisements;
using SecPlayerPrefs;
using OneSignalPush;

public class StartUp : MonoBehaviour {

    public float timeToWait = 5.0f;
    [SerializeField]

    int sessionCount = 0;

    public bool testMode = true;

    void Start() {
        //iOSReviewRequest.Request();

        sessionCount = SecurePlayerPrefs.GetInt("sessionCount", 0);
        sessionCount++;
        SecurePlayerPrefs.SetInt("sessionCount", sessionCount);

        SecurePlayerPrefs.SetInt("NativeReview", 1);

        GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
        Social.localUser.Authenticate(success => {
            if (success) {
                Debug.Log("Authentication successful");
                string userInfo = "Username: " + Social.localUser.userName +
                    "\nUser ID: " + Social.localUser.id +
                    "\nIsUnderage: " + Social.localUser.underage;
                Debug.Log(userInfo);
            }
            else
                Debug.Log("Authentication failed");
        });

        // Initialize Unity Ads
        Advertisement.Initialize("2677701", testMode);

        	// Use this for initialization
		OneSignal.StartInit("9778f7d3-1a67-4f53-962a-563943835df7")
            .Settings(new Dictionary<string, bool>() {
                { OneSignal.kOSSettingsAutoPrompt, false },
                { OneSignal.kOSSettingsInAppLaunchURL, true } })
            .EndInit();
  
        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;

        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene() {
        yield return new WaitForSeconds(timeToWait);

        SceneManager.LoadScene("Main_Menu");
    }
}
