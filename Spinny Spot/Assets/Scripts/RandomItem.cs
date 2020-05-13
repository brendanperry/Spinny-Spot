﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OneSignalPush;
using TMPro;
using SecPlayerPrefs;
using UnityEngine.Advertisements;

public class RandomItem : MonoBehaviour {

    public int[] coinRewards;

    public GameObject panel;
    public GameObject notifPanel;
    public GameObject ErrorPanel;

    public GameObject ChestTimerObj;
    ChestTimer chestTimerScript;

    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI text;
    public string currencyName;
    int reward;

    public GameObject Currency_ManagerObj;
    CurrencyManager currencyManagerScript;

    public GameObject CurrencyTextObj;
    DisplayCurrencyCount displayCurrencyCountScript;

    [SerializeField] GameObject star;
    [SerializeField] GameObject yellow;
    public GameObject particles;
    [SerializeField] float timeToWaitForPopUp = 3;

    [SerializeField] AudioClip rewardSound;
    AudioSource audioSource;

    int chestsOpened = 0;
    public int rewardAmount = 25;
    public GameObject CloseText;
    public TextMeshProUGUI CollectText;
    public GameObject starEffect;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        currencyManagerScript = Currency_ManagerObj.GetComponent<CurrencyManager>();
        displayCurrencyCountScript = CurrencyTextObj.GetComponent<DisplayCurrencyCount>();
        chestTimerScript = ChestTimerObj.GetComponent<ChestTimer>();
        chestsOpened = SecurePlayerPrefs.GetInt("chestsOpened", 0);
    }

    public void OpenChest() {
        if (buttonText.text == "Claim Reward") {

            if(SecurePlayerPrefs.GetInt("soundfx", 0) == 0) {
                audioSource.PlayOneShot(rewardSound);
            }
            int randomNum = Random.Range(0, coinRewards.Length - 1);
            reward = coinRewards[randomNum];

            chestTimerScript.SetPrefs();
            chestTimerScript.ShowTime(0);

            // Create panel animation
            text.text = reward.ToString();
            StartCoroutine(ShowPopUp());
            print(reward);

            chestsOpened++;
            SecurePlayerPrefs.SetInt("chestsOpened", chestsOpened);

            if (chestsOpened == 3) {
                iOSReviewRequest.Request();
                print("Show native review!");
            }

        } else {
            print("Cannot open chest");
            if (!audioSource.isPlaying) {
                audioSource.Play();
            } 
        }
    }

    public void UpdateNotifs() {
        chestTimerScript.PostNotification();
    }

    IEnumerator ShowPopUp() {
        if(Advertisement.IsReady() && SecurePlayerPrefs.GetInt("DoubleStars", 0) == 0) {
            CollectText.text = "Watch Video to Double";
            print("Show");
        } else {
            CloseText.SetActive(false);
            print("No Video");
        } 

        yield return new WaitForSeconds(timeToWaitForPopUp);
        panel.SetActive(true);
    }

    public void VideoClick() {
        if(CollectText.text == "Watch Video to Double") {
            ShowVideo();
        }
    }

    public void ClaimReward() {
        currencyManagerScript.AddCurrency(reward, new Vector3(0,0,0));
        displayCurrencyCountScript.UpdateTotal();
        panel.SetActive(false);

        LeanTween.value(yellow, 1, 0, 1).setOnUpdate((float flt) => {
            yellow.GetComponent<Image>().fillAmount = flt;
        });
        star.GetComponent<Animator>().Play("New State");
        particles.SetActive(false);

        if(SecurePlayerPrefs.GetInt("Notif", 0) == 0) {
            notifPanel.SetActive(true);
            SecurePlayerPrefs.SetInt("Notif", 1);
        } else {
            UpdateNotifs();
        }
    }

    void ShowVideo() {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show("giftVideo", options);
    }

    void HandleShowResult (ShowResult result) {
        if(result == ShowResult.Finished) {
            print("Video Complete");

            // Give user reward
            currencyManagerScript.AddCurrency(reward, new Vector3(0,0,0));
            displayCurrencyCountScript.UpdateTotal();
        }
    }

    public void CloseRewardPanel() {
        panel.SetActive(false);
    }

    public void CloseErrorMessage() {
        ErrorPanel.SetActive(false);
    }
}