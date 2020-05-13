using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OneSignalPush.MiniJSON;
using TMPro;
using System;
using SecPlayerPrefs;

public class ChestTimer : MonoBehaviour {

    private string url = "https://brendanperry.me/dateandtime.php";

    [SerializeField]
    private int minutesRequired = 10;
    private string timeWhenOpenedLastChest;
    private string currentTime;
    private int minutesPassed;

    [SerializeField]
    GameObject ErrorPanel;
    public GameObject particles;

    public Image star;
    public Image yellow;

    private void OnApplicationPause(bool pause) {
        if (!pause) {
            GetComponent<TextMeshProUGUI>().text = "Loading";
            GetCurrentTime();
        }
    }

    // Get previous time and start function
    void Start() {
        InvokeRepeating("GetCurrentTime", 0, 60);
    }

    // Update currentTime every 60 seconds
    void GetCurrentTime() {
        timeWhenOpenedLastChest = SecurePlayerPrefs.GetString("timeWhenOpenedLastChest", "17:5:1:0:0");
        StartCoroutine(GetTime());
    }

    // Sets the current time
    IEnumerator GetTime() {
        print("try");
        WWW www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error)) {
            print(www.error);
            ErrorPanel.SetActive(true);
        } else {
            if (www.isDone) {
                print(www.text);
                currentTime = www.text;
                CalculateTimePassed();
            }
        }
    }

    // Variables specific to the calc function
    int prevYear, year;
    int prevMonth, month;
    int prevDay, day;
    int prevHour, hour;
    int prevMinute, minute;
    int daysPassed;
    int prevTotalMinutes, totalMinutes;

    // Calculates the time in minutes between the time since the last chest was opened and the time the function is being ran
    void CalculateTimePassed() {

         print("CALC");
         int.TryParse(timeWhenOpenedLastChest.Split(':')[0], out prevYear);
         int.TryParse(timeWhenOpenedLastChest.Split(':')[1], out prevMonth);
         int.TryParse(timeWhenOpenedLastChest.Split(':')[2], out prevDay);
         int.TryParse(timeWhenOpenedLastChest.Split(':')[3], out prevHour);
         int.TryParse(timeWhenOpenedLastChest.Split(':')[4], out prevMinute);

         int.TryParse(currentTime.Split(':')[0], out year);
         int.TryParse(currentTime.Split(':')[1], out month);
         int.TryParse(currentTime.Split(':')[2], out day);
         int.TryParse(currentTime.Split(':')[3], out hour);
         int.TryParse(currentTime.Split(':')[4], out minute);

        print(prevYear + " " + prevMonth + " " + prevDay + " " + prevHour + " " + prevMinute + " " + 0);
        print(year + " " + month + " " + day + " " + hour + " " + minute + " " + 0);

        DateTime lastTime = new DateTime(prevYear, prevMonth, prevDay, prevHour, prevMinute, 0);
        DateTime thisTime = new DateTime(year, month, day, hour, minute, 0);
        TimeSpan totalTime = thisTime - lastTime;
        print("Total Time: " + totalTime.TotalMinutes);

        minutesPassed = (int)totalTime.TotalMinutes;
        if(minutesPassed == totalMinutes) {
            SecurePlayerPrefs.SetInt("ActiveMessage", 0);
        }
        ShowTime(minutesPassed);
    } 

     // Update the text
     int minutesRemaining;
     public void ShowTime(int minutesPassed) {
         print("UPDATE TEXT");
         int minutesRemaining = minutesRequired - minutesPassed;
         print(minutesRemaining + " Remaining");

         if (minutesRemaining <= 0) {
            print("Unlocked!");
            minutesPassed = minutesRequired;
            star.gameObject.GetComponent<Animator>().Play("starFloat");
            particles.SetActive(true);
            GetComponent<TextMeshProUGUI>().text = "Claim Reward";
         }
         else {
             print(minutesRemaining);

             int hoursLeft;
             int minutesLeft;

             int.TryParse((minutesRemaining / 60).ToString().Split('.')[0], out hoursLeft);

             int minutesTemp = minutesRemaining;
             if (hoursLeft > 0) {
                 while (minutesTemp >= 60) {
                     minutesTemp -= 60;
                 }
                 minutesLeft = minutesTemp;
             }
             else {
                 minutesLeft = minutesRemaining;
             }
          
            GetComponent<TextMeshProUGUI>().SetText(hoursLeft.ToString() + "h " + minutesLeft + "m");
         }


         if(yellow.fillAmount == 0) {
            LeanTween.value(this.gameObject, 0, minutesPassed * 1.0f / minutesRequired, 1).setOnUpdate((float flt) => {
                yellow.fillAmount = flt;
            });
         }
    } 

        public void SetPrefs() {
        SecurePlayerPrefs.SetString("timeWhenOpenedLastChest", currentTime);
    }

    public void PostNotification() {
        GameObject.Find("Notification Manager").GetComponent<NotificationManager>().PostNotification(OneSignal.GetPermissionSubscriptionState().subscriptionStatus.userId);
    }
}